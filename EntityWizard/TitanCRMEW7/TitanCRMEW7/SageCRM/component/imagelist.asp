<%@ CodePage=65001 Language=JavaScript%>
<%

//******************************************************************************
//******************************************************************************
/*
*  Sage CRM .NET Component Suite
*  Returns comma delimited list of button images
*  Version 2.1.7
*/
//******************************************************************************
if (Request.ServerVariables("HTTPS")=="off"){
  protocall="http://";
}else{
  protocall="https://";
}

function ShowFolderFileList(folderspec)
{
   var fso, f, f1, fc, s;
   fso = new ActiveXObject("Scripting.FileSystemObject");
   f = fso.GetFolder(folderspec);
   fc = new Enumerator(f.files);
   s = "";
   for (; !fc.atEnd(); fc.moveNext())
   {
      //s += protocall+Request.ServerVariables("SERVER_NAME")+"/"+sInstallName+"/Img/Buttons/"+fc.item().name;
      s+=fc.item().name;      
      s += ",";
   }
   return(s);
}

var imagePath=Request.ServerVariables("APPL_PHYSICAL_PATH")+"\\Img\\Buttons"
res=ShowFolderFileList(imagePath);
Response.Write(res);    
     
%>