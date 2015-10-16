<!-- #include file ="..\crmwizard.js" -->

<%

var Now=new Date();
if (eWare.Mode<1) eWare.Mode=1;


List=eWare.GetBlock("**&customGridName&**");

matchRulesRS = eWare.FindRecord("matchrules","MaRu_TableName=N'**&EntityName&**'");
bFirst=true;
sWhere="";
sPOST="";
while( !matchRulesRS.eof )
{
  sVal = Request.Form(matchRulesRS("MaRu_FieldName"));
  sField = matchRulesRS("MaRu_FieldName");
  if( Defined(sVal) && (sVal != '') )
  {
    sPOST = sPOST + "&fieldname" + "=" + sField;
    sPOST = sPOST + "&fieldval" + "=" + sVal;
    if( !bFirst )
    {
      sWhere = sWhere + " and ";
    }
    else bFirst = false;

    type = matchRulesRS("MaRu_MatchType");
    if( type.toUpperCase() == 'CONTAINS')
      sWhere=sWhere+sField+" like N'%"+sVal+"%'";
    else if( type.toUpperCase() == 'DOESNTMATCH')
      sWhere=sWhere+sField+" <> N'"+sVal + "'";
    else if( type.toUpperCase() == 'EXACT')
      sWhere=sWhere+sField+" = N'"+sVal + "'";
    else if( type.toUpperCase() == 'PHONETIC')
      sWhere=sWhere+"SUBSTRING(SOUNDEX(" + sField + "), 2, 3) = SUBSTRING(SOUNDEX('" + sVal + "'), 2, 3)";
    else if( type.toUpperCase() == 'STARTINGWITH')
      sWhere=sWhere+sField+" like N'"+ sVal + "%'";
  }
  matchRulesRS.NextRecord();
}

context=Request.QueryString("context");

RS=eWare.FindRecord("**&EntityName&**", sWhere);
if( (sWhere == '') || (RS.eof) )
  Response.Redirect(eWare.URL("**&newASP&**") + sPOST + "&E=**&EntityName&**&context="+context);

List.ArgObj=sWhere;

container=eWare.GetBlock("container");
container.AddBlock(List);
container.ButtonImage="nextcircle.gif";
container.ButtonTitle=eWare.GetTrans('GenCaptions','DedupeIgnore**&EntityName&**');

container.AddButton(eWare.Button(eWare.GetTrans('GenCaptions','DedupeBack**&EntityName&**'), "prevcircle.gif", eWare.URL("**&dedupeASP&**") + "&E=**&EntityName&**&context="+context));
container.FormAction=eWare.URL("**&newASP&**") + sPOST + "&E=**&EntityName&**&context=" + context;

eWare.AddContent('<table class=INFOCONTENT><td CLASS=INFOCONTENT><b>');
eWare.AddContent(eWare.GetTrans('GenCaptions','Dedupe**&EntityName&**'));
eWare.AddContent('</b></td></table>');

eWare.AddContent(container.Execute());
Response.Write(eWare.GetPage());
%>