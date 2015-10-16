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
  
  debugcrm("entrygroup_frm", Request.Form);
  //data  
  var record_data=eWare.FindRecord(EntityName, EntityWhere);
  //screen
  var v_sql="select SeaP_Order,SeaP_ColName,SeaP_Newline,Custom_Edits.ColP_EntryType , ColP_Required"+
            " from custom_screens,Custom_Edits"+ 
            " where SeaP_SearchBoxName='"+EntryBlock+"' and Seap_DeviceID is null"+
            " and ColP_ColName=SeaP_ColName order by SeaP_Order";

  var record_screen=eWare.CreateQueryObj(v_sql);
  //Response.Write(v_sql);
  record_screen.SelectSQL();
  //commented out as did not work as such
  var result="<div class=\"cTab\" >tab top</div>";
  result='<div id="'+EntryBlock+'_wrapper" class=\"entrygroup_wrapper\" >';  
  while (!record_screen.eof)
  {
    var rec_data=checkData(record_data(record_screen("SeaP_ColName")),record_screen("ColP_EntryType"),record_screen("SeaP_ColName"));
    if (rec_data!="")
    {
      result+="<div id=\""+record_screen("SeaP_ColName")+"_caption\" class=\"VIEWBOXCAPTION\" >"+getColTrans(record_screen("SeaP_ColName"))+":</div>";
      result+="<div id=\""+record_screen("SeaP_ColName")+"\" class=\"CRMData\">"+rec_data+"</div>";
    }
    record_screen.Next();
  }
  result+='</div>';  
  Response.Write(result);

  debugcrm("entrygroup", result);

}catch(e){
  logerror(e);
}

function getFieldAdvSS(SeaP_ColName, data)
{
  if ((data==null) || (data=="") || (data+""=="undefined"))
    return "";
    
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
      return record_capt(v_displayfield);
    } 
  
}

function getColTrans(colname)
{
      return eWare.GetTrans("ColNames",colname);
}
function checkData(val, ColP_EntryType,SeaP_ColName)
{
  if (ColP_EntryType=="56")
    return getFieldAdvSS(SeaP_ColName,val);
  if ((val+"")=="undefined")
    return "";
  if (ColP_EntryType=="21")
  {
      return eWare.GetTrans(SeaP_ColName,val);
  }
  return val;
}
%>