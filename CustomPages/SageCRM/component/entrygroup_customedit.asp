<!-- #include file ="SageCRM.js" -->
<%
//******************************************************************************
//******************************************************************************
/*
*  TITAN ASP.NET Suite for Sage CRM
*/
//******************************************************************************
try
{
  var EntityName=Request.QueryString("EntityName");
  var EntryBlock=Request.QueryString("EntryBlock");
  var EntityWhere=new String(Request.QueryString("EntityWhere"));
  EntityWhere_arr=EntityWhere.split("=");
  EntityWhere_valstr=new String(EntityWhere_arr[1]);
  EntityWhere_valstr_arr=EntityWhere_valstr.split(",");
  if (Defined(EntityWhere_valstr_arr[0]))
    EntityWhere=EntityWhere_arr[0]+"="+EntityWhere_valstr_arr[0];
  var BlockTitle=Request.QueryString("BlockTitle");
  var em=Request.QueryString("emx");
  
 // Response.Write("here"+Request.QueryString);
 // Response.End();  
  
  debugcrm("entrygroup_frm", Request.Form);
  //data  
  var record_data=null;
  //screen
  var v_sql="select distinct SeaP_Order,SeaP_ColName,SeaP_Newline,Custom_Edits.ColP_EntryType , ColP_Required"+
            " from custom_screens,Custom_Edits"+ 
            " where SeaP_SearchBoxName='"+EntryBlock+"' and Seap_DeviceID is null"+
            " and ColP_ColName=SeaP_ColName order by SeaP_Order";

  var record_screen=eWare.CreateQueryObj(v_sql);
  record_screen.SelectSQL();

  if (EntityWhere.length>0)
  {
    record_data=eWare.FindRecord(EntityName, EntityWhere);
  }else
  if (em=="Y")
  {    
    record_data=eWare.CreateRecord(EntityName);
  }

  if ((em!="")&&(em+""!="undefined"))
  {    
    while (!record_screen.eof)
    {
      if (record_screen("ColP_EntryType")=="45")
      {
        if (Request.Form(record_screen("SeaP_ColName"))=="on")
        {
          record_data(record_screen("SeaP_ColName"))="Y";
        }else{
          record_data(record_screen("SeaP_ColName"))="N";
        }
      }else{
        record_data(record_screen("SeaP_ColName"))=Request.Form(record_screen("SeaP_ColName"));    
      }
      record_screen.Next();
    }       
    var recid = record_data.SaveChanges();  
    Response.Write(record_data.recordid);
    Response.End();
  }  

  //commented out as did not work as such
  var result="<div class=\"cTab\" >tab top</div>";
  result='<br /><div id="'+EntryBlock+'_wrapper" class=\"entrygroup_wrapper\" >';  
  while (!record_screen.eof)
  {
    var rec_data="";
    if (record_data!=null)
      rec_data=checkData(record_data(record_screen("SeaP_ColName")),record_screen("ColP_EntryType"),record_screen("SeaP_ColName"));
    result+="<div id=\""+record_screen("SeaP_ColName")+"_caption\" class=\"VIEWBOXCAPTION\" >"+getColTrans(record_screen("SeaP_ColName"))+":</div>";
    result+=getFieldComponent(record_screen("ColP_EntryType"),record_screen("SeaP_ColName"),rec_data)
    record_screen.Next();
  }
  result+='</div>';  
  Response.Write(result);

  debugcrm("entrygroup", result);

}catch(e){
 logerror(e);
}

