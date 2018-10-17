<!-- #include file ="SageCRM.js" -->
<%
//******************************************************************************
//******************************************************************************
/*
*  TITAN ASP.NET Suite for Sage CRM
*  19/3/2010 fix for where clause
*/
//******************************************************************************
try
{

  //get the data
  ListBlock=new String(Request.QueryString("ListBlock"));
  EntityName=new String(Request.QueryString("EntityName"));
  EntityWhere=new String(Request.QueryString("EntityWhere"));
  var EntityWhere=new String(Request.QueryString("EntityWhere"));
  EntityWhere_arr=EntityWhere.split("=");
  EntityWhere_valstr=new String(EntityWhere_arr[1]);
  EntityWhere_valstr_arr=EntityWhere_valstr.split(",");
  //EntityWhere=EntityWhere_arr[0]+"="+EntityWhere_valstr_arr[0];
  BlockTitle=new String(Request.QueryString("BlockTitle"));
  FilterBlock=new String(Request.QueryString("Filter"));
  SelectSQL=new String(Request.QueryString("SelectSQL"));
  var Evalcode=new String(Request.Form("Evalcode"));  
  
  //workflow
  ShowNewWorkFlowButtons=new String(Request.QueryString("ShowNewWorkFlowButtons"));
  WorkFlowTable=new String(Request.QueryString("WorkFlowTable"));
  ShowWorkFlowButtons=new String(Request.QueryString("ShowWorkFlowButtons"));

  Container=eWare.GetBlock("Container");
  Container.DisplayForm=false;
  Container.DisplayButton(Button_Default) = false;
 // Response.Write(EntityWhere+"<br /><br />");

  if (ShowNewWorkFlowButtons=="True"){
    Container.ShowNewWorkFlowButtons=ShowNewWorkFlowButtons;
    Container.WorkFlowTable=WorkFlowTable;
    Container.ShowWorkFlowButtons=ShowWorkFlowButtons;
  }
  function _parsesagecrm_code_all(val)
  {
    if (val=="sagecrm_code_all")
	{
	  return "";
	}
	return val;
  }
  var filterSQL = new String("");
  if (Defined(FilterBlock)&& (FilterBlock!='')){
    FilterBlock=eWare.GetBlock(FilterBlock);
    eEntries = new Enumerator(FilterBlock);
    while (!eEntries.atEnd()) {
      y = eEntries.item();
      yName=new String(y.FieldName);
      if ((Request.Form(y.FieldName)!="") && (Defined(Request.Form(y.FieldName)))){
        //fix up some of the types..crm does not always set this up correctly
        if ( (yName.indexOf("_createddate")>1) || (yName.indexOf("_updateddate")>1) || (yName.indexOf("_timestamp")>1))
          y.EntryType=iEntryType_DateTime;
        if (yName.indexOf("_userid")>1)
          y.EntryType=iEntryType_UserSelect;
		//patch for crm not supplying correc data for crystal manager field - crre_ignoresecurity-crre_category
        //this fixes using the filter on a crm screen in 7.3
		if (y.FieldName=="crre_ignoresecurity")	
		{
			y.EntryType = iEntryType_CheckBox;
		}else if ((y.FieldName=="crre_category")||(y.FieldName=="crre_entity"))	
		{
			y.EntryType = iEntryType_Select;
		}			
		//  Response.Write(y.FieldName+"="+y.EntryType+"<br/>");
		if (y.EntryType == iEntryType_CheckBox)
        {
		    var _val=new String(Request.Form(y.FieldName));
			if (_val=="Checked")
			{
				if (filterSQL!="")
					filterSQL+= " AND ";
				filterSQL += " " + y.FieldName + " = 'Y' "; 
			}else if (_val=="NotChecked")
			{
				if (filterSQL!="")
					filterSQL+= " AND ";
				filterSQL += " " + y.FieldName + " is null "; 			
			}else{
			  val="";///do nothing with this field
			}
		}
		else	
		if (y.EntryType == iEntryType_Select) 
		{
		      var _val=_parsesagecrm_code_all(Request.Form(y.FieldName));
			  if (_val!="")
			  {
				  if (filterSQL!="")
					filterSQL+= " AND ";
				  filterSQL += " " + y.FieldName + " = '" + _val +"'";     
              }			  
		}else		
        if ((y.EntryType > iEntryType_Select) && !(((y.EntryType == iEntryType_DateTime) || (y.EntryType == iEntryType_Date)))) {   ///int
          if (filterSQL!="")
            filterSQL+= " AND ";
          filterSQL += " " + y.FieldName + " = " + Request.Form(y.FieldName);              
        }else
        if ((y.EntryType == iEntryType_DateTime) || (y.EntryType == iEntryType_Date)){   ///dates
          getDate = _getDateSQL(y.FieldName);
          if (filterSQL!="" && getDate != "")
            filterSQL+= " AND ";
          filterSQL += getDate;
        }else{  //assume text type
          if (filterSQL!="")
            filterSQL+= " AND ";
          filterSQL += " " + y.FieldName + " like '%" + _parsesagecrm_code_all(Request.Form(y.FieldName)) +"%'";      
        }
        
      }
      eEntries.moveNext();
    }    
  }
  
  function _getDateSQL(entry){
    var DateTimeModesproj_createddate="DateTimeModes"+entry;
    var _relative=entry+"_relative";
    var _relative_option=entry+"_relative_option";
    var _start=entry+"_start";
    var _end=entry+"_end";
    var _sqlwhere = new String("");
      
    if (Request.Form("DateTimeModes"+entry)=="Between")
    {
      _startDate=fixSQLDate(Request.Form(_start));
      _endDate=fixSQLDate(Request.Form(_end));
      if  (_endDate!="" && _startDate!="")
        return " (" + entry + " between '" + _startDate + "' and '" +  _endDate+"')";
      if  (_endDate=="" && _startDate!="")
        return " (" + entry + " > '" + _startDate + "')";
      if  (_endDate!="" && _startDate=="")
        return " (" + entry + " > '" + _endDate + "')";
    }
    else  //relative
    {
      _relativeValue = Request.Form(_relative);
      _relativeOption = Request.Form(_relative_option);
      if (_relativeOption=="Current"){
        ival = "0";
      }else
      if (_relativeOption=="Previous"){
        ival = "-1";
      }else
      if (_relativeOption=="Next"){
        ival = "1";
      }          
      if (_relativeValue=='Day'){      
        _sqlwhere=" (DATEPART(DY, "+entry+") = DATEPART(DY, DATEADD(DD, "+ival+", GETDATE())) "+
                  "AND DATEPART(YY, "+entry+") = DATEPART(YY, DATEADD(DD, "+ival+", GETDATE()))) ";
      }else
      if (_relativeValue=='Week'){
        _sqlwhere=" (DATEPART(WK, "+entry+") = DATEPART(WK, DATEADD(DD, 7 * "+ival+", GETDATE())) "+
                  "AND DATEPART(YY, "+entry+") = DATEPART(YY, DATEADD(DD, 7 * "+ival+", GETDATE())))";
      }else
      if (_relativeValue=='Month'){
        _sqlwhere=" (DATEPART(MM, "+entry+") = DATEPART(MM, DATEADD(MM, "+ival+", GETDATE())) "+
                  "AND DATEPART(YY, "+entry+") = DATEPART(YY, DATEADD(MM, "+ival+", GETDATE()))) ";
      }else
      if (_relativeValue=='Quarter'){
        _sqlwhere = " (DATEPART(QQ, "+entry+") = DATEPART(QQ, DATEADD(QQ, "+ival+", GETDATE())) "+
                    "AND DATEPART(YY, "+entry+") = DATEPART(YY, DATEADD(QQ, "+ival+", GETDATE()))) ";
      }else
      if (_relativeValue=='BIQuarter'){
        if (_relativeOption=="Current"){
           _sqlwhere=" ("+entry+" BETWEEN CAST(CAST(DATEPART(YY, DATEADD(QQ,  0, GETDATE())) AS CHAR)"+
                     " + '/' + CAST((DATEPART(QQ, DATEADD(QQ,  0, GETDATE())) * 3) - 2 AS CHAR) + '/01' AS DATETIME) "+
                     "AND DATEADD(QQ, 1, CAST(CAST(DATEPART(YY, DATEADD(QQ,  1, GETDATE())) AS CHAR) + '/' + "+
                     "CAST((DATEPART(QQ, DATEADD(QQ, 1, GETDATE())) * 3) - 2 AS CHAR) + '/01' AS DATETIME))) ";
        }else
        if (_relativeOption=="Previous"){
           _sqlwhere=" ("+entry+" BETWEEN CAST(CAST(DATEPART(YY, DATEADD(QQ, -2, GETDATE())) AS CHAR)"+
                     " + '/' + CAST((DATEPART(QQ, DATEADD(QQ, -2, GETDATE())) * 3) - 2 AS CHAR) + '/01' AS DATETIME) "+
                     "AND DATEADD(QQ, 1, CAST(CAST(DATEPART(YY, DATEADD(QQ, -1, GETDATE())) AS CHAR) + '/' + "+
                     "CAST((DATEPART(QQ, DATEADD(QQ, -1, GETDATE())) * 3) - 2 AS CHAR) + '/01' AS DATETIME))) ";
        }else
        if (_relativeOption=="Next"){
           _sqlwhere=" ("+entry+" BETWEEN CAST(CAST(DATEPART(YY, DATEADD(QQ,  1, GETDATE())) AS CHAR)"+
                     " + '/' + CAST((DATEPART(QQ, DATEADD(QQ,  1, GETDATE())) * 3) - 2 AS CHAR) + '/01' AS DATETIME) "+
                     "AND DATEADD(QQ, 1, CAST(CAST(DATEPART(YY, DATEADD(QQ,  2, GETDATE())) AS CHAR) + '/' + "+
                     "CAST((DATEPART(QQ, DATEADD(QQ, 2, GETDATE())) * 3) - 2 AS CHAR) + '/01' AS DATETIME))) ";
        }          
      }else
      if (_relativeValue=='Year'){
        _sqlwhere=" (DATEPART(YY, "+entry+") = DATEPART(YY, DATEADD(YY, "+ival+", GETDATE()))) ";
      } 
      return _sqlwhere;       
    }
    return "";
  }
  if ( (EntityWhere!="") && (filterSQL!="") )
    filterSQL=" AND "+filterSQL;

  //debug line
  //Response.Write("=="+filterSQL+"==");

  //get our listblock
  var block = eWare.GetBlock(ListBlock);

  //set the title if there is one
  if (Defined(BlockTitle))
    block.Title= BlockTitle;

  //hide the form
  block.DisplayForm=false;

  if (Defined(Evalcode))
  {
    Evalcode=unescape(Evalcode);
    eval(Evalcode);
  }
  
  EntityWhere+=filterSQL;
  //Response.Write(EntityWhere);
  if ( (!Defined(SelectSQL))|| (SelectSQL=="") ){
    //get our record object
    rs = eWare.FindRecord(EntityName, EntityWhere);
    //render the block
    Container.AddBlock(block);
    var xresponse=Container.Execute(rs);
  }else{
    var rs = eWare.CreateQueryObj(SelectSQL);  
    rs.SelectSql();
    block.ArgObj=rs;
    //render the block
    Container.AddBlock(block);
    var xresponse=Container.Execute();  
  }
  //return the content
  //Response.Write( "list" );
  Response.Write( xresponse );
 // Response.Write( "listend" );
  Response.End();
}catch(e){
  logerror(e);
  if ((e.number & 0xFFFF)==65535){
    Response.Write("<br /><b>SQL</b>="+EntityWhere+"<br />");
  }
}
%>
