<%  

  var record=eWare.CreateQueryObj(SQL);
  record.SelectSQL();
  iTopCount=0;
  iRowCount=0;  
  while (!record.eof)
  {
      iRowCount++;
      if  ( (iFrom==-1) || (iFrom<iRowCount) )
      {
        if ( (Top==-1) || (Top==0) ||(iTopCount<Top) ){
          eQueryFields = new Enumerator(record);
          result+="<datatable>";
          icol=0;
          while (!eQueryFields.atEnd()) {
            icol++;
            var fieldx=eQueryFields.item();
            fieldx=fieldx.toLowerCase();
            fieldx=fieldx.replace(/\s/g, "");     
			try{			
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
				result+="<"+escape(fieldx)+">"+(CustomEscape(fieldval))+"</"+escape(fieldx)+">";
			}catch(errField){
			  //carry on?
			  result+="<"+escape(fieldx)+">ERROR with field data</"+escape(fieldx)+">";
			}
            eQueryFields.moveNext();
          }
          result+="</datatable>";
        }            
        iTopCount++;        
      }
      record.Next();
  }
%>