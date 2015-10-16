<%  
  var ado_connection=Server.CreateObject("ADODB.Connection");
  var record = Server.CreateObject("ADODB.Recordset");

  ado_connection.Open(ado_ConnectionString);
    
  SQL=unescape(SQL);  //required for ado
    
  record.Open(SQL , ado_connection, adOpenForwardOnly, adLockReadOnly, adCmdText);
  iTopCount=0;
  iRowCount=0;  
  iFrom=0;
  icol=0;
  Top=-1;
  result+="[";

  while (!record.EOF)
  {
      iRowCount++;
      if  ( (iFrom==-1) || (iFrom<iRowCount) )
      {
        if ( (Top==-1) || (Top==0) ||(iTopCount<Top) ){
            eQueryFields = new Enumerator(record.Fields);
            if (iRowCount>1)
            {
              result+=",{";
            }else{
              result+="{";            
            }
          
          icol=0;
          while (!eQueryFields.atEnd()) {
            if (icol>0)
            {
              result+=",";
            }          
            var fieldval=new String(eQueryFields.item());
            var fieldx=new String("");
            fieldx=record.Fields(icol).Name;          
            //fieldx=fieldx.trim();
            fieldx=fieldx.toLowerCase();
            fieldx=fieldx.replace(/\s/g, "");        
            if (!Defined(fieldval)){
              fieldval="";
            }
            if (fieldval=="")
              fieldval=" ";
            if (fieldx=="")
            {
              fieldx="nocolumnname"+icol;
            }
            fieldval=fieldval.rtrim();
            result+="\""+escape(fieldx)+"\":\""+(CustomEscape(fieldval))+"\"";
            eQueryFields.moveNext();
            icol++;
          }
          result+="}";
        }            
        iTopCount++; 
      }
      record.MoveNext();
  }  
  result+="]";
  
  record.Close();
  record=null;
  ado_connection.Close();
%>