function getFieldComponent(ColP_EntryType,SeaP_ColName,data)
{
   if (ColP_EntryType=="10")
     return getFieldText(SeaP_ColName,data); 
   if (ColP_EntryType=="21")
     return getFieldSelect(SeaP_ColName,data); 
   if (ColP_EntryType=="22")
     return getFieldUser(SeaP_ColName,data); 
   if (ColP_EntryType=="56")
     return getFieldAdvSS(SeaP_ColName,data); 
   if (ColP_EntryType=="44")
     return getFieldText44(SeaP_ColName,data); 
   if (ColP_EntryType=="11")
     return getFieldText11(SeaP_ColName,data);
   if (ColP_EntryType=="45")
     return getFieldText45(SeaP_ColName,data);
      
//default
   return getFieldText(SeaP_ColName,data);
}
function getFieldAdvSS(SeaP_ColName, data)
{
  if (data=="")
  {
      return "<input id=\""+SeaP_ColName+"\" name=\""+SeaP_ColName+"\" value=\"\"  />";  
  }else{
    var v_sql="select comp_name from vcompany where comp_companyid="+data;      
    var v_displayfield="comp_name";
    if (SeaP_ColName.indexOf("personid")>-1)
    {
      v_sql="select rtrim(pers_firstname)+' '+rtrim(pers_lastname) as personfullname from vperson where pers_personid="+data;
      v_displayfield="personfullname";  
    }                                                                                                                  
  
    var record_capt=eWare.CreateQueryObj(v_sql);
    record_capt.SelectSQL();
    while (!record_capt.eof)
    {
      return "<input id=\""+SeaP_ColName+"\" name=\""+SeaP_ColName+"\" value=\""+record_capt(v_displayfield)+"\"  />";
    } 
  }
}
function getFieldUser(SeaP_ColName, data)
{
  var res="<select id=\""+SeaP_ColName+"\" name=\""+SeaP_ColName+"\"  />";

  var v_sql="select User_UserId, User_FirstName, User_LastName from Users"+
            " WHERE User_IsTemplate='N' and (User_Resource='false' or User_Resource is null)"+ 
            " order by User_FirstName";
  var record_capt=eWare.CreateQueryObj(v_sql);
  record_capt.SelectSQL();
  while (!record_capt.eof)
  {
    var v_selected="";
    if (record_capt("User_UserId")==data)
      v_selected="SELECTED";
    res+="<option value=\""+record_capt("User_UserId")+"\""+v_selected+" >"+record_capt("User_FirstName")+
            " "+record_capt("User_LastName")+
            "</option>";
    record_capt.Next();
  }  
    
  res+="</select>";
  return res;
}
function getFieldSelect(SeaP_ColName, data)
{
  var res="<select id=\""+SeaP_ColName+"\" name=\""+SeaP_ColName+"\"  />";

  var v_sql="select Capt_Code, Capt_US from Custom_Captions where "+
        "Capt_Family='"+SeaP_ColName+"' order by Capt_Order";
  var record_capt=eWare.CreateQueryObj(v_sql);
  record_capt.SelectSQL();
  while (!record_capt.eof)
  {
    var v_selected="";
    if (record_capt("Capt_Code")==data)
      v_selected="SELECTED";
    res+="<option value=\""+record_capt("Capt_Code")+"\""+v_selected+" >"+record_capt("Capt_US")+"</option>";
    record_capt.Next();
  }  
    
  res+="</select>";
  return res;
}
function getFieldText44(SeaP_ColName, data)
{
  if (data=="")
  {
    return "<span>&nbsp;</span>";  
  }else{
    return "<input id=\""+SeaP_ColName+"\" name=\""+SeaP_ColName+"\" value=\""+data+"\" />";
  }
}
function getFieldText45(SeaP_ColName, data)
{
  var checked="";
  if (data=="Y")
    checked="checked";
  return "<input type=\"checkbox\" id=\""+SeaP_ColName+"\" name=\""+SeaP_ColName+"\" "+checked+" />";
  
}
function getFieldText11(SeaP_ColName, data)
{
  return "<textarea id=\""+SeaP_ColName+"\" name=\""+SeaP_ColName+"\" rows=5 cols=15 >"+data+"</textarea>";
}
function getFieldText(SeaP_ColName, data)
{
  return "<input id=\""+SeaP_ColName+"\" name=\""+SeaP_ColName+"\" value=\""+data+"\"  onChange=\"getFieldTextOnChange(this)\"  />";
}
function getColTrans(colname)
{
      return eWare.GetTrans("ColNames",colname);
}
function checkData(val, ColP_EntryType,SeaP_ColName)
{
  if ((val+"")=="undefined")
    return "";
  if (ColP_EntryType=="21")
  {
      return eWare.GetTrans(SeaP_ColName,val);
  }
  return val;
}
%>