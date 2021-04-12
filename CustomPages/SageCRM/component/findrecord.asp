<!-- #include file ="SageCRM.js" -->
<%
//******************************************************************************
//******************************************************************************
/*
*  TITAN ASP.NET for Sage CRM
*  example test url
*  http://localhost/crm71/custompages/sagecrm/component/findrecord.asp?SID=56295243957079&TableName=company,vcompanype&WhereClause=comp_companyid%3C50
*/
//******************************************************************************
//try{
Response.clear();
  var TableName=new String(Request.QueryString('TableName'));
  var WhereClause=new String(Request.QueryString('WhereClause'));
  var OrderBy=new String(Request.QueryString('OrderBy'));
  //Top is used by data aware controls
  //how many records to display in a data enabled control
  var Top=new Number(Request.QueryString('Top'));
  //iTop is used when coding
  var iTop=new Number(Request.QueryString('iTop'));
  //where to display data from.
  var iFrom=new Number(Request.QueryString('iFrom'));
  
  //only return certain columns.
  var columnList=new String(Request.Form('columnList'));
  //translate return columns
  var translate=new String(Request.Form('translate'));  
  //translate='Y';//REMOVE CODE TO TEST ONLY
    //test line only
  //columnList="comp_companyid,comp_name";
  
  //accomodate 7.1 changes
    var tablearr=TableName.split(",");
    var coreTable=tablearr[0];
    if (tablearr.length>1)
    {
        coreTable=tablearr[1];
    }  
/*  
//s  test code
  
  TableName="Company";
 // iFrom=10;
  iTop=1;
  WhereClause="comp_companyid<100";
  */  
  debugcrm("findrecord_qs", Request.QueryString);
  if (!Defined(WhereClause)){
    WhereClause="";
  }
  if ((iTop!="") && (Defined(iTop)))
  {
    Top=iTop;
  }  
  if (isNaN(Top)){
    Top=-1;
  }
  if (isNaN(iFrom)){
    iFrom=-1;
  }

  var fieldarr=new Array();
  var fieldtype=new Array();
  var result="<?xml version=\"1.0\" standalone=\"yes\"?>";
  result+="<data>";
  
  var record=eWare.FindRecord(TableName,WhereClause);
  if (OrderBy+""!="undefined")
  {
    record.OrderBy=OrderBy;
  }
  var _itemstoselect="*";
  
  if ((columnList!=null)&&(columnList+""!="undefined")&&(columnList!=""))
	{
		_itemstoselect=columnList;
	}
	
  try
  {
    var fieldrec = eWare.CreateQueryObj("select top 1 "+_itemstoselect+" from "+coreTable);    
    fieldrec.SelectSQL();
    eQueryFields = new Enumerator(fieldrec);
    while (!eQueryFields.atEnd()) {
      var fieldx=new String(eQueryFields.item());
      fieldx=fieldx.toLowerCase();
      fieldarr[fieldarr.length]=fieldx;
	  etype=new String("10");
	  if (translate=='Y')
	  {
		var fieldrecX = eWare.CreateQueryObj("select top 1 colp_colname, colp_entrytype from custom_edits where colp_colname='"+fieldx+"' ORDER BY ColP_ColPropsId");
		fieldrecX.SelectSQL();
		if (!fieldrecX.eof){
			etype=new String(fieldrecX("colp_entrytype"));
		}
	  }
      fieldtype[fieldarr.length]=etype.substr(0,2);
      eQueryFields.moveNext();
    }  
  }catch(e)
  {   //for external tables and views we use the metadata in crm
    var fieldrec = eWare.CreateQueryObj("select colp_colname, colp_entrytype from custom_edits where ColP_Entity = '"+tablearr[0]+ "'"+
                     " and ((colp_system is null) or (colp_system='')) ORDER BY ColP_ColPropsId");
    fieldrec.SelectSQL();
    while (!fieldrec.eof){
      fieldarr[fieldarr.length]=fieldrec("colp_colname");
      etype=new String(fieldrec("colp_entrytype"));
      fieldtype[fieldarr.length]=etype.substr(0,2);
      fieldrec.NextRecord();
    } 
  }
  iTopCount=0;
  iRowCount=0;
  while (!record.eof)
  {
      iRowCount++;
      if  ( (iFrom==-1) || (iFrom<iRowCount) )
      {
        if ( (Top==-1) || (Top==0) ||(iTopCount<Top) ){
          result+="<"+tablearr[0]+">";
          result+="<"+record.idfield+">"+record[record.idfield]+"</"+record.idfield+">";
          for(i=0;i<fieldarr.length;i++){
		    var fieldName=new String(fieldarr[i]);
		   // Response.Write('<br>'+fieldarr[i]);
			//Response.Flush();
			try{
				fieldval=record[fieldarr[i]];
				if (!Defined(record[fieldarr[i]])){
				  fieldval="";
				  if (i==0)  //fix for when the data in the first column is blank...breaks xml 
					fieldval="_";
				}
				fieldName=fieldName.replace(/\s/g, '');    
				if (record.idfield!=fieldarr[i]){
					var _fval=CustomEscape(fieldval);
					//get translations if set
					if (fieldtype[i+1]=="21")
					{
					  _fval=CRM.GetTrans(fieldName,fieldval);
					  _fval=CustomEscape(_fval);
					}else
					if (fieldtype[i+1]=="53")
					{
						//territory
						var terr_sql="select Terr_Caption from Territories where Terr_TerritoryID='"+fieldval+"'";
						var terr_sqlrec = eWare.CreateQueryObj(terr_sql);    
						terr_sqlrec.SelectSQL();
						if (!terr_sqlrec.eof){
							 _fval=CustomEscape(terr_sqlrec("Terr_Caption"));
						}
					}
					result+="<"+fieldName+">"+_fval+"</"+fieldName+">";
				}
			}catch(errField){
			  //carry on?
			  result+="<"+fieldName+">ERROR with field data</"+fieldName+">";
			}
			
          }
          result+="</"+tablearr[0]+">";
        }
        iTopCount++;
      }
      record.NextRecord();
  }
  //add the total row count info now
  result+="<crmtogethertrc>";
  result+="<crmtogethertotal>"+iRowCount;  
  result+="</crmtogethertotal>";    
  result+="</crmtogethertrc>";
  result+="</data>";
  Response.Write(result);
  debugcrm("findrecord", result);
  Response.End();
//}catch(e){
  //logerror(e);//
//}

function _getuser(uid){
  if (((Defined(uid)) && (uid!='')) && (!isNaN(uid))) {
    var urec=eWare.FindRecord("Users","user_userid="+uid);
    return urec("user_firstname")+" "+urec("user_lastname");
  }
  return '';
}
%>