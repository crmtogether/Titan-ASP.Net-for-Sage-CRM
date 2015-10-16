<%  
  var record=eWare.CreateQueryObj(SQL);
  record.SelectSQL();
  iTopCount=0;
  iRowCount=0;  
  iFrom=0;
  icol=0;
  Top=-1;
  result+="[";  
  while (!record.eof)
  {
      iRowCount++;
      if  ( (iFrom==-1) || (iFrom<iRowCount) )
      {
        if ( (Top==-1) || (Top==0) ||(iTopCount<Top) ){
          eQueryFields = new Enumerator(record);
          if (iRowCount>1)
          {
            result+=",{";
          }else{
            result+="{";            
          }          
          //result+="<datatable>";
          icol=0;
          while (!eQueryFields.atEnd()) {
            if (icol>0)
            {
              result+=",";
            }            
            var fieldx=eQueryFields.item();
            fieldx=fieldx.toLowerCase();
            fieldx=fieldx.replace(/\s/g, "");        
            fieldval=record.FieldValue(fieldx);
            if (!Defined(fieldval)){
              fieldval="";
            }
            if (fieldval=="")
              fieldval="";
            if (fieldx=="")
            {
              fieldx="nocolumnname"+icol;
            }
            //result+="<"+escape(fieldx)+">"+(CustomEscape(fieldval))+"</"+escape(fieldx)+">";
            result+="\""+escape(fieldx)+"\":\""+(CustomEscape(fieldval))+"\"";
            eQueryFields.moveNext();
            icol++;
          }
          result+="}";          
          //result+="</datatable>";
        }            
        iTopCount++;        
      }
      record.Next();
  }
  result+="]";  
%>