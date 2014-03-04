<!-- #include file ="SageCRM.js" -->
<%
//******************************************************************************
//******************************************************************************
/*
*  TITAN ASP.NET Suite for Sage CRM
*/
//******************************************************************************

if (!Array.prototype.indexOf)
{
  Array.prototype.indexOf = function(elt /*, from*/)
  {
    var len = this.length;

    var from = Number(arguments[1]) || 0;
    from = (from < 0)
         ? Math.ceil(from)
         : Math.floor(from);
    if (from < 0)
      from += len;

    for (; from < len; from++)
    {
      if (from in this &&
          this[from] === elt)
        return from;
    }
    return -1;
  };
}

function recentObject()
{

  this.Entity=new String();
  this.EntityId=new String();
  this.Action=new String();
  this.EntityKey=new String();
  this.Description=new String();

  
}

function recentListObject()
{
  this.UserId=new String();
  this.objList=new Array();
  
  //methods
  this.updateRecentList=_updateRecentList;
  this.readRecentList=_readRecentList;
  this.unreadRecentList=_unreadRecentList;
  this.findRecentObject=_findRecentObject;
  this.sortRecentList=_sortRecentList;
  this.moveToTopOfList=_moveToTopOfList;
}

function _moveToTopOfList(obj)
{
  var newList=new Array();
  var added=false;
  var found=false;
  var ent="";
  //check if its in the list
  for (var i=0;i<this.objList.length;i++)
  {
    _robj=this.objList[i];
    if ( (obj.Entity==_robj.Entity) && (obj.EntityId==_robj.EntityId) )  //found our object
    {
      found=true;
      break;
    }
  }
  newList[0]=obj;    
  if (found)   
  {
      for (var j=0;j<this.objList.length;j++)//add the remaining objects
      {
         _robj=this.objList[j];
         if (!((_robj.Entity==obj.Entity) && (_robj.EntityId==obj.EntityId)))  //do not add to list if found as its not on the top   
         {
            newList[newList.length]=_robj;
         }
      }
  }else{
      for (var j=0;j<this.objList.length;j++)  //add all objects
      {
         _robj=this.objList[j];
         newList[newList.length]=_robj;
      }  
  }
  this.objList=newList;
}

function _updateRecentList(Entity,EntityId,Action,Key, description)
{
  this.readRecentList();
  //check if the object exists
  var _robj=this.findRecentObject(Entity,EntityId);
  if (_robj!=null) //found one
  {
     //add to the top of the list
     this.moveToTopOfList(_robj);
  }else
  {
    //create and add to the top of the list
     _robj=new recentObject();
     _robj.Entity=Entity;
     _robj.EntityId=EntityId;
     _robj.Action=Action;
     _robj.EntityKey=Key;
     _robj.Description=description;     
     this.moveToTopOfList(_robj); //add to the top of the list
  }
  this.sortRecentList();
  //write back to the database
  var newrecent=this.unreadRecentList();
  flosh("<br />====<br />"+newrecent);
  rec=eWare.FindRecord("users","user_userid="+this.UserId);
  rec("User_RecentList")=newrecent;
  rec.SaveChanges();
}
function _sortRecentList()
{
    var entityarr=new Array();
    var res=new Array();
    for (var i=0;i<this.objList.length;i++)
    {
      obj=this.objList[i];
      flosh("obj.Entity="+obj.EntityId);
      if (entityarr.indexOf(obj.Entity)==-1)
        entityarr[entityarr.length]=obj.Entity;
        
    }
    entityarr.sort();
    for (var j=0;j<entityarr.length;j++)
    {
      for (var x=0;x<this.objList.length;x++)
      {
        obj=this.objList[x];
        if (obj.Entity==entityarr[j])
          res[res.length]=obj;
      }
    }
    this.objList=res;
}
function _unreadRecentList()
{
  this.sortRecentList();

  var res=new String("");
  var header=new String("");
  var desclist=new String("");
  var datalist=new String("");
  var ent="";

  for (var i=0;i<this.objList.length;i++)
  {
  
    var _obj=this.objList[i];
    //build header
    if (ent!=_obj.Entity)
    {
      if (res!="")
        res+=desclist+""+datalist;
      desclist="";
      datalist="";
      if (res!="")
        res+="";
      res += _obj.Entity+"";
      ent=_obj.Entity;
      //flosh("ent="+ent+"##entity="+_obj.Entity);

    }else
    {
    }
    if (desclist!="")
      desclist+=","
    desclist+= _obj.Description;
    if (datalist!="")
      datalist+=","
    datalist+= _obj.Action+"X"+_obj.EntityKey+"X"+_obj.EntityId;

    
  }
  //catch the last item
  res+=desclist+""+datalist;
  
  return res;
}

