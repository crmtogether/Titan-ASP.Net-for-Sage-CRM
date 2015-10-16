<%
//******************************************************************************
//******************************************************************************
/*
*  Titan ASP.Net for Sage CRM
*/
//******************************************************************************
try{

var EntityName=Request.QueryString("EntityName");
var EntryBlockName=Request.QueryString("EntryBlockName");
var icon=Request.QueryString("icon");

  var EntityWhere=new String(Request.QueryString("EntityWhere"));
  EntityWhere_arr=EntityWhere.split("=");
  EntityWhere_valstr=new String(EntityWhere_arr[1]);
  EntityWhere_valstr_arr=EntityWhere_valstr.split(",");
  EntityWhere=EntityWhere_arr[0]+"="+EntityWhere_valstr_arr[0];

var user_language=eWare.GetContextInfo("user","user_language");
if (user_language=="")
{
  user_language="US";
}

//changed findrecord to queryobj so we can order the columns
//var brec=eWare.FindRecord("custom_screens","seap_searchboxname='"+EntryBlockName+"' and seap_deviceid is null");

var brec=eWare.CreateQueryObj("select * from custom_screens where seap_searchboxname='"+EntryBlockName+"' and seap_deviceid is null order by seap_order");
brec.SelectSQL();

var datarec=eWare.FindRecord(EntityName, EntityWhere);

%>
<DIV ID="SageCRMTopContentData" >
<HTML>
  <HEAD>
    <META http-equiv="Content-Type" content="text/html; charset=utf-8">
<TITLE>
      CRM
    </TITLE>
<% if (getCRMVersion()=="6") { %>
    <LINK REL="stylesheet" HREF="/<%=sInstallName%>/eware.css">
    <%} else {%>
    <LINK REL="stylesheet" HREF="/<%=sInstallName%>/color1.css">
    <% } %>
</HEAD>
  <BODY CLASS=TOPBODY VLINK=#003B72 LINK=#003B72 LEFTMARGIN=5 TOPMARGIN=0>
    <TABLE CELLPADDING=0 CELLSPACING=0 BORDER=0 HEIGHT=99% WIDTH=100%>
      <TR>
        <TD ALIGN=LEFT WIDTH=50>
          <IMG SRC="/<%=sInstallName%>/Themes/img/color/Icons/<%=icon%>" HSPACE=0 BORDER=0 ALIGN=TOP>
        </TD>
        <TD ALIGN=LEFT VALIGN=MIDDLE>
          <TABLE BORDER=0>
          <%
          rowcount=0;
          while(!brec.eof){
          %>
            <%
            if ((rowcount==0) || (brec("seap_newline")=="1")){
            %>
              <TR>
            <%
            }
            %>
              <TD CLASS=TOPCAPTION>
                <%=eWare.GetTrans("colnames",brec("seap_colname")) %>:
                <SPAN CLASS=TOPHEADING>
                </SPAN>
              </TD>
              <TD CLASS=TOPSUBHEADING ALIGN=LEFT>
                <%=getLookup(brec("seap_colname"),datarec(brec("seap_colname"))) %>
              </TD>
            <%
            rowcount++;
            %>
            <%
              brec.NextRecord();
              if ((brec.eof) || ((rowcount==0) || (brec("seap_newline")=="1"))){
              %>
                </TR>
              <%
              }
            }
            %>
          </TABLE>
      </TR>
    </TABLE>
  </BODY>
</HTML>
</DIV>

<%

function getLookup(colname, code)
{
  res=new String(code);
  var lrec=eWare.CreateQueryObj("select colp_entrytype, colp_lookupfamily from custom_edits where colp_colname='"+colname+"'");
  lrec.SelectSQL();
  if (lrec.eof)
  {
    return code;
  }
  fam=new String(lrec("colp_lookupfamily"));
  fam=fam.toLowerCase();
  colp_entrytype=lrec("colp_entrytype");
  //Response.Write(colname+"="+colp_entrytype);
  if (colp_entrytype=="10") //choices
  {
    res=code;
  }
  else
  if (colp_entrytype=="21") //choices
  {
     var whereclause="capt_family='"+colname+"' and capt_familytype='Choices' and capt_code='"+code+"'";
     var capq=eWare.FindRecord("custom_captions" ,whereclause);
     if (!capq.eof){
       res=capq("capt_"+user_language);   
     }
  }else
  if (colp_entrytype=="22")  //user
  {
    if ((!Defined(code)) || (code==""))
      return code;  
    res=getUserName(code);
  }else
  if (colp_entrytype=="56")  //entity
  {
    var erec=getEntityRecord(fam,code);
    res=getTableDescField(fam, erec);
  }else
  if (colp_entrytype=="53")  //territory
  {
     res=getTerrCaption(code); 
  }  
  
  if (!Defined(res))
    res="";
  return res;
}

}catch(e){
  logerror(e);
}
%>