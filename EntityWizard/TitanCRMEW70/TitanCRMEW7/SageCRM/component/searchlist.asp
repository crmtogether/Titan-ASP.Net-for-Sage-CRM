<!-- #include file ="SageCRM.js" -->
<%
//******************************************************************************
//******************************************************************************
/*
*  TITAN ASP.NET Suite for Sage CRM
*/
//******************************************************************************
try{
  var ListBlock=new String(Request.QueryString("ListBlock"));
  ListBlock=getZeroField(ListBlock);
  var EntityName=new String(Request.QueryString("EntityName"));
  EntityName=getZeroField(EntityName);
  var EntityWhere=new String(Request.QueryString("EntityWhere"));
  EntityWhere=getZeroField(EntityWhere);
  var BlockTitle=new String(Request.QueryString("BlockTitle"));
  BlockTitle=getZeroField(BlockTitle);
  var EntryBlock=new String(Request.QueryString("EntryBlockName"));
  EntryBlock=getZeroField(EntryBlock);

  Container=eWare.GetBlock("container");
  block = eWare.GetBlock(EntryBlock);
  List=eWare.GetBlock(ListBlock);
  content= eWare.GetBlock("Content");

  block.DisplayForm=false;
  block.DisplayButton(Button_Default) = false;
  Container.DisplayForm=false;
  Container.DisplayButton(Button_Default) = false;

  Container.AddBlock(block);
  content.contents="CUTFROMHERE";
  Container.AddBlock(content);
  Container.AddBlock(List);
  if (Request.Form("em")!=6){   //6 = clear
    //List.ArgObj=getFilterSQL();
    //Response.Write(getFilterSQL());
    //List.ArgObj=block;//commented this out as sometimes the filter was not used
    result=new String(Container.Execute(block));
    idx=result.indexOf("CUTFROMHERE");
    result=result.substring(idx+11,result.length);
    //PATCH up the html
    result="<table width=\"100%\" border=\"0\" ><tr><td>" +result;
    Response.Write(result);
  }

}catch(e){
  logerror(e);
}
function getFilterSQL(){
  var filterSQL = new String("");
  eEntries = new Enumerator(block);
  while (!eEntries.atEnd()) {
    y = eEntries.item();
    yName=new String(y.FieldName);
    if ((Request.Form(y.FieldName)!="") && (Defined(Request.Form(y.FieldName)))){
      //fix up some of the types..crm does not always set this up correctly
      if ( (yName.indexOf("_createddate")>1) || (yName.indexOf("_updateddate")>1) || (yName.indexOf("_timestamp")>1))
        y.EntryType=iEntryType_DateTime;
      if (yName.indexOf("_userid")>1)
        y.EntryType=iEntryType_UserSelect;
      if ((y.EntryType > iEntryType_Select) && !(((y.EntryType == iEntryType_DateTime) || (y.EntryType == iEntryType_Date)))) {   ///int
        if (filterSQL!="")
          filterSQL= " AND ";
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
        filterSQL += " " + y.FieldName + " like '" + Request.Form(y.FieldName) +"%'";      
      }
      
    }
    eEntries.moveNext();
  }    
  
  return filterSQL;
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


%>