function _findRecentObject(Entity,EntityId)
{
  for(var i=0;i<this.objList.length;i++)
  {
    var robj=this.objList[i];
    if ( (Entity==robj.Entity) && (EntityId==robj.EntityId))
    {
      return robj; //found so return it.
    }
  }
  return null;
}

function _readRecentList()
{
  rec=eWare.FindRecord("users","user_userid="+this.UserId);
  while (!rec.eof)
  {
    var User_RecentList=new String(rec("User_RecentList"));
    if ( (User_RecentList!="") && (Defined(User_RecentList)))
    {
      var ur_arr=User_RecentList.split("");//delimited list
      for(var _rcount=0;_rcount<ur_arr.length;_rcount++)
      {      
        var Entity=ur_arr[_rcount]
        //description
        _rcount++;
        var desc=new String(ur_arr[_rcount]); //e.g 5-10055:Server memory allocation error
        var desc_arr=desc.split(","); //comma delimited list
        //data
        _rcount++;
        var data_line_item=new String(ur_arr[_rcount]); //e.g 5-10055:Server memory allocation error
        var data_line_item_arr=desc.split(","); //comma delimited list
  
        for (var di=0;di<desc_arr.length;di++)
        {
          var r_obj=new recentObject();
          this.objList[this.objList.length]=r_obj; 
          r_obj.Entity=Entity;  //first bit with the entity
          flosh("<br /><br />Entity="+r_obj.Entity);
  
          r_obj.Description=desc_arr[di];
          flosh("Desc="+r_obj.Description);
          
          //_rcount++;
          flosh("data="+data_line_item);
          var data_line_item_arr=data_line_item.split(","); //comma delimited list
          
          var dataX=new String(data_line_item_arr[di]);
          var dataX_arr=dataX.split("X"); //X delimited list
          r_obj.Action=dataX_arr[0];
          flosh("Action="+r_obj.Action);
          r_obj.EntityKey=dataX_arr[1];
          flosh("EntityKey="+r_obj.EntityKey);        
          r_obj.EntityId=dataX_arr[2];   
          flosh("EntityId="+r_obj.EntityId);
        }      
      }
    }
    rec.NextRecord();//should only be one record but just in case...
  }
}

function flosh(datastr)
{
  if (true)
  {
    Response.Write(datastr+"<br />");
    Response.Flush();
  }
}
try
{
  var UserId=Request.QueryString('Uid');
  var Entity=Request.QueryString('Entity');
  var EntityId=Request.QueryString('EntityId');
  var SummaryAction=Request.QueryString('Action');
  var EntityKey=Request.QueryString('EntityKey');
  var Description=Request.QueryString('Description');
  
  var RLObj=new recentListObject();
  //test line
  //UserId=1;
  RLObj.UserId=UserId;
  RLObj.updateRecentList(Entity,EntityId,SummaryAction,EntityKey, "\""+Description+"\"");
  //sample lines for testing
  //RLObj.updateRecentList("Case","7","281","8", "\"ref789\"");
  //RLObj.updateRecentList("Company","1020","200","1", "\"another compa\"");
  
}catch(e){
  logerror(e);
}
%>
