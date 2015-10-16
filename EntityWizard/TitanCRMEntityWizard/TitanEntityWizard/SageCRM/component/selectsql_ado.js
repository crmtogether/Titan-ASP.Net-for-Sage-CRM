<%  
  var ado_connection=Server.CreateObject("ADODB.Connection");
  var record = Server.CreateObject("ADODB.Recordset");

  ado_connection.Open(ado_ConnectionString);
    
  SQL=unescape(SQL);  //required for ado
  record.Open(SQL , ado_connection, adOpenForwardOnly, adLockReadOnly, adCmdText);

  iTopCount=0;
  iRowCount=0;  
  while (!record.EOF)
  {
      iRowCount++;
      if  ( (iFrom==-1) || (iFrom<iRowCount) )
      {
        if ( (Top==-1) || (Top==0) ||(iTopCount<Top) ){
          eQueryFields = new Enumerator(record.Fields);
          result+="<datatable>";
          icol=0;
          while (!eQueryFields.atEnd()) {
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
            result+="<"+escape(fieldx)+">"+(CustomEscape(fieldval))+"</"+escape(fieldx)+">";
            eQueryFields.moveNext();
            icol++;
          }
          result+="</datatable>";
        }            
        iTopCount++; 
      }
      record.MoveNext();
      
  }
  
  record.Close();
  record=null;
  ado_connection.Close();
  
%>