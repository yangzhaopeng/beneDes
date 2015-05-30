//
//
//	Copyright?2005. FarPoint Technologies.	All rights reserved.
//
var the_fpSpread = new Fpoint_FPSpread();
function FpSpread_EventHandlers(){
var e3=the_fpSpread;
this.TranslateKey=function (event){
e3.TranslateKey(event);
}
this.SetActiveSpread=function (event){
e3.SetActiveSpread(event);
}
this.MouseDown=function (event){
e3.MouseDown(event);
}
this.MouseUp=function (event){
e3.MouseUp(event);
}
this.MouseMove=function (event){
e3.MouseMove(event);
}
this.DblClick=function (event){
e3.DblClick(event);
}
this.HandleFirstKey=function (event){
e3.HandleFirstKey(event);
}
this.DoPropertyChange=function (event){
e3.DoPropertyChange(event);
}
this.CmdbarMouseOver=function (event){
e3.CmdbarMouseOver(event);
}
this.CmdbarMouseOut=function (event){
e3.CmdbarMouseOut(event);
}
this.ScrollViewport=function (event){
e3.ScrollViewport(event);
}
this.Focus=function (event){
var e4=event.target;
e3.Focus(e4);
}
e3.AttachEvent(document,"keydown",this.TranslateKey,true);
e3.AttachEvent(document,"mousedown",this.SetActiveSpread,false);
e3.AttachEvent(document,"keyup",this.HandleFirstKey,true);
e3.AttachEvent(window,"resize",e3.DoResize,false);
this.AttachEvents=function (e4){
e3.AttachEvent(e4,"mousedown",this.MouseDown,false);
e3.AttachEvent(e4,"mouseup",this.MouseUp,false);
e3.AttachEvent(document,"mouseup",this.MouseUp,false);
e3.AttachEvent(e4,"mousemove",this.MouseMove,false);
e3.AttachEvent(e4,"dblclick",this.DblClick,false);
e3.AttachEvent(e4,"focus",this.Focus,false);
var e5=e3.GetViewport(e4);
if (e5!=null){
e3.AttachEvent(e3.GetViewport(e4).parentNode,"DOMAttrModified",this.DoPropertyChange,true);
e3.AttachEvent(e3.GetViewport(e4).parentNode,"scroll",this.ScrollViewport);
}
var e6=e3.GetCommandBar(e4);
if (e6!=null){
e3.AttachEvent(e6,"mouseover",this.CmdbarMouseOver,false);
e3.AttachEvent(e6,"mouseout",this.CmdbarMouseOut,false);
}
}
this.DetachEvents=function (e4){
e3.DetachEvent(e4,"mousedown",this.MouseDown,false);
e3.DetachEvent(e4,"mouseup",this.MouseUp,false);
e3.DetachEvent(document,"mouseup",this.MouseUp,false);
e3.DetachEvent(e4,"mousemove",this.MouseMove,false);
e3.DetachEvent(e4,"dblclick",this.DblClick,false);
e3.DetachEvent(e4,"focus",this.Focus,false);
var e5=e3.GetViewport(e4);
if (e5!=null){
e3.DetachEvent(e3.GetViewport(e4).parentNode,"DOMAttrModified",this.DoPropertyChange,true);
e3.DetachEvent(e3.GetViewport(e4).parentNode,"scroll",this.ScrollViewport);
}
var e6=e3.GetCommandBar(e4);
if (e6!=null){
e3.DetachEvent(e6,"mouseover",this.CmdbarMouseOver,false);
e3.DetachEvent(e6,"mouseout",this.CmdbarMouseOut,false);
}
}
}
function Fpoint_FPSpread(){
this.a6=false;
this.a7=false;
this.a8=null;
this.a9=null;
this.b0=null;
this.b1=-1;
this.b2=null;
this.b3=null;
this.b4=null;
this.b5=null;
this.b6=-1;
this.b7=-1;
this.b8=null;
this.b9=null;
this.c0=new Array();
this.error=false;
this.left=37;
this.right=39;
this.up=38;
this.down=40;
this.tab=9;
this.enter=13;
this.shift=16;
this.space=32;
this.altkey=18;
this.home=36;
this.end=35;
this.pup=33;
this.pdn=34;
this.backspace=8;
this.InitFields=function (e4){
if (this.b3==null)
this.b3=new this.Margin();
e4.c8=null;
e4.groupBar=null;
e4.c9=null;
e4.d0=null;
e4.d1=null;
e4.d2=null;
e4.d3=null;
e4.d4=null;
e4.d5=null;
e4.d6=null;
e4.d7="";
e4.d8=null;
e4.e2=false;
e4.slideLeft=0;
e4.slideRight=0;
e4.setAttribute("rowCount",0);
e4.setAttribute("colCount",0);
e4.d9=new Array();
e4.e0=new Array();
e4.e1=new Array();
e4.footerSpanCells=new Array();
this.activePager=null;
this.dragSlideBar=false;
e4.allowColMove=(e4.getAttribute("colMove")=="true");
e4.allowGroup=(e4.getAttribute("allowGroup")=="true");
e4.selectedCols=[];
e4.msgList=new Array();
e4.mouseY=null;
}
this.RegisterSpread=function (e4){
var e7=this.GetTopSpread(e4);
if (e7!=e4)return ;
if (this.spreads==null){
this.spreads=new Array();
}
var e8=this.spreads.length;
for (var e9=0;e9<e8;e9++){
if (this.spreads[e9]==e4)return ;
}
this.spreads.length=e8+1;
this.spreads[e8]=e4;
}
this.Init=function (e4){
if (e4==null)alert("spread is not defined!");
e4.initialized=false;
this.b2=null;
this.c0=new Array();
this.RegisterSpread(e4);
this.InitFields(e4);
this.InitMethods(e4);
e4.c1=document.getElementById(e4.id+"_XMLDATA");
if (e4.c1==null){
e4.c1=document.createElement("XML");
e4.c1.id=e4.id+"_XMLDATA";
e4.c1.style.display="none";
document.body.insertBefore(e4.c1,null);
}
var f0=document.getElementById(e4.id+"_data");
if (f0!=null&&f0.getAttribute("data")!=null){
e4.c1.innerHTML=f0.getAttribute("data");
f0.value="";
}
this.SaveData(e4);
e4.c2=document.getElementById(e4.id+"_viewport");
if (e4.c2!=null){
e4.c3=e4.c2.parentNode;
}
e4.c4=document.getElementById(e4.id+"_corner");
if (e4.c4!=null&&e4.c4.childNodes.length>0){
e4.c4=e4.c4.getElementsByTagName("TABLE")[0];
}
e4.c5=document.getElementById(e4.id+"_rowHeader");
if (e4.c5!=null)e4.c5=e4.c5.getElementsByTagName("TABLE")[0];
e4.c6=document.getElementById(e4.id+"_colHeader");
if (e4.c6!=null)e4.c6=e4.c6.getElementsByTagName("TABLE")[0];
e4.colFooter=document.getElementById(e4.id+"_colFooter");
if (e4.colFooter!=null)e4.colFooter=e4.colFooter.getElementsByTagName("TABLE")[0];
e4.footerCorner=document.getElementById(e4.id+"_footerCorner");
if (e4.footerCorner!=null&&e4.footerCorner.childNodes.length>0){
e4.footerCorner=e4.footerCorner.getElementsByTagName("TABLE")[0];
}
var c7=e4.c7=document.getElementById(e4.id+"_commandBar");
var f1=this.GetViewport(e4);
if (f1!=null){
e4.setAttribute("rowCount",f1.rows.length);
if (f1.rows.length==1)e4.setAttribute("rowCount",0);
e4.setAttribute("colCount",f1.getAttribute("cols"));
}
var d9=e4.d9;
var e1=e4.e1;
var e0=e4.e0;
var f2=e4.footerSpanCells;
this.InitSpan(e4,this.GetViewport(e4),d9);
this.InitSpan(e4,this.GetColHeader(e4),e1);
this.InitSpan(e4,this.GetRowHeader(e4),e0);
e4.style.overflow="hidden";
if (this.GetParentSpread(e4)==null){
this.LoadScrollbarState(e4);
var f3=this.GetData(e4);
if (f3!=null){
var f4=f3.getElementsByTagName("root")[0];
var f5=f4.getElementsByTagName("activespread")[0];
if (f5!=null&&f5.innerHTML!=""){
this.SetPageActiveSpread(document.getElementById(this.Trim(f5.innerHTML)));
}
}
}
this.InitLayout(e4);
e4.e2=true;
if (this.GetPageActiveSpread()==e4&&(e4.getAttribute("AllowInsert")=="false"||e4.getAttribute("IsNewRow")=="true")){
var f6=this.GetCmdBtn(e4,"Insert");
this.UpdateCmdBtnState(f6,true);
f6=this.GetCmdBtn(e4,"Add");
this.UpdateCmdBtnState(f6,true);
}
this.CreateTextbox(e4);
this.CreateFocusBorder(e4);
this.InitSelection(e4);
e4.initialized=true;
if (this.GetPageActiveSpread()==e4)
{
try {
this.Focus(e4);
}catch (e){}
}
this.SaveData(e4);
if (this.handlers==null)
this.handlers=new FpSpread_EventHandlers();
this.handlers.DetachEvents(e4);
this.handlers.AttachEvents(e4);
if (c7!=null&&e4.style.position==""){
c7.parentNode.style.backgroundColor=c7.style.backgroundColor;
c7.parentNode.style.borderTop=c7.style.borderTop;
}
this.SyncColSelection(e4);
}
this.Dispose=function (e4){
if (this.handlers==null)
this.handlers=new FpSpread_EventHandlers();
this.handlers.DetachEvents(e4);
}
this.CmdbarMouseOver=function (event){
var f7=this.GetTarget(event);
if (f7!=null&&f7.tagName=="IMG"&&f7.getAttribute("disabled")!="true"){
f7.style.backgroundColor="cyan";
}
}
this.CmdbarMouseOut=function (event){
var f7=this.GetTarget(event);
if (f7!=null&&f7.tagName=="IMG"){
f7.style.backgroundColor="";
}
}
this.DoPropertyChange=function (event){
if (event.attrName=="curpos"){
this.ScrollViewport(event);
}else if (this.b4==null&&this.b5==null&&event.attrName=="pageincrement"&&event.ctrlKey){
var e4=this.GetSpread(this.GetTarget(event));
if (e4!=null)
this.SizeAll(this.GetTopSpread(e4));
}
}
this.HandleFirstKey=function (){
var e4=this.GetPageActiveSpread();
if (e4==null)return ;
var e7=this.GetTopSpread(e4);
var f8=document.getElementById(e7.id+"_textBox");
if (f8!=null&&f8.value!=""){
f8.value="";
}
}
this.IsXHTML=function (e4){
var e7=this.GetTopSpread(e4);
if (e7==null)return false;
var f9=e7.getAttribute("strictMode");
return (f9!=null&&f9=="true");
}
this.GetData=function (e4){
return e4.c1;
}
this.AttachEvent=function (target,event,handler,useCapture){
if (target.addEventListener!=null){
target.addEventListener(event,handler,useCapture);
}else if (target.attachEvent!=null){
target.attachEvent("on"+event,handler);
}
}
this.DetachEvent=function (target,event,handler,useCapture){
if (target.removeEventListener!=null){
target.removeEventListener(event,handler,useCapture);
}else if (target.detachEvent!=null){
target.detachEvent("on"+event,handler);
}
}
this.CancelDefault=function (e){
if (e.preventDefault!=null){
e.preventDefault();
e.stopPropagation();
}else {
e.cancelBubble=false;
e.returnValue=false;
}
return false;
}
this.CreateEvent=function (name){
var g0=document.createEvent("Events")
g0.initEvent(name,true,true);
return g0;
}
this.Refresh=function (e4){
var f7=e4.style.display;
e4.style.display="none";
e4.style.display=f7;
}
this.InitMethods=function (e4){
var e3=this;
e4.Edit=function (){e3.Edit(this);}
e4.Update=function (){e3.Update(this);}
e4.Cancel=function (){e3.Cancel(this);}
e4.Clear=function (){e3.Clear(this);}
e4.Copy=function (){e3.Copy(this);}
e4.Paste=function (){e3.Paste(this);}
e4.Prev=function (){e3.Prev(this);}
e4.Next=function (){e3.Next(this);}
e4.Add=function (){e3.Add(this);}
e4.Insert=function (){e3.Insert(this);}
e4.Delete=function (){e3.Delete(this);}
e4.Print=function (){e3.Print(this);}
e4.StartEdit=function (cell){e3.StartEdit(this,cell);}
e4.EndEdit=function (){e3.EndEdit(this);}
e4.ClearSelection=function (){e3.ClearSelection(this);}
e4.GetSelectedRange=function (){return e3.GetSelectedRange(this);}
e4.SetSelectedRange=function (r,c,rc,cc){e3.SetSelectedRange(this,r,c,rc,cc);}
e4.GetSelectedRanges=function (){return e3.GetSelectedRanges(this);}
e4.AddSelection=function (r,c,rc,cc){e3.AddSelection(this,r,c,rc,cc);}
e4.AddSpan=function (r,c,rc,cc,spans){e3.AddSpan(this,r,c,rc,cc,spans);}
e4.RemoveSpan=function (r,c,spans){e3.RemoveSpan(this,r,c,spans);}
e4.GetActiveRow=function (){var f7=e3.GetRowFromCell(this,this.d1);if (f7<0)return f7;return e3.GetSheetIndex(this,f7);}
e4.GetActiveCol=function (){return e3.GetColFromCell(this,this.d1);}
e4.SetActiveCell=function (r,c){e3.SetActiveCell(this,r,c);}
e4.GetCellByRowCol=function (r,c){return e3.GetCellByRowCol(this,r,c);}
e4.GetValue=function (r,c){return e3.GetValue(this,r,c);}
e4.SetValue=function (r,c,v,noEvent,recalc){e3.SetValue(this,r,c,v,noEvent,recalc);}
e4.GetFormula=function (r,c){return e3.GetFormula(this,r,c);}
e4.SetFormula=function (r,c,f,recalc,clientOnly){e3.SetFormula(this,r,c,f,recalc,clientOnly);}
e4.GetHiddenValue=function (r,colName){return e3.GetHiddenValue(this,r,colName);}
e4.GetSheetRowIndex=function (r){return e3.GetSheetRowIndex(this,r);}
e4.GetSheetColIndex=function (c){return e3.GetSheetColIndex(this,c);}
e4.GetRowCount=function (){return e3.GetRowCount(this);}
e4.GetColCount=function (){return e3.GetColCount(this);}
e4.GetRowByKey=function (key){return e3.GetRowByKey(this,key);}
e4.GetColByKey=function (key){return e3.GetColByKey(this,key);}
e4.GetRowKeyFromRow=function (r){return e3.GetRowKeyFromRow(this,r);}
e4.GetColKeyFromCol=function (c){return e3.GetColKeyFromCol(this,c);}
e4.GetTotalRowCount=function (){return e3.GetTotalRowCount(this);}
e4.GetPageCount=function (){return e3.GetPageCount(this);}
e4.GetParentSpread=function (){return e3.GetParentSpread(this);}
e4.GetChildSpread=function (r,ri){return e3.GetChildSpread(this,r,ri);}
e4.GetChildSpreads=function (){return e3.GetChildSpreads(this);}
e4.GetParentRowIndex=function (){return e3.GetParentRowIndex(this);}
e4.GetActiveChildSheetView=function (){return e3.GetActiveChildSheetView(this);}
e4.GetSpread=function (f7){return e3.GetSpread(f7);}
e4.UpdatePostbackData=function (){e3.UpdatePostbackData(this);}
e4.SizeToFit=function (c){e3.SizeToFit(this,c);}
e4.SetColWidth=function (c,w){e3.SetColWidth(this,c,w);}
e4.GetPreferredRowHeight=function (r){return e3.GetPreferredRowHeight(this,r);}
e4.SetRowHeight2=function (r,h){e3.SetRowHeight2(this,r,h);}
e4.CallBack=function (cmd,asyncCallBack){e3.SyncData(this.getAttribute("name"),cmd,this,asyncCallBack);}
e4.AddKeyMap=function (keyCode,ctrl,shift,alt,action){e3.AddKeyMap(this,keyCode,ctrl,shift,alt,action);}
e4.RemoveKeyMap=function (keyCode,ctrl,shift,alt){e3.RemoveKeyMap(this,keyCode,ctrl,shift,alt);}
e4.MoveToPrevCell=function (){e3.MoveToPrevCell(this);}
e4.MoveToNextCell=function (){e3.MoveToNextCell(this);}
e4.MoveToNextRow=function (){e3.MoveToNextRow(this);}
e4.MoveToPrevRow=function (){e3.MoveToPrevRow(this);}
e4.MoveToFirstColumn=function (){e3.MoveToFirstColumn(this);}
e4.MoveToLastColumn=function (){e3.MoveToLastColumn(this);}
e4.ScrollTo=function (r,c){e3.ScrollTo(this,r,c);}
e4.focus=function (){e3.Focus(this);}
e4.ShowMessage=function (msg,r,c,time){return e3.ShowMessage(this,msg,r,c,time);}
e4.HideMessage=function (r,c){return e3.HideMessage(this,r,c);}
e4.ProcessKeyMap=function (event){
if (this.keyMap!=null){
var e8=this.keyMap.length;
for (var e9=0;e9<e8;e9++){
var g1=this.keyMap[e9];
if (event.keyCode==g1.key&&event.ctrlKey==g1.ctrl&&event.shiftKey==g1.shift&&event.altKey==g1.alt){
var g2=false;
if (typeof(g1.action)=="function")
g2=g1.action();
else 
g2=eval(g1.action);
return g2;
}
}
}
return true;
}
e4.Cells=function (r,c){return e3.Cells(this,r,c);}
e4.Rows=function (r,c){return e3.Rows(this,r,c);}
e4.Columns=function (r,c){return e3.Columns(this,r,c);}
e4.GetTitleInfo=function (r,c){return e3.GetTitleInfo(this,r,c);}
e4.SizeSpread=function (e4){return e3.SizeSpread(e4);}
}
this.CreateTextbox=function (e4){
var e7=this.GetTopSpread(e4);
if (e7==null)return ;
var f8=document.getElementById(e7.id+"_textBox");
if (f8==null)
{
f8=document.createElement('INPUT');
f8.type="text";
f8.setAttribute("autocomplete","off");
f8.style.position="absolute";
f8.style.borderWidth=0;
f8.style.top="-10px";
f8.style.left="-100px";
f8.style.width="0px";
f8.style.height="1px";
if (e4.tabIndex!=null)
f8.tabIndex=e4.tabIndex;
f8.id=e4.id+"_textBox";
e4.insertBefore(f8,e4.firstChild);
}
}
this.CreateLineBorder=function (e4,id){
var g3=document.getElementById(id);
if (g3==null)
{
g3=document.createElement('div');
g3.style.position="absolute";
g3.style.left="-1000px";
g3.style.top="0px";
g3.style.overflow="hidden";
g3.style.border="1px solid black";
if (e4.getAttribute("FocusBorderColor")!=null)
g3.style.borderColor=e4.getAttribute("FocusBorderColor");
if (e4.getAttribute("FocusBorderStyle")!=null)
g3.style.borderStyle=e4.getAttribute("FocusBorderStyle");
g3.id=id;
var g4=this.GetViewport(e4).parentNode;
g4.insertBefore(g3,null);
}
return g3;
}
this.CreateFocusBorder=function (e4){
if (this.GetTopSpread(e4).getAttribute("hierView")=="true")return ;
if (this.GetTopSpread(e4).getAttribute("showFocusRect")=="false")return ;
if (this.GetViewport(e4)==null)return ;
var g3=this.CreateLineBorder(e4,e4.id+"_focusRectT");
g3.style.height=0;
g3=this.CreateLineBorder(e4,e4.id+"_focusRectB");
g3.style.height=0;
g3=this.CreateLineBorder(e4,e4.id+"_focusRectL");
g3.style.width=0;
g3=this.CreateLineBorder(e4,e4.id+"_focusRectR");
g3.style.width=0;
}
this.GetPosIndicator=function (e4){
var g5=e4.posIndicator;
if (g5==null)
g5=this.CreatePosIndicator(e4);
else if (g5.parentNode!=e4)
e4.insertBefore(g5,null);
return g5;
}
this.CreatePosIndicator=function (e4){
var g5=document.createElement("img");
g5.style.position="absolute";
g5.style.top="0px";
g5.style.left="-400px";
g5.style.width="10px";
g5.style.height="10px";
g5.style.zIndex=1000;
g5.id=e4.id+"_posIndicator";
if (e4.getAttribute("clienturl")!=null)
g5.src=e4.getAttribute("clienturl")+"down.gif";
else 
g5.src=e4.getAttribute("clienturlres");
e4.insertBefore(g5,null);
e4.posIndicator=g5;
return g5;
}
this.InitSpan=function (e4,e5,spans){
if (e5!=null){
var g6=0;
if (e5==this.GetViewport(e4))
g6=e5.rows.length;
var g7=e5.rows;
var g8=this.GetColCount(e4);
for (var g9=0;g9<g7.length;g9++){
if (this.IsChildSpreadRow(e4,e5,g9)){
if (e5==this.GetViewport(e4))g6--;
}else {
var h0=g7[g9].cells;
for (var h1=0;h1<h0.length;h1++){
var h2=h0[h1];
if (h2!=null&&((h2.rowSpan!=null&&h2.rowSpan>1)||(h2.colSpan!=null&&h2.colSpan>1))){
var h3=this.GetRowFromCell(e4,h2);
var h4=parseInt(h2.getAttribute("scol"));
if (h4<g8){
this.AddSpan(e4,h3,h4,h2.rowSpan,h2.colSpan,spans);
}
}
}
}
}
if (e5==this.GetViewport(e4))e4.setAttribute("rowCount",g6);
}
}
this.GetColWithSpan=function (e4,g9,spans,h1){
var h5=0;
var h6=0;
if (h1==0){
while (this.IsCovered(e4,g9,h6,spans))
{
h6++;
}
}
for (var e9=0;e9<spans.length;e9++){
if (spans[e9].rowCount>1&&(spans[e9].col<=h1||h1==0&&spans[e9].col<h6)&&g9>=spans[e9].row&&g9<spans[e9].row+spans[e9].rowCount)
h5+=spans[e9].colCount;
}
return h5;
}
this.AddSpan=function (e4,g9,h1,rc,g8,spans){
if (spans==null)spans=e4.d9;
var h7=new this.Range();
this.SetRange(h7,"Cell",g9,h1,rc,g8);
spans.push(h7);
this.PaintFocusRect(e4);
}
this.RemoveSpan=function (e4,g9,h1,spans){
if (spans==null)spans=e4.d9;
for (var e9=0;e9<spans.length;e9++){
var h7=spans[e9];
if (h7.row==g9&&h7.col==h1){
var h8=spans.length-1;
for (var h9=e9;h9<h8;h9++){
spans[h9]=spans[h9+1];
}
spans.length=spans.length-1;
break ;
}
}
this.PaintFocusRect(e4);
}
this.Focus=function (e4){
if (this.a7)return ;
this.SetPageActiveSpread(e4);
var i0=this.GetOperationMode(e4);
if (e4.d1==null&&i0!="MultiSelect"&&i0!="ExtendedSelect"&&e4.GetRowCount()>0&&e4.GetColCount()>0){
var i1=this.FireActiveCellChangingEvent(e4,0,0,0);
if (!i1){
e4.SetActiveCell(0,0);
var g0=this.CreateEvent("ActiveCellChanged");
g0.cmdID=e4.id;
g0.row=g0.Row=0;
g0.col=g0.Col=0;
if (e4.getAttribute("LayoutMode"))
g0.InnerRow=g0.innerRow=0;
this.FireEvent(e4,g0);
}
}
var e7=this.GetTopSpread(e4);
var f8=document.getElementById(e7.id+"_textBox");
if (e4.d1!=null){
var i2=this.GetEditor(e4.d1);
if (i2==null){
if (f8!=null){
if (this.b8!=f8){
try {f8.focus();}catch (g0){}
}
}
}else {
if (i2.tagName!="SELECT")i2.focus();
this.SetEditorFocus(i2);
}
}else {
if (f8!=null){
try {f8.focus();}catch (g0){}
}
}
this.EnableButtons(e4);
}
this.GetTotalRowCount=function (e4){
var f7=parseInt(e4.getAttribute("totalRowCount"));
if (isNaN(f7))f7=0;
return f7;
}
this.GetPageCount=function (e4){
var f7=parseInt(e4.getAttribute("pageCount"));
if (isNaN(f7))f7=0;
return f7;
}
this.GetColCount=function (e4){
var f7=parseInt(e4.getAttribute("colCount"));
if (isNaN(f7))f7=0;
return f7;
}
this.GetRowCount=function (e4){
var f7=parseInt(e4.getAttribute("rowCount"));
if (isNaN(f7))f7=0;
return f7;
}
this.GetRowCountInternal=function (e4){
var f7=parseInt(this.GetViewport(e4).rows.length);
if (isNaN(f7))f7=0;
return f7;
}
this.IsChildSpreadRow=function (e4,view,g9){
if (e4==null||view==null)return false;
if (g9>=1&&g9<view.rows.length){
if (view.rows[g9].cells.length>0&&view.rows[g9].cells[0]!=null&&view.rows[g9].cells[0].firstChild!=null){
var f7=view.rows[g9].cells[0].firstChild;
if (f7.nodeName!="#text"&&f7.getAttribute("FpSpread")=="Spread")return true;
}
}
return false;
}
this.GetChildSpread=function (e4,row,rindex){
var i3=this.GetViewport(e4);
if (i3!=null){
var g9=this.GetDisplayIndex(e4,row)+1;
if (typeof(rindex)=="number")g9+=rindex;
if (g9>=1&&g9<i3.rows.length){
if (i3.rows[g9].cells.length>0&&i3.rows[g9].cells[0]!=null&&i3.rows[g9].cells[0].firstChild!=null){
var f7=i3.rows[g9].cells[0].firstChild;
if (f7.nodeName!="#text"&&f7.getAttribute("FpSpread")=="Spread"){
return f7;
}
}
}
}
return null;
}
this.GetChildSpreads=function (e4){
var e9=0;
var g2=new Array();
var i3=this.GetViewport(e4);
if (i3!=null){
for (var g9=1;g9<i3.rows.length;g9++){
if (i3.rows[g9].cells.length>0&&i3.rows[g9].cells[0]!=null&&i3.rows[g9].cells[0].firstChild!=null){
var f7=i3.rows[g9].cells[0].firstChild;
if (f7.nodeName!="#text"&&f7.getAttribute("FpSpread")=="Spread"){
g2.length=e9+1;
g2[e9]=f7;
e9++;
}
}
}
}
return g2;
}
this.GetDisplayIndex=function (e4,row){
if (row<0)return -1;
var e9=0;
var g9=0;
var i3=this.GetViewport(e4);
if (i3!=null){
for (e9=0;e9<i3.rows.length;e9++){
if (this.IsChildSpreadRow(e4,i3,e9))continue ;
if (g9==row)break ;
g9++;
}
}
return e9;
}
this.GetSheetIndex=function (e4,row,c2){
var e9=0
var g9=0;
var i3=c2;
if (i3==null)i3=this.GetViewport(e4);
if (i3!=null){
if (row<0||row>=i3.rows.length)return -1;
for (e9=0;e9<row;e9++){
if (this.IsChildSpreadRow(e4,i3,e9))continue ;
g9++;
}
}
return g9;
}
this.GetParentRowIndex=function (e4){
var i4=this.GetParentSpread(e4);
if (i4==null)return -1;
var i3=this.GetViewport(i4);
if (i3==null)return -1;
var i5=e4.parentNode.parentNode;
var e9=i5.rowIndex-1;
for (;e9>0;e9--){
if (this.IsChildSpreadRow(i4,i3,e9))continue ;
else 
break ;
}
return this.GetSheetIndex(i4,e9,i3);
}
this.CreateTestBox=function (e4){
var i6=document.getElementById(e4.id+"_testBox");
if (i6==null)
{
i6=document.createElement("span");
i6.style.position="absolute";
i6.style.borderWidth=0;
i6.style.top="-500px";
i6.style.left="-100px";
i6.id=e4.id+"_testBox";
e4.insertBefore(i6,e4.firstChild);
}
return i6;
}
this.SizeToFit=function (e4,h1){
if (h1==null||h1<0)h1=0;
var e5=this.GetViewport(e4);
if (e5!=null){
var i6=this.CreateTestBox(e4);
var g7=e5.rows;
var i7=0;
for (var g9=0;g9<g7.length;g9++){
if (!this.IsChildSpreadRow(e4,e5,g9)){
var i8=this.GetCellFromRowCol(e4,g9,h1);
if (i8.colSpan>1)continue ;
var i9=this.GetPreferredCellWidth(e4,i8,i6);
if (i9>i7)i7=i9;
}
}
this.SetColWidth(e4,h1,i7);
}
}
this.GetPreferredCellWidth=function (e4,i8,i6){
if (i6==null)i6=this.CreateTestBox(e4);
var j0=this.GetRender(e4,i8);
if (j0!=null){
i6.style.fontFamily=j0.style.fontFamily;
i6.style.fontSize=j0.style.fontSize;
i6.style.fontWeight=j0.style.fontWeight;
i6.style.fontStyle=j0.style.fontStyle;
}
i6.innerHTML=i8.innerHTML;
var i9=i6.offsetWidth+8;
if (i8.style.paddingLeft!=null&&i8.style.paddingLeft.length>0)
i9+=parseInt(i8.style.paddingLeft);
if (i8.style.paddingRight!=null&&i8.style.paddingRight.length>0)
i9+=parseInt(i8.style.paddingRight);
return i9;
}
this.GetHierBar=function (e4){
if (e4.c8==null)e4.c8=document.getElementById(e4.id+"_hierBar");
return e4.c8;
}
this.GetGroupBar=function (e4){
if (e4.groupBar==null)e4.groupBar=document.getElementById(e4.id+"_groupBar");
return e4.groupBar;
}
this.GetPager1=function (e4){
if (e4.c9==null)e4.c9=document.getElementById(e4.id+"_pager1");
return e4.c9;
}
this.GetPager2=function (e4){
if (e4.d0==null)e4.d0=document.getElementById(e4.id+"_pager2");
return e4.d0;
}
this.SynRowHeight=function (e4,c5,e5,g9,updateParent,header,c4){
if (c5==null||e5==null)return ;
if (typeof(c5.rows[g9])!="undefined"&&
typeof(c5.rows[g9].cells[0])!="undefined")
{
if (c5.rows[g9].cells[0].style.posHeight==null||c5.rows[g9].cells[0].style.posHeight=="")
c5.rows[g9].cells[0].style.posHeight=c5.rows[g9].offsetHeight-1;
}
var j1=c5.rows[g9].offsetHeight;
var g4=e5.rows[g9].offsetHeight;
if (j1==g4&&(g9>0||c4))return ;
var j2=0;
if (e5.cellSpacing=="0"&&g9==0){
if (document.defaultView!=null&&document.defaultView.getComputedStyle!=null){
var j3=0;
for (var e9=0;e9<e5.rows[g9].cells.length;e9++){
j3=parseInt(document.defaultView.getComputedStyle(e5.rows[g9].cells[e9],'').getPropertyValue("border-top-width"));
if (j3>j2)j2=j3;
}
}
}
e5.rows[g9].style.height="";
var j4=Math.max(j1,g4);
j2=parseInt(j2/2);
var j5=this.IsXHTML(e4);
if (this.IsChildSpreadRow(e4,e5,g9)){
if (j5)j4-=1;
c5.rows[g9].cells[0].style.posHeight=j4-1;
return ;
}
if (j5){
if (j4==j1){
if (e5.rows[g9].cells[0]!=null){
if (e5.cellSpacing=="0"&&g9==0){
e5.rows[g9].cells[0].style.posHeight+=(j4-g4-j2);
}else {
e5.rows[g9].cells[0].style.posHeight+=(j4-g4);
}
}
}else {
if (c5.rows[g9].cells[0]!=null){
if (c5.cellSpacing=="0"&&g9==0){
c5.rows[g9].cells[0].style.posHeight+=(j4-j1+j2);
}else {
c5.rows[g9].cells[0].style.posHeight+=(j4-j1);
}
}
}
}
else 
{
if (j4==j1){
if (e5.rows[g9].cells[0]!=null){
if (g9==0&&parseInt(e5.cellSpacing)==0){
e5.rows[g9].cells[0].style.posHeight=j1-j2;
}else {
e5.rows[g9].cells[0].style.posHeight=j1;
}
}
}
else {
if (c5.rows[g9].cells[0]!=null){
if (g9==0&&parseInt(e5.cellSpacing)==0){
c5.rows[g9].cells[0].style.posHeight=g4+j2;
}else 
c5.rows[g9].cells[0].style.posHeight=g4;
}
}
}
if (updateParent){
var i4=this.GetParentSpread(e4);
if (i4!=null)this.UpdateRowHeight(i4,e4);
}
}
this.SizeAll=function (e4){
var j6=this.GetChildSpreads(e4);
if (j6!=null&&j6.length>0){
for (var e9=0;e9<j6.length;e9++){
this.SizeAll(j6[e9]);
}
}
this.SizeSpread(e4);
if (this.GetParentSpread(e4)!=null)
this.Refresh(e4);
}
this.SizeSpread=function (e4){
var j5=this.IsXHTML(e4);
var c2=this.GetViewport(e4);
if (c2==null)return ;
this.SyncMsgs(e4);
var c5=this.GetRowHeader(e4);
if (c5!=null){
for (var e9=0;e9<c2.rows.length&&e9<c5.rows.length;e9++){
this.SynRowHeight(e4,c5,c2,e9,false,true);
this.SynRowHeight(e4,c5,c2,e9,false,true);
if (e9==0&&c5.rows[0].cells[0]&&c2.rows[0].cells[0]&&c2.rows[0].cells[0].getAttribute("CellType2")=="SlideShowCellType")
c5.rows[0].cells[0].style.posHeight=c2.rows[0].cells[0].offsetHeight-1;
}
}
var j7=this.GetColFooter(e4);
var c6=this.GetColHeader(e4);
var c4=this.GetCorner(e4);
if (c4!=null&&c6!=null&&c4.getAttribute("allowTableCorner")){
for (var e9=0;e9<c4.rows.length&&e9<c6.rows.length;e9++){
if (c6.rows[e9].cells.length){
if (c6.rows[0].cells.length>1)
this.SynRowHeight(e4,c6,c4,e9,true,true,false);
this.SynRowHeight(e4,c4,c6,e9,true,false,true);
}
}
}
var j8=this.GetColGroup(c2);
var j9=this.GetColGroup(c6);
if (j8!=null&&j8.childNodes.length>0&&j9!=null&&j9.childNodes.length>0){
var k0=-1;
if (this.b4!=null)k0=parseInt(this.b4.getAttribute("index"));
if (this.b4==null||k0==0)
{
var k1=parseInt(j8.childNodes[0].width);var k2=parseInt(j8.childNodes[0].offsetLeft);
j9.childNodes[0].width=""+(k1-k2)+"px";
j8.childNodes[0].width=""+k1+"px";
this.SetWidthFix(c6,0,(k1-k2));
this.SetWidthFix(c2,0,k1);
}
}
var i4=this.GetParentSpread(e4);
if (i4!=null)this.UpdateRowHeight(i4,e4);
var j4=e4.clientHeight;
var k3=this.GetCommandBar(e4);
if (k3!=null)
{
k3.style.width=""+e4.clientWidth+"px";
if (e4.style.position!="absolute"&&e4.style.position!="relative"){
k3.parentNode.style.borderTop="1px solid white";
k3.parentNode.style.backgroundColor=k3.style.backgroundColor;
}
var k4=this.GetElementById(k3,e4.id+"_cmdTable");
if (k4!=null){
if (e4.style.position!="absolute"&&e4.style.position!="relative"&&(k4.style.height==""||parseInt(k4.style.height)<27)){
k4.style.height=""+(k4.offsetHeight+3)+"px";
}
if (!j5&&parseInt(c2.cellSpacing)>0)
k4.parentNode.style.height=""+(k4.offsetHeight+3)+"px";
j4-=Math.max(k4.parentNode.offsetHeight,k4.offsetHeight);
}
if (e4.style.position!="absolute"&&e4.style.position!="relative")
k3.style.position="";
}
var c6=this.GetColHeader(e4);
if (c6!=null)
{
j4-=c6.offsetHeight;
c6.parentNode.style.height=""+(c6.offsetHeight-parseInt(c6.cellSpacing))+"px";
if (j5)
j4+=parseInt(c6.cellSpacing);
}
var j7=this.GetColFooter(e4);
if (j7!=null)
{
j4-=j7.offsetHeight;
j7.parentNode.style.height=""+(j7.offsetHeight)+"px";
}
var c8=this.GetHierBar(e4);
if (c8!=null)
{
j4-=c8.offsetHeight;
}
var k5=document.getElementById(e4.id+"_titleBar");
if (k5)j4-=k5.parentNode.parentNode.offsetHeight;
var k6=this.GetGroupBar(e4);
if (k6!=null){
j4-=k6.offsetHeight;
}
var c9=this.GetPager1(e4);
if (c9!=null)
{
j4-=c9.offsetHeight;
this.InitSlideBar(e4,c9);
}
var k7=(e4.getAttribute("cmdTop")=="true");
var d0=this.GetPager2(e4);
if (d0!=null)
{
d0.style.width=""+(e4.clientWidth-10)+"px";
j4-=Math.max(d0.offsetHeight,28);
this.InitSlideBar(e4,d0);
}
var k8=null;
if (c5!=null)k8=c5.parentNode;
var k9=null;
if (c6!=null)k9=c6.parentNode;
var l0=null;
if (j7!=null)l0=j7.parentNode;
var l1=this.GetFooterCorner(e4);
if (l0!=null)
{
l0.style.height=""+j7.offsetHeight-parseInt(c2.cellSpacing)+"px";
if (l1!=null){
l1.parentNode.style.height=l0.style.height;
}
}
if (l1!=null&&!j5)
l1.width=""+(l1.parentNode.offsetWidth+parseInt(c2.cellSpacing))+"px";
var l2=c2.parentNode;
var c4=this.GetCorner(e4);
if (j5&&k9!=null)
{
k9.style.height=""+c6.offsetHeight-parseInt(c2.cellSpacing)+"px";
if (c4!=null){
c4.parentNode.style.height=k9.style.height;
}
}
if (c4!=null&&!j5)
c4.width=""+(c4.parentNode.offsetWidth+parseInt(c2.cellSpacing))+"px";
if (l2!=null){
if (k8!=null){
l2.style.width=""+Math.max(e4.clientWidth-k8.offsetWidth+parseInt(c2.cellSpacing),1)+"px";
l2.style.height=""+Math.max(j4,1)+"px";
l2.style.width=""+Math.max(e4.clientWidth-k8.offsetWidth+parseInt(c2.cellSpacing),1)+"px";
}else {
l2.style.width=""+Math.max(e4.clientWidth,1)+"px";
l2.style.height=""+Math.max(j4,1)+"px";
l2.style.width=""+Math.max(e4.clientWidth,1)+"px";
}
}
var l3=0;
if (this.GetColFooter(e4)){
l3=this.GetColFooter(e4).offsetHeight;
}
if (k3!=null&&!k7){
if (d0!=null){
if (e4.style.position=="absolute"||e4.style.position=="relative"){
k3.style.position="absolute";
k3.style.top=""+(e4.clientHeight-Math.max(d0.offsetHeight,28)-k3.offsetHeight)+"px";
}else {
k3.style.position="absolute";
k3.style.top=""+(c2.parentNode.offsetTop+l3+c2.parentNode.offsetHeight)+"px";
}
}else {
if (e4.style.position=="absolute"||e4.style.position=="relative")
{
k3.style.position="absolute";
k3.style.top=""+(e4.clientHeight-k3.offsetHeight)+"px";
}else {
k3.style.position="absolute";
if (d0!=null)
k3.style.top=""+(this.GetOffsetTop(e4,e4,document.body)+e4.clientHeight-Math.max(d0.offsetHeight,28)-k3.offsetHeight)+"px";
else 
k3.style.top=""+(this.GetOffsetTop(e4,e4,document.body)+e4.clientHeight-k3.offsetHeight+1)+"px";
}
}
}
if (d0!=null)
{
if (e4.style.position=="absolute"||e4.style.position=="relative"){
d0.style.position="absolute";
d0.style.top=""+(e4.clientHeight-Math.max(d0.offsetHeight,28))+"px";
}else {
d0.style.position="absolute";
if (k3!=null&&!k7)
d0.style.top=""+(c2.parentNode.offsetTop+c2.parentNode.offsetHeight+k3.offsetHeight+l3)+"px";
else 
d0.style.top=""+(c2.parentNode.offsetTop+c2.parentNode.offsetHeight+l3)+"px";
}
}
if (k8!=null){
if (j5)k8.style.height=""+Math.max(l2.offsetHeight,1)+"px";
else k8.style.height=Math.max(l2.offsetHeight,1);
}
if (c2&&!c5){
c2.parentNode.parentNode.style.height=""+c2.parentNode.offsetHeight+"px";
}
return ;
if (this.GetParentSpread(e4)==null&&k9!=null){
var i9=0;
if (k8!=null){
i9=Math.max(e4.clientWidth-k8.offsetWidth,1);
}else {
i9=Math.max(e4.clientWidth,1);
}
k9.style.width=i9;
k9.parentNode.style.width=i9;
}
if (j5)
{
if (c2!=null){
c2.style.posTop=-c2.cellSpacing;
var l4=e4.clientWidth;
if (c5!=null)l4-=c5.parentNode.offsetWidth;
c2.parentNode.style.width=""+l4+"px";
}
if (c5!=null){
c5.style.position="relative";
c5.parentNode.style.position="relative";
c5.style.posTop=-c2.cellSpacing;
c5.width=""+(c5.parentNode.offsetWidth)+"px";
}
}else {
if (c2!=null){
var l4=e4.clientWidth;
if (c5!=null){
l4-=c5.parentNode.offsetWidth;
c5.width=""+(c5.parentNode.offsetWidth+parseInt(c2.cellSpacing))+"px";
}
c2.parentNode.style.width=""+l4+"px";
}
}
this.ScrollView(e4);
this.PaintFocusRect(e4);
}
this.InitSlideBar=function (e4,pager){
var l5=this.GetElementById(pager,e4.id+"_slideBar");
if (l5!=null){
var j5=this.IsXHTML(e4);
if (j5)
l5.style.height=Math.max(pager.offsetHeight,28)+"px";
else 
l5.style.height=(pager.offsetHeight-2)+"px";
var f7=pager.getElementsByTagName("TABLE");
if (f7!=null&&f7.length>0){
var l6=f7[0].rows[0];
var h4=l6.cells[0];
var l7=l6.cells[2];
e4.slideLeft=Math.max(107,h4.offsetWidth+1);
if (h4.style.paddingRight!="")e4.slideLeft+=parseInt(h4.style.paddingRight);
e4.slideRight=pager.offsetWidth-l7.offsetWidth-l5.offsetWidth-3;
if (l7.style.paddingRight!="")e4.slideRight-=parseInt(l7.style.paddingLeft);
var l8=parseInt(pager.getAttribute("curPage"));
var l9=parseInt(pager.getAttribute("totalPage"))-1;
if (l9==0)l9=1;
var m0=false;
var l4=Math.max(107,e4.slideLeft)+(l8/l9)*(e4.slideRight-e4.slideLeft);
if (pager.id.indexOf("pager1")>=0&&e4.style.position!="absolute"&&e4.style.position!="relative"){
l4+=this.GetOffsetLeft(e4,pager,document);
var m1=(this.GetOffsetTop(e4,h4,pager)+this.GetOffsetTop(e4,pager,document));
l5.style.top=m1+"px";
m0=true;
}
var k5=document.getElementById(e4.id+"_titleBar");
if (pager.id.indexOf("pager1")>=0&&!m0&&k5!=null){
var m1=k5.parentNode.parentNode.offsetHeight;
l5.style.top=m1+"px";
}
l5.style.left=l4+"px";
}
}
}
this.InitLayout=function (e4){
this.SizeSpread(e4);
this.SizeSpread(e4);
this.SizeSpread(e4);
}
this.GetRowByKey=function (e4,key){
if (key=="-1")
return -1;
var m2=this.GetViewport(e4);
if (m2!=null){
for (var i5=0;i5<m2.rows.length;i5++){
if (m2.rows[i5].getAttribute("FpKey")==key){
return i5;
}
}
}
if (m2!=null)
return 0;
else 
return -1;
}
this.GetColByKey=function (e4,key){
if (key=="-1")
return -1;
var m2=this.GetViewport(e4);
var m3=this.GetColGroup(m2);
if (m3==null||m3.childNodes.length==0)
m3=this.GetColGroup(this.GetColHeader(e4));
if (m3!=null){
for (var m4=0;m4<m3.childNodes.length;m4++){
var f7=m3.childNodes[m4];
if (f7.getAttribute("FpCol")==key){
return m4;
}
}
}
return 0;
}
this.IsRowSelected=function (e4,i5){
var m5=this.GetSelection(e4);
if (m5!=null){
var m6=m5.firstChild;
while (m6!=null){
var g9=parseInt(m6.getAttribute("rowIndex"));
var m7=parseInt(m6.getAttribute("rowcount"));
if (g9<=i5&&i5<g9+m7)
return true;
m6=m6.nextSibling;
}
}
}
this.InitSelection=function (e4){
var g9=0;
var h1=0;
var f3=this.GetData(e4);
if (f3==null)return ;
var f4=f3.getElementsByTagName("root")[0];
var m8=f4.getElementsByTagName("state")[0];
var m5=m8.getElementsByTagName("selection")[0];
var m9=m8.firstChild;
while (m9!=null&&m9.tagName!="activerow"&&m9.tagName!="ACTIVEROW"){
m9=m9.nextSibling;
}
if (m9!=null)
g9=this.GetRowByKey(e4,m9.innerHTML);
if (g9>=this.GetRowCount(e4))g9=0;
var n0=m8.firstChild;
while (n0!=null&&n0.tagName!="activecolumn"&&n0.tagName!="ACTIVECOLUMN"){
n0=n0.nextSibling;
}
if (n0!=null)
h1=this.GetColByKey(e4,n0.innerHTML);
if (g9<0)g9=0;
if (g9>=0||h1>=0){
var n1=f3;
if (this.GetParentSpread(e4)!=null){
var n2=this.GetTopSpread(e4);
if (n2.initialized)n1=this.GetData(n2);
f4=n1.getElementsByTagName("root")[0];
}
var n3=f4.getElementsByTagName("activechild")[0];
e4.d3=g9;e4.d4=h1;
if ((this.GetParentSpread(e4)==null&&(n3==null||n3.innerHTML==""))||(n3!=null&&e4.id==this.Trim(n3.innerHTML))){
this.UpdateAnchorCell(e4,g9,h1);
}else {
e4.d1=this.GetCellFromRowCol(e4,g9,h1);
}
}
var m6=m5.firstChild;
while (m6!=null){
var g9=this.GetRowByKey(e4,m6.getAttribute("row"));
var h1=this.GetColByKey(e4,m6.getAttribute("col"));
var m7=parseInt(m6.getAttribute("rowcount"));
var g8=parseInt(m6.getAttribute("colcount"));
m6.setAttribute("rowIndex",g9);
m6.setAttribute("colIndex",h1);
this.PaintSelection(e4,g9,h1,m7,g8,true);
m6=m6.nextSibling;
}
this.PaintFocusRect(e4);
}
this.TranslateKey=function (event){
event=this.GetEvent(event);
var n4=this.GetTarget(event);
try {
if (document.readyState!=null&&document.readyState!="complete")return ;
var e4=this.GetPageActiveSpread();
if (event.altKey&&event.keyCode==this.down&&typeof(n4.getAttribute("mccbparttype"))!="undefined"&&n4.getAttribute("mccbparttype")=="DropDownButton")return ;
if (typeof(e4.getAttribute("mcctCellType"))!="undefined"&&e4.getAttribute("mcctCellType")=="true")return ;
if (this.GetOperationMode(e4)=="RowMode"&&this.GetEnableRowEditTemplate(e4)=="true"&&this.IsInRowEditTemplate(e4,n4))return ;
if (e4!=null){
if (event.keyCode==229){
this.CancelDefault(event);
return ;
}
if (!this.IsChild(n4,this.GetTopSpread(e4)))return ;
this.KeyDown(e4,event);
var n5=false;
if (event.keyCode==this.tab){
var n6=this.GetProcessTab(e4);
n5=(n6=="true"||n6=="True");
}
if (n5)
this.CancelDefault(event);
}
}catch (g0){}
}
this.IsInRowEditTemplate=function (e4,n4){
while (n4&&n4.parentNode){
n4=n4.parentNode;
if (n4.tagName=="DIV"&&n4.id==e4.id+"_RowEditTemplateContainer")
return true;
}
return false;
}
this.KeyAction=function (key,ctrl,shift,alt,action){
this.key=key;
this.ctrl=ctrl;
this.shift=shift;
this.alt=alt;
this.action=action;
}
this.RemoveKeyMap=function (e4,keyCode,ctrl,shift,alt,action){
if (e4.keyMap==null)e4.keyMap=new Array();
var e8=e4.keyMap.length;
for (var e9=0;e9<e8;e9++){
var g1=e4.keyMap[e9];
if (g1!=null&&g1.key==keyCode&&g1.ctrl==ctrl&&g1.shift==shift&&g1.alt==alt){
for (var h9=e9+1;h9<e8;h9++){
e4.keyMap[h9-1]=e4.keyMap[h9];
}
e4.keyMap.length=e4.keyMap.length-1;
break ;
}
}
}
this.AddKeyMap=function (e4,keyCode,ctrl,shift,alt,action){
if (e4.keyMap==null)e4.keyMap=new Array();
var g1=this.GetKeyAction(e4,keyCode,ctrl,shift,alt);
if (g1!=null){
g1.action=action;
}else {
var e8=e4.keyMap.length;
e4.keyMap.length=e8+1;
e4.keyMap[e8]=new this.KeyAction(keyCode,ctrl,shift,alt,action);
}
}
this.GetKeyAction=function (e4,keyCode,ctrl,shift,alt){
if (e4.keyMap==null)e4.keyMap=new Array();
var e8=e4.keyMap.length;
for (var e9=0;e9<e8;e9++){
var g1=e4.keyMap[e9];
if (g1!=null&&g1.key==keyCode&&g1.ctrl==ctrl&&g1.shift==shift&&g1.alt==alt){
return g1;
}
}
return null;
}
this.MoveToPrevCell=function (e4){
var n7=this.EndEdit(e4);
if (!n7)return ;
var g9=e4.GetActiveRow();
var h1=e4.GetActiveCol();
this.MoveLeft(e4,g9,h1);
}
this.MoveToNextCell=function (e4){
var n7=this.EndEdit(e4);
if (!n7)return ;
var g9=e4.GetActiveRow();
var h1=e4.GetActiveCol();
this.MoveRight(e4,g9,h1);
}
this.MoveToNextRow=function (e4){
var n7=this.EndEdit(e4);
if (!n7)return ;
var g9=e4.GetActiveRow();
var h1=e4.GetActiveCol();
this.MoveDown(e4,g9,h1);
}
this.MoveToPrevRow=function (e4){
var n7=this.EndEdit(e4);
if (!n7)return ;
var g9=e4.GetActiveRow();
var h1=e4.GetActiveCol();
if (g9>0)
this.MoveUp(e4,g9,h1);
}
this.MoveToFirstColumn=function (e4){
var n7=this.EndEdit(e4);
if (!n7)return ;
var g9=e4.GetActiveRow();
if (e4.d1.parentNode.rowIndex>=0)
this.UpdateLeadingCell(e4,g9,0);
}
this.MoveToLastColumn=function (e4){
var n7=this.EndEdit(e4);
if (!n7)return ;
var g9=e4.GetActiveRow();
if (e4.d1.parentNode.rowIndex>=0){
h1=this.GetColCount(e4)-1;
this.UpdateLeadingCell(e4,g9,h1);
}
}
this.UpdatePostbackData=function (e4){
this.SaveData(e4);
}
this.PrepareData=function (m6){
var g2="";
if (m6!=null){
if (m6.nodeName=="#text")
g2=m6.nodeValue;
else {
g2=this.GetBeginData(m6);
var f7=m6.firstChild;
while (f7!=null){
var n8=this.PrepareData(f7);
if (n8!="")g2+=n8;
f7=f7.nextSibling;
}
g2+=this.GetEndData(m6);
}
}
return g2;
}
this.GetBeginData=function (m6){
var g2="<"+m6.nodeName.toLowerCase();
if (m6.attributes!=null){
for (var e9=0;e9<m6.attributes.length;e9++){
var n9=m6.attributes[e9];
if (n9.nodeName!=null&&n9.nodeName!=""&&n9.nodeName!="style"&&n9.nodeValue!=null&&n9.nodeValue!="")
g2+=(" "+n9.nodeName+"=\""+n9.nodeValue+"\"");
}
}
g2+=">";
return g2;
}
this.GetEndData=function (m6){
return "</"+m6.nodeName.toLowerCase()+">";
}
this.SaveData=function (e4){
if (e4==null)return ;
try {
var f3=this.GetData(e4);
var f4=f3.getElementsByTagName("root")[0];
var f7=this.PrepareData(f4);
var o0=document.getElementById(e4.id+"_data");
o0.value=encodeURIComponent(f7);
}catch (g0){
alert("e "+g0);
}
}
this.SetActiveSpread=function (event){
try {
event=this.GetEvent(event);
var n4=this.GetTarget(event);
var o1=this.GetSpread(n4,false);
var o2=this.GetPageActiveSpread();
if (this.a7&&(o1==null||o1!=o2)&&o1.getAttribute("mcctCellType")!="true"&&o2.getAttribute("mcctCellType")!="true"){
if (n4!=this.a8&&this.a8!=null){
if (this.a8.blur!=null)this.a8.blur();
}
var n7=this.EndEdit();
if (!n7)return ;
}
var o3=false;
if (o1==null){
o1=this.GetSpread(n4,true);
o3=(o1!=null);
}
var h2=this.GetCell(n4,true);
if (h2==null&&o2!=null&&o2.e2){
this.SaveData(o2);
o2.e2=false;
}
if (o2!=null&&o2.e2&&(o1!=o2||o1==null||o3)){
this.SaveData(o2);
o2.e2=false;
}
if (o2!=null&&o2.e2&&o1==o2&&n4.tagName=="INPUT"&&(n4.type=="submit"||n4.type=="button"||n4.type=="image")){
this.SaveData(o2);
o2.e2=false;
}
if (o1!=null&&this.GetOperationMode(o1)=="ReadOnly")return ;
var n2=null;
if (o1==null){
if (o2==null)return ;
n2=this.GetTopSpread(o2);
this.SetActiveSpreadID(n2,"",null,false);
this.SetPageActiveSpread(null);
}else {
if (o1!=o2){
if (o2!=null){
n2=this.GetTopSpread(o2);
this.SetActiveSpreadID(n2,"",null,false);
}
if (o3){
n2=this.GetTopSpread(o1);
var j3=this.GetTopSpread(o2);
if (n2!=j3){
this.SetActiveSpreadID(n2,o1.id,o1.id,true);
this.SetPageActiveSpread(o1);
}else {
this.SetActiveSpreadID(n2,o2.id,o2.id,true);
this.SetPageActiveSpread(o2);
}
}else {
n2=this.GetTopSpread(o1);
this.SetPageActiveSpread(o1);
this.SetActiveSpreadID(n2,o1.id,o1.id,false);
}
}
}
}catch (g0){}
}
this.SetActiveSpreadID=function (e4,id,child,o3){
var f3=this.GetData(e4);
var f4=f3.getElementsByTagName("root")[0];
var f5=f4.getElementsByTagName("activespread")[0];
var o4=f4.getElementsByTagName("activechild")[0];
if (f5==null)return ;
if (o3&&o4!=null&&o4.nodeValue!=""){
f5.innerHTML=o4.innerHTML;
}else {
f5.innerHTML=id;
if (child!=null&&o4!=null)o4.innerHTML=child;
}
this.SaveData(e4);
e4.e2=false;
}
this.GetSpread=function (ele,incCmdBar){
var i9=ele;
while (i9!=null&&i9.tagName!="BODY"){
if (typeof(i9.getAttribute)!="function")break ;
var e4=i9.getAttribute("FpSpread");
if (e4==null)e4=i9.FpSpread;
if (e4=="Spread"){
if (!incCmdBar){
var f7=ele;
while (f7!=null&&f7!=i9){
if (f7.id==i9.id+"_commandBar"||f7.id==i9.id+"_pager1"||f7.id==i9.id+"_pager2")return null;
f7=f7.parentNode;
}
}
return i9;
}
i9=i9.parentNode;
}
return null;
}
this.ScrollViewport=function (event){
var f7=this.GetTarget(event);
var e4=this.GetTopSpread(f7);
if (e4!=null)this.ScrollView(e4);
}
this.GetActiveChildSheetView=function (e4){
var o2=this.GetPageActiveSheetView();
if (typeof(o2)=="undefined")return null;
var n2=this.GetTopSpread(e4);
var o5=this.GetTopSpread(o2);
if (o5!=n2)return null;
if (o2==o5)return null;
return o2;
}
this.ScrollTo=function (e4,i5,m4){
var h2=this.GetCellByRowCol(e4,i5,m4);
if (h2==null)return ;
var i3=this.GetViewport(e4).parentNode;
if (i3==null)return ;
i3.scrollTop=h2.offsetTop;
i3.scrollLeft=h2.offsetLeft;
}
this.ScrollView=function (e4){
var o1=this.GetTopSpread(e4);
var c5=this.GetParent(this.GetRowHeader(o1));
var c6=this.GetParent(this.GetColHeader(o1));
var j7=this.GetParent(this.GetColFooter(o1));
var i3=this.GetParent(this.GetViewport(o1));
var o6=false;
if (c5!=null){
o6=(c5.scrollTop!=i3.scrollTop);
c5.scrollTop=i3.scrollTop;
}
if (c6!=null){
if (!o6)o6=(c6.scrollLeft!=i3.scrollLeft);
c6.scrollLeft=i3.scrollLeft;
}
if (j7!=null){
if (!o6)o6=(j7.scrollLeft!=i3.scrollLeft);
j7.scrollLeft=i3.scrollLeft;
}
if (this.GetParentSpread(e4)==null)this.SaveScrollbarState(e4,i3.scrollTop,i3.scrollLeft);
if (o6){
var g0=this.CreateEvent("Scroll");
this.FireEvent(e4,g0);
if (e4.frzRows!=0||e4.frzCols!=0)this.SyncMsgs(e4);
}
if (i3.scrollTop>0&&i3.scrollTop+i3.offsetHeight>=this.GetViewport(o1).offsetHeight){
if (!this.a7&&e4.getAttribute("loadOnDemand")=="true"){
if (e4.LoadState!=null)return ;
e4.LoadState=true;
this.SaveData(e4);
e4.CallBack("LoadOnDemand",true);
}
}
}
this.SaveScrollbarState=function (e4,scrollTop,scrollLeft){
if (this.GetParentSpread(e4)!=null)return ;
var f3=this.GetData(e4);
var f4=f3.getElementsByTagName("root")[0];
var o7=f4.getElementsByTagName("scrollTop")[0];
var o8=f4.getElementsByTagName("scrollLeft")[0];
if (e4.getAttribute("scrollContent")=="true")
if (o7!=null&&o8!=null)
if (o7.innerHTML!=scrollTop||o8.innerHTML!=scrollLeft)
this.ShowScrollingContent(e4,o7.innerHTML==scrollTop);
if (o7!=null)o7.innerHTML=scrollTop;
if (o8!=null)o8.innerHTML=scrollLeft;
}
this.LoadScrollbarState=function (e4){
return ;
if (this.GetParentSpread(e4)!=null)return ;
var f3=this.GetData(e4);
var f4=f3.getElementsByTagName("root")[0];
var o7=f4.getElementsByTagName("scrollTop")[0];
var o8=f4.getElementsByTagName("scrollLeft")[0];
var o9=0;
if (o7!=null&&o7.innerHTML!=""){
o9=parseInt(o7.innerHTML);
}else {
o9=0;
}
var p0=0;
if (o8!=null&&o8.innerHTML!=""){
p0=parseInt(o8.innerHTML);
}else {
p0=0;
}
var i3=this.GetParent(this.GetViewport(e4));
if (i3!=null){
if (!isNaN(o9))i3.scrollTop=o9;
if (!isNaN(p0))i3.scrollLeft=p0;
var c5=this.GetParent(this.GetRowHeader(e4));
var c6=this.GetParent(this.GetColHeader(e4));
var j7=this.GetParent(this.GetColFooter(e4));
if (j7!=null){
j7.scrollLeft=i3.scrollLeft;
}
if (c5!=null){
c5.scrollTop=i3.scrollTop;
}
if (c6!=null){
c6.scrollLeft=i3.scrollLeft;
}
}
}
this.GetParent=function (g0){
if (g0==null)
return null;
else 
return g0.parentNode;
}
this.GetViewport=function (e4){
return e4.c2;
}
this.GetCommandBar=function (e4){
return e4.c7;
}
this.GetRowHeader=function (e4){
return e4.c5;
}
this.GetColHeader=function (e4){
return e4.c6;
}
this.GetColFooter=function (e4){
return e4.colFooter;
}
this.GetFooterCorner=function (e4){
return e4.footerCorner;
}
this.GetCmdBtn=function (e4,id){
var o1=this.GetTopSpread(e4);
var p1=this.GetCommandBar(o1);
if (p1!=null)
return this.GetElementById(p1,o1.id+"_"+id);
else 
return null;
}
this.Range=function (){
this.type="Cell";
this.row=-1;
this.col=-1;
this.rowCount=0;
this.colCount=0;
}
this.SetRange=function (h7,type,i5,m4,m7,g8){
h7.type=type;
h7.row=i5;
h7.col=m4;
h7.rowCount=m7;
h7.colCount=g8;
if (type=="Row"){
h7.col=h7.colCount=-1;
}else if (type=="Column"){
h7.row=h7.rowCount=-1;
}else if (type=="Table"){
h7.col=h7.colCount=-1;h7.row=h7.rowCount=-1;
}
}
this.Margin=function (left,top,right,bottom){
this.left;
this.top;
this.right;
this.bottom;
}
this.GetRender=function (h2){
var f7=h2;
if (f7.firstChild!=null&&f7.firstChild.tagName!=null&&f7.firstChild.tagName!="BR")
return f7.firstChild;
if (f7.firstChild!=null&&f7.firstChild.value!=null){
f7=f7.firstChild;
}
return f7;
}
this.GetPreferredRowHeight=function (e4,g9){
var i6=this.CreateTestBox(e4);
g9=this.GetDisplayIndex(e4,g9);
var i3=this.GetViewport(e4);
var i7=0;
var p2=i3.rows[g9].offsetHeight;
var e8=i3.rows[g9].cells.length;
for (var e9=0;e9<e8;e9++){
var i8=i3.rows[g9].cells[e9];
var j0=this.GetRender(i8);
if (j0!=null){
i6.style.fontFamily=j0.style.fontFamily;
i6.style.fontSize=j0.style.fontSize;
i6.style.fontWeight=j0.style.fontWeight;
i6.style.fontStyle=j0.style.fontStyle;
}
var m4=this.GetColFromCell(e4,i8);
i6.style.posWidth=this.GetColWidthFromCol(e4,m4);
if (j0!=null&&j0.tagName=="SELECT"){
var f7="";
for (var h9=0;h9<j0.childNodes.length;h9++){
var p3=j0.childNodes[h9];
if (p3.text!=null&&p3.text.length>f7.length)f7=p3.text;
}
i6.innerHTML=f7;
}
else if (j0!=null&&j0.tagName=="INPUT")
i6.innerHTML=j0.value;
else 
{
i6.innerHTML=i8.innerHTML;
}
p2=i6.offsetHeight;
if (p2>i7)i7=p2;
}
return Math.max(0,i7)+3;
}
this.SetRowHeight2=function (e4,g9,height){
if (height<1){
height=1;
}
g9=this.GetDisplayIndex(e4,g9);
var b5=null;
if (this.GetRowHeader(e4)!=null)b5=this.GetRowHeader(e4).rows[g9];
if (b5!=null){
b5.style.posHeight=height;
b5.cells[0].style.posHeight=height;
}
var i3=this.GetViewport(e4);
if (b5!=null){
i3.rows[b5.rowIndex].cells[0].style.posHeight=b5.style.posHeight;
}else if (i3!=null){
i3.rows[g9].cells[0].style.posHeight=height;
b5=i3.rows[g9];
}
var p4=this.AddRowInfo(e4,b5.FpKey);
if (p4!=null){
this.SetRowHeight(e4,p4,b5.style.posHeight);
}
var i4=this.GetParentSpread(e4);
if (i4!=null)i4.UpdateRowHeight(e4);
this.SizeSpread(e4);
}
this.GetRowHeightInternal=function (e4,g9){
var b5=null;
if (this.GetRowHeader(e4)!=null)
b5=this.GetRowHeader(e4).rows[g9];
else if (this.GetViewport(e4)!=null)
b5=this.GetViewport(e4).rows[g9];
if (b5!=null)
return b5.offsetHeight;
else 
return 0;
}
this.GetCell=function (ele,noHeader,event){
var f7=ele;
while (f7!=null){
if (noHeader){
if ((f7.tagName=="TD"||f7.tagName=="TH")&&(f7.parentNode.getAttribute("FpSpread")=="r")){
return f7;
}
}else {
if ((f7.tagName=="TD"||f7.tagName=="TH")&&(f7.parentNode.getAttribute("FpSpread")=="r"||f7.parentNode.getAttribute("FpSpread")=="ch"||f7.parentNode.getAttribute("FpSpread")=="rh")){
return f7;
}
}
f7=f7.parentNode;
}
return null;
}
this.InRowHeader=function (e4,h2){
return (this.IsChild(h2,this.GetRowHeader(e4)));
}
this.InColHeader=function (e4,h2){
return (this.IsChild(h2,this.GetColHeader(e4)));
}
this.InColFooter=function (e4,h2){
return (this.IsChild(h2,this.GetColFooter(e4)));
}
this.IsHeaderCell=function (e4,h2){
return (h2!=null&&(h2.tagName=="TD"||h2.tagName=="TH")&&(h2.parentNode.getAttribute("FpSpread")=="ch"||h2.parentNode.getAttribute("FpSpread")=="rh"));
}
this.GetSizeColumn=function (e4,ele,event){
if (ele.tagName!="TD"||(this.GetColHeader(e4)==null))return null;
var m4=-1;
var f7=ele;
var p0=this.GetViewport(this.GetTopSpread(e4)).parentNode.scrollLeft+window.scrollX;
while (f7!=null&&f7.parentNode!=null&&f7.parentNode!=document.documentElement){
if (f7.parentNode.getAttribute("FpSpread")=="ch"){
var p5=this.GetOffsetLeft(e4,f7);
var p6=p5+f7.offsetWidth;
if (event.clientX+p0<p5+3){
m4=this.GetColFromCell(e4,f7)-1;
}
else if (event.clientX+p0>p6-4){
m4=this.GetColFromCell(e4,f7);
var p7=this.GetSpanCell(f7.parentNode.rowIndex,m4,e4.e1);
if (p7!=null){
m4=p7.col+p7.colCount-1;
}
}else {
m4=this.GetColFromCell(e4,f7);
var p7=this.GetSpanCell(f7.parentNode.rowIndex,m4,e4.e1);
if (p7!=null){
var i9=p5;
m4=-1;
for (var e9=p7.col;e9<p7.col+p7.colCount&&e9<this.GetColCount(e4);e9++){
if (this.IsChild(f7,this.GetColHeader(e4)))
i9+=parseInt(this.GetElementById(this.GetColHeader(e4),e4.id+"col"+e9).width);
if (event.clientX>i9-3&&event.clientX<i9+3){
m4=e9;
break ;
}
}
}else {
m4=-1;
}
}
if (isNaN(m4)||m4<0)return null;
var p8=0;
var p9=this.GetColCount(e4);
var q0=true;
var e5=null;
var h1=m4+1;
while (h1<p9){
var m3=this.GetColGroup(this.GetColHeader(e4));
if (h1<m3.childNodes.length)
p8=parseInt(m3.childNodes[h1].width);
if (p8>1){
q0=false;
break ;
}
h1++;
}
if (q0){
h1=m4+1;
while (h1<p9){
if (this.GetSizable(e4,h1)){
m4=h1;
break ;
}
h1++;
}
}
if (!this.GetSizable(e4,m4))return null;
if (this.IsChild(f7,this.GetColHeader(e4))){
return this.GetElementById(this.GetColHeader(e4),e4.id+"col"+m4);
}
}
f7=f7.parentNode;
}
return null;
}
this.GetColGroup=function (f7){
if (f7==null)return null;
var m3=f7.getElementsByTagName("COLGROUP");
if (m3!=null&&m3.length>0){
if (f7.colgroup!=null)return f7.colgroup;
var j3=new Object();
j3.childNodes=new Array();
for (var e9=0;e9<m3[0].childNodes.length;e9++){
if (m3[0].childNodes[e9]!=null&&m3[0].childNodes[e9].tagName=="COL"){
var e8=j3.childNodes.length;
j3.childNodes.length++;
j3.childNodes[e8]=m3[0].childNodes[e9];
}
}
f7.colgroup=j3;
return j3;
}else {
return null;
}
}
this.GetSizeRow=function (e4,ele,event){
var m7=this.GetRowCount(e4);
if (m7==0)return null;
var h2=this.GetCell(ele);
if (h2==null){
if (ele.getAttribute("FpSpread")=="rowpadding"){
if (event.clientY<3){
var e8=ele.parentNode.rowIndex;
if (e8>1){
var i5=ele.parentNode.parentNode.rows[e8-1];
if (this.GetSizable(e4,i5))
return i5;
}
}
}
var c4=this.GetCorner(e4);
if (c4!=null&&this.IsChild(ele,c4)){
if (event.clientY>ele.offsetHeight-4){
var q1=null;
var e8=0;
q1=this.GetRowHeader(e4);
if (q1!=null){
while (e8<q1.rows.length&&q1.rows[e8].offsetHeight<2&&!this.GetSizable(e4,q1.rows[e8]))
e8++;
if (e8<q1.rows.length&&this.GetSizable(e4,q1.rows[e8])&&q1.rows[e8].offsetHeight<2)
return q1.rows[e8];
}
}else {
}
}
return null;
}
var e0=e4.e0;
var d9=e4.d9;
var f7=h2;
var o9=this.GetViewport(this.GetTopSpread(e4)).parentNode.scrollTop+window.scrollY;
while (f7!=null&&f7!=document.documentElement){
if (f7.getAttribute("FpSpread")=="rh"){
var e8=-1;
var q2=this.GetOffsetTop(e4,f7);
var q3=q2+f7.offsetHeight;
if (event.clientY+o9<q2+3){
if (f7.rowIndex>1)
e8=f7.rowIndex-1;
}
else if (event.clientY+o9>q3-4){
var p7=this.GetSpanCell(this.GetRowFromCell(e4,h2),this.GetColFromCell(e4,h2),e0);
if (p7!=null){
var j4=q2;
for (var e9=p7.row;e9<p7.row+p7.rowCount;e9++){
if (this.GetRowHeader(e4).rows[e9].cells.length>0)
j4+=parseInt(this.GetRowHeader(e4).rows[e9].cells[0].style.height);
if (event.clientY>j4-3&&event.clientY<j4+3){
e8=e9;
break ;
}
}
}else {
if (f7.rowIndex>=0)e8=f7.rowIndex;
}
}
else {
break ;
}
var j4=0;
var m7=this.GetRowHeader(e4).rows.length;
var q4=true;
var q1=null;
q1=this.GetRowHeader(e4);
var g9=e8+1;
while (g9<m7){
if (q1.rows[g9].style.height!=null)j4=parseInt(q1.rows[g9].style.height);
else j4=parseInt(q1.rows[g9].offsetHeight);
if (j4>1){
q4=false;
break ;
}
g9++;
}
if (q4){
g9=e8+1;
while (g9<m7){
if (this.GetSizable(e4,this.GetRowHeader(e4).rows[g9])){
e8=g9;
break ;
}
g9++;
}
}
if (e8>=0&&this.GetSizable(e4,q1.rows[e8])){
return q1.rows[e8];
}
else if (event.clientY<3){
while (e8>0&&q1.rows[e8].offsetHeight==0&&!this.GetSizable(e4,q1.rows[e8]))
e8--;
if (e8>=0&&this.GetSizable(e4,q1.rows[e8]))
return q1.rows[e8];
else 
return null;
}
}
f7=f7.parentNode;
}
return null;
}
this.GetElementById=function (i4,id){
if (i4==null)return null;
var f7=i4.firstChild;
while (f7!=null){
if (f7.id==id||(typeof(f7.getAttribute)=="function"&&f7.getAttribute("name")==id))return f7;
var j3=this.GetElementById(f7,id)
if (j3!=null)return j3;
f7=f7.nextSibling;
}
return null;
}
this.GetSizable=function (e4,ele){
if (typeof(ele)=="number"){
var h2=this.GetElementById(this.GetColHeader(e4),e4.id+"col"+ele);
return (h2!=null&&(h2.getAttribute("Sizable")==null||h2.getAttribute("Sizable")=="True"));
}
return (ele!=null&&(ele.getAttribute("Sizable")==null||ele.getAttribute("Sizable")=="True"));
}
this.GetSpanWidth=function (e4,m4,p9){
var i9=0;
var e5=this.GetViewport(e4);
if (e5!=null){
var m3=this.GetColGroup(e5);
if (m3!=null){
for (var e9=m4;e9<m4+p9;e9++){
i9+=parseInt(m3.childNodes[e9].width);
}
}
}
return i9;
}
this.GetCellType=function (h2){
if (h2!=null&&h2.getAttribute("FpCellType")!=null)return h2.getAttribute("FpCellType");
if (h2!=null&&h2.getAttribute("FpRef")!=null){
var f7=document.getElementById(h2.getAttribute("FpRef"));
return f7.getAttribute("FpCellType");
}
if (h2!=null&&h2.getAttribute("FpCellType")!=null)return h2.getAttribute("FpCellType");
return "text";
}
this.GetCellType2=function (h2){
if (h2!=null&&h2.getAttribute("FpRef")!=null){
h2=document.getElementById(h2.getAttribute("FpRef"));
}
var q5=null;
if (h2!=null){
q5=h2.getAttribute("FpCellType");
if (q5=="readonly")q5=h2.getAttribute("CellType");
if (q5==null&&h2.getAttribute("CellType2")=="TagCloudCellType")
q5=h2.getAttribute("CellType2");
}
if (q5!=null)return q5;
return "text";
}
this.GetCellEditorID=function (e4,h2){
if (h2!=null&&h2.getAttribute("FpRef")!=null){
var f7=document.getElementById(h2.getAttribute("FpRef"));
return f7.getAttribute("FpEditorID");
}
if (h2.getAttribute("FpEditorID")!=null)
return h2.getAttribute("FpEditorID");
return e4.getAttribute("FpDefaultEditorID");
}
this.EditorMap=function (editorID,a8){
this.id=editorID;
this.a8=a8;
}
this.ValidatorMap=function (validatorID,validator){
this.id=validatorID;
this.validator=validator;
}
this.GetCellEditor=function (e4,editorID,noClone){
var a8=null;
for (var e9=0;e9<this.c0.length;e9++){
var q6=this.c0[e9];
if (q6.id==editorID){
a8=q6.a8;
break ;
}
}
if (a8==null){
a8=document.getElementById(editorID);
this.c0[this.c0.length]=new this.EditorMap(editorID,a8);
}
if (noClone)
return a8;
return a8.cloneNode(true);
}
this.GetCellValidatorID=function (e4,h2){
return null;
}
this.GetCellValidator=function (e4,validatorID){
return null;
}
this.GetTableRow=function (e4,g9){
var f4=this.GetData(e4).getElementsByTagName("root")[0];
var f3=f4.getElementsByTagName("data")[0];
var f7=f3.firstChild;
while (f7!=null){
if (f7.getAttribute("key")==""+g9)return f7;
f7=f7.nextSibling;
}
return null;
}
this.GetTableCell=function (i5,h1){
if (i5==null)return null;
var f7=i5.firstChild;
while (f7!=null){
if (f7.getAttribute("key")==""+h1)return f7;
f7=f7.nextSibling;
}
return null;
}
this.AddTableRow=function (e4,g9){
if (g9==null)return null;
var m6=this.GetTableRow(e4,g9);
if (m6!=null)return m6;
var f4=this.GetData(e4).getElementsByTagName("root")[0];
var f3=f4.getElementsByTagName("data")[0];
if (document.all!=null){
m6=this.GetData(e4).createNode("element","row","");
}else {
m6=document.createElement("row");
m6.style.display="none";
}
m6.setAttribute("key",g9);
f3.appendChild(m6);
return m6;
}
this.AddTableCell=function (i5,h1){
if (i5==null)return null;
var m6=this.GetTableCell(i5,h1);
if (m6!=null)return m6;
if (document.all!=null){
m6=this.GetData(e4).createNode("element","cell","");
}else {
m6=document.createElement("cell");
m6.style.display="none";
}
m6.setAttribute("key",h1);
i5.appendChild(m6);
return m6;
}
this.GetCellValue=function (e4,h2){
if (h2==null)return null;
var g9=this.GetRowKeyFromCell(e4,h2);
var h1=this.GetColKeyFromCell(e4,h2);
var q7=this.AddTableCell(this.AddTableRow(e4,g9),h1);
return q7.innerHTML;
}
this.HTMLEncode=function (s){
var q8=new String(s);
var q9=new RegExp("&","g");
q8=q8.replace(q9,"&amp;");
q9=new RegExp("<","g");
q8=q8.replace(q9,"&lt;");
q9=new RegExp(">","g");
q8=q8.replace(q9,"&gt;");
q9=new RegExp("\"","g");
q8=q8.replace(q9,"&quot;");
return q8;
}
this.HTMLDecode=function (s){
var q8=new String(s);
var q9=new RegExp("&amp;","g");
q8=q8.replace(q9,"&");
q9=new RegExp("&lt;","g");
q8=q8.replace(q9,"<");
q9=new RegExp("&gt;","g");
q8=q8.replace(q9,">");
q9=new RegExp("&quot;","g");
q8=q8.replace(q9,'"');
return q8;
}
this.SetCellValue=function (e4,h2,val,noEvent,recalc){
if (h2==null)return ;
var r0=this.GetCellType(h2);
if (r0=="readonly")return ;
var g9=this.GetRowKeyFromCell(e4,h2);
var h1=this.GetColKeyFromCell(e4,h2);
var q7=this.AddTableCell(this.AddTableRow(e4,g9),h1);
val=this.HTMLEncode(val);
val=this.HTMLEncode(val);
q7.innerHTML=val;
if (!noEvent){
var g0=this.CreateEvent("DataChanged");
g0.cell=h2;
g0.cellValue=val;
g0.row=g9;
g0.col=h1;
this.FireEvent(e4,g0);
}
var f6=this.GetCmdBtn(e4,"Update");
if (f6!=null&&f6.getAttribute("disabled")!=null)
this.UpdateCmdBtnState(f6,false);
f6=this.GetCmdBtn(e4,"Cancel");
if (f6!=null&&f6.getAttribute("disabled")!=null)
this.UpdateCmdBtnState(f6,false);
e4.e2=true;
if (recalc){
this.UpdateValues(e4);
}
}
this.GetSelectedRanges=function (e4){
var m5=this.GetSelection(e4);
var g2=new Array();
var m6=m5.firstChild;
while (m6!=null){
var h7=new this.Range();
this.GetRangeFromNode(e4,m6,h7);
var f7=g2.length;
g2.length=f7+1;
g2[f7]=h7;
m6=m6.nextSibling;
}
return g2;
}
this.GetSelectedRange=function (e4){
var h7=new this.Range();
var m5=this.GetSelection(e4);
var m6=m5.lastChild;
if (m6!=null){
this.GetRangeFromNode(e4,m6,h7);
}
return h7;
}
this.GetRangeFromNode=function (e4,m6,h7){
if (m6==null||e4==null||h7==null)return ;
var g9=this.GetRowByKey(e4,m6.getAttribute("row"));
var h1=this.GetColByKey(e4,m6.getAttribute("col"));
var m7=parseInt(m6.getAttribute("rowcount"));
var g8=parseInt(m6.getAttribute("colcount"));
var i3=this.GetViewport(e4);
if (i3!=null){
var r1=this.GetDisplayIndex(e4,g9);
for (var e9=r1;e9<r1+m7;e9++){
if (this.IsChildSpreadRow(e4,i3,e9))m7--;
}
}
var r2=null;
if (g9<0&&h1<0&&m7!=0&&g8!=0)
r2="Table";
else if (g9<0&&h1>=0&&g8>0)
r2="Column";
else if (h1<0&&g9>=0&&m7>0)
r2="Row";
else 
r2="Cell";
this.SetRange(h7,r2,g9,h1,m7,g8);
}
this.GetSelection=function (e4){
var f3=this.GetData(e4);
var f4=f3.getElementsByTagName("root")[0];
var m8=f4.getElementsByTagName("state")[0];
var r3=m8.getElementsByTagName("selection")[0];
return r3;
}
this.GetRowKeyFromRow=function (e4,g9){
if (g9<0)return null;
var e5=null;
e5=this.GetViewport(e4);
return e5.rows[g9].getAttribute("FpKey");
}
this.GetColKeyFromCol=function (e4,h1){
if (h1<0)return null;
var e5=this.GetViewport(e4);
var m3=this.GetColGroup(e5);
if (m3==null||m3.childNodes.length==0)
m3=this.GetColGroup(this.GetColHeader(e4));
if (m3!=null&&h1>=0&&h1<m3.childNodes.length){
return m3.childNodes[h1].getAttribute("FpCol");
}
return null;
}
this.GetRowKeyFromCell=function (e4,h2){
var g9=h2.parentNode.getAttribute("FpKey");
return g9;
}
this.GetColKeyFromCell=function (e4,h2){
var m4=this.GetColFromCell(e4,h2);
var e5=this.GetViewport(e4);
var m3=this.GetColGroup(e5);
if (m3!=null&&m4>=0&&m4<m3.childNodes.length){
return m3.childNodes[m4].getAttribute("FpCol");
}
return null;
}
this.SetSelection=function (e4,i5,m4,rowcount,colcount,addSelection){
if (!e4.initialized)return ;
var r4=i5;
var r5=m4;
if (i5!=null&&parseInt(i5)>=0){
i5=this.GetRowKeyFromRow(e4,i5);
if (i5!="newRow")
i5=parseInt(i5);
}
if (m4!=null&&parseInt(m4)>=0){
m4=parseInt(this.GetColKeyFromCol(e4,m4));
}
var m6=this.GetSelection(e4);
if (m6==null)return ;
if (addSelection==null)
addSelection=(e4.getAttribute("multiRange")=="true"&&!this.a6);
var r6=m6.lastChild;
if (r6==null||addSelection){
if (document.all!=null){
r6=this.GetData(e4).createNode("element","range","");
}else {
r6=document.createElement('range');
r6.style.display="none";
}
m6.appendChild(r6);
}
r6.setAttribute("row",i5);
r6.setAttribute("col",m4);
r6.setAttribute("rowcount",rowcount);
r6.setAttribute("colcount",colcount);
r6.setAttribute("rowIndex",r4);
r6.setAttribute("colIndex",r5);
e4.e2=true;
this.PaintFocusRect(e4);
var f6=this.GetCmdBtn(e4,"Update");
this.UpdateCmdBtnState(f6,false);
var g0=this.CreateEvent("SelectionChanged");
this.FireEvent(e4,g0);
}
this.CreateSelectionNode=function (e4,i5,m4,rowcount,colcount,r4,r5){
var r6=document.createElement('range');
r6.style.display="none";
r6.setAttribute("row",i5);
r6.setAttribute("col",m4);
r6.setAttribute("rowcount",rowcount);
r6.setAttribute("colcount",colcount);
r6.setAttribute("rowIndex",r4);
r6.setAttribute("colIndex",r5);
return r6;
}
this.AddRowToSelection=function (e4,m6,i5){
var r4=i5;
if (typeof(i5)!="undefined"&&parseInt(i5)>=0){
i5=this.GetRowKeyFromRow(e4,i5);
if (i5!="newRow")
i5=parseInt(i5);
}
if (!this.IsRowSelected(e4,i5)&&!isNaN(i5))
{
var r6=this.CreateSelectionNode(e4,i5,-1,1,-1,r4,-1);
m6.appendChild(r6);
}
}
this.RemoveSelection=function (e4,i5,m4,rowcount,colcount){
var m6=this.GetSelection(e4);
if (m6==null)return ;
var r6=m6.firstChild;
while (r6!=null){
var g9=parseInt(r6.getAttribute("rowIndex"));
var m7=parseInt(r6.getAttribute("rowcount"));
if (g9<=i5&&i5<g9+m7){
m6.removeChild(r6);
for (var e9=g9;e9<g9+m7;e9++){
if (e9!=i5){
this.AddRowToSelection(e4,m6,e9);
}
}
break ;
}
r6=r6.nextSibling;
}
e4.e2=true;
var f6=this.GetCmdBtn(e4,"Update");
this.UpdateCmdBtnState(f6,false);
var g0=this.CreateEvent("SelectionChanged");
this.FireEvent(e4,g0);
}
this.GetColInfo=function (e4,h1){
var f3=this.GetData(e4);
var f4=f3.getElementsByTagName("root")[0];
var m8=f4.getElementsByTagName("state")[0];
var m4=m8.getElementsByTagName("colinfo")[0];
var f7=m4.firstChild;
while (f7!=null){
if (f7.getAttribute("key")==""+h1)return f7;
f7=f7.nextSibling;
}
return null;
}
this.GetColWidthFromCol=function (e4,h1){
var m3=this.GetColGroup(this.GetViewport(e4));
return parseInt(m3.childNodes[h1].width);
}
this.GetColWidth=function (colInfo){
if (colInfo==null)return null;
var m6=colInfo.getElementsByTagName("width")[0];
if (m6!=null)return m6.innerHTML;
return 0;
}
this.AddColInfo=function (e4,h1){
var m6=this.GetColInfo(e4,h1);
if (m6!=null)return m6;
var f3=this.GetData(e4);
var f4=f3.getElementsByTagName("root")[0];
var m8=f4.getElementsByTagName("state")[0];
var m4=m8.getElementsByTagName("colinfo")[0];
if (document.all!=null){
m6=this.GetData(e4).createNode("element","col","");
}else {
m6=document.createElement('col');
m6.style.display="none";
}
m6.setAttribute("key",h1);
m4.appendChild(m6);
return m6;
}
this.SetColWidth=function (e4,m4,width){
if (m4==null)return ;
m4=parseInt(m4);
var j5=this.IsXHTML(e4);
var r7=null;
if (this.GetViewport(e4)!=null){
var m3=this.GetColGroup(this.GetViewport(e4));
if (m3==null||m3.childNodes.length==0){
m3=this.GetColGroup(this.GetColHeader(e4));
}
r7=this.AddColInfo(e4,m3.childNodes[m4].getAttribute("FpCol"));
if (this.GetViewport(e4).cellSpacing=="0"&&this.GetColCount(e4)>1&&this.GetViewport(e4).rules!="rows"){
if (m4==0)width-=1;
}
if (width==0)width=1;
if (m3!=null)
m3.childNodes[m4].width=width;
this.SetWidthFix(this.GetViewport(e4),m4,width);
}
if (this.GetColHeader(e4)!=null){
if (this.GetViewport(e4)!=null){
if (this.GetViewport(e4).cellSpacing=="0"&&this.GetColCount(e4)>1&&this.GetViewport(e4).rules!="rows"){
if (j5){
if (m4==this.colCount-1)width-=1;
}
}
}
if (width<=0)width=1;
document.getElementById(e4.id+"col"+m4).width=width;
this.SetWidthFix(this.GetColHeader(e4),m4,width);
if (this.GetViewport(e4)!=null){
if (this.GetViewport(e4).cellSpacing=="0"&&this.GetColCount(e4)>1&&this.GetViewport(e4).rules!="rows"){
if (m4==this.GetColCount(e4)-1)width+=1;
}
}
}
if (this.GetColFooter(e4)!=null){
var m3=this.GetColGroup(this.GetColFooter(e4));
if (m3==null||m3.childNodes.length==0){
m3=this.GetColGroup(this.GetColHeader(e4));
}
r7=this.AddColInfo(e4,m3.childNodes[m4].getAttribute("FpCol"));
if (this.GetColFooter(e4).cellSpacing=="0"&&this.GetColCount(e4)>1&&this.GetColFooter(e4).rules!="rows"){
if (m4==0)width-=1;
}
if (width==0)width=1;
if (m3!=null)
m3.childNodes[m4].width=width;
this.SetWidthFix(this.GetColFooter(e4),m4,width);
}
var e7=this.GetTopSpread(e4);
this.SizeAll(e7);
this.Refresh(e7);
if (r7!=null){
var m6=r7.getElementsByTagName("width");
if (m6!=null&&m6.length>0)
m6[0].innerHTML=width;
else {
if (document.all!=null){
m6=this.GetData(e4).createNode("element","width","");
}else {
m6=document.createElement('width');
m6.style.display="none";
}
r7.appendChild(m6);
m6.innerHTML=width;
}
}
var f6=this.GetCmdBtn(e4,"Update");
if (f6!=null)this.UpdateCmdBtnState(f6,false);
e4.e2=true;
}
this.SetWidthFix=function (e5,m4,width){
if (e5==null||e5.rows.length==0)return ;
var e9=0;
var r8=0;
var i8=e5.rows[0].cells[0];
var r9=i8.colSpan;
if (r9==null)r9=1;
while (m4>r8+r9){
e9++;
r8=r8+r9;
i8=e5.rows[0].cells[e9];
r9=i8.colSpan;
if (r9==null)r9=1;
}
i8.width=width;
}
this.GetRowInfo=function (e4,g9){
var f3=this.GetData(e4);
var f4=f3.getElementsByTagName("root")[0];
var m8=f4.getElementsByTagName("state")[0];
var i5=m8.getElementsByTagName("rowinfo")[0];
var f7=i5.firstChild;
while (f7!=null){
if (f7.getAttribute("key")==""+g9)return f7;
f7=f7.nextSibling;
}
return null;
}
this.GetRowHeight=function (p4){
if (p4==null)return null;
var s0=p4.getElementsByTagName("height");
if (s0!=null&&s0.length>0)return s0[0].innerHTML;
return 0;
}
this.AddRowInfo=function (e4,g9){
var m6=this.GetRowInfo(e4,g9);
if (m6!=null)return m6;
var f3=this.GetData(e4);
var f4=f3.getElementsByTagName("root")[0];
var m8=f4.getElementsByTagName("state")[0];
var i5=m8.getElementsByTagName("rowinfo")[0];
if (document.all!=null){
m6=this.GetData(e4).createNode("element","row","");
}else {
m6=document.createElement('row');
m6.style.display="none";
}
m6.setAttribute("key",g9);
i5.appendChild(m6);
return m6;
}
this.GetTopSpread=function (g0)
{
if (g0==null)return null;
var g2=this.GetSpread(g0);
if (g2==null)return null;
var f7=g2.parentNode;
while (f7!=null&&f7.tagName!="BODY")
{
if (f7.getAttribute&&f7.getAttribute("FpSpread")=="Spread"){
if (f7.getAttribute("hierView")=="true")
g2=f7;
else 
break ;
}
f7=f7.parentNode;
}
return g2;
}
this.GetParentSpread=function (e4)
{
try {
var f7=e4.parentNode;
while (f7!=null&&f7.getAttribute&&f7.getAttribute("FpSpread")!="Spread")f7=f7.parentNode;
if (f7!=null&&f7.getAttribute&&f7.getAttribute("hierView")=="true")
return f7;
else 
return null;
}catch (g0){
return null;
}
}
this.SetRowHeight=function (e4,p4,height){
if (p4==null)return ;
var m6=p4.getElementsByTagName("height");
if (m6!=null&&m6.length>0)
m6[0].innerHTML=height;
else {
if (document.all!=null){
m6=this.GetData(e4).createNode("element","height","");
}else {
m6=document.createElement('height');
m6.style.display="none";
}
p4.appendChild(m6);
m6.innerHTML=height;
}
var f6=this.GetCmdBtn(e4,"Update");
if (f6!=null)this.UpdateCmdBtnState(f6,false);
e4.e2=true;
}
this.SetActiveRow=function (e4,i5){
if (this.GetRowCount(e4)<1)return ;
if (i5==null)i5=-1;
var f3=this.GetData(e4);
var f4=f3.getElementsByTagName("root")[0];
var m8=f4.getElementsByTagName("state")[0];
var m9=m8.firstChild;
while (m9!=null&&m9.tagName!="activerow"&&m9.tagName!="ACTIVEROW"){
m9=m9.nextSibling;
}
if (m9!=null)
m9.innerHTML=""+i5;
if (i5!=null&&e4.getAttribute("IsNewRow")!="true"&&e4.getAttribute("AllowInsert")=="true"){
var f6=this.GetCmdBtn(e4,"Insert");
this.UpdateCmdBtnState(f6,false);
f6=this.GetCmdBtn(e4,"Add");
this.UpdateCmdBtnState(f6,false);
}else {
var f6=this.GetCmdBtn(e4,"Insert");
this.UpdateCmdBtnState(f6,true);
f6=this.GetCmdBtn(e4,"Add");
this.UpdateCmdBtnState(f6,true);
}
if (i5!=null&&e4.getAttribute("IsNewRow")!="true"&&(e4.getAttribute("AllowDelete")==null||e4.getAttribute("AllowDelete")=="true")){
var f6=this.GetCmdBtn(e4,"Delete");
this.UpdateCmdBtnState(f6,(i5==-1));
}else {
var f6=this.GetCmdBtn(e4,"Delete");
this.UpdateCmdBtnState(f6,true);
}
e4.e2=true;
}
this.SetActiveCol=function (e4,m4){
var f3=this.GetData(e4);
var f4=f3.getElementsByTagName("root")[0];
var m8=f4.getElementsByTagName("state")[0];
var n0=m8.firstChild;
while (n0!=null&&n0.tagName!="activecolumn"&&n0.tagName!="ACTIVECOLUMN"){
n0=n0.nextSibling;
}
if (n0!=null)
n0.innerHTML=""+parseInt(m4);
e4.e2=true;
}
this.GetEditor=function (h2){
if (h2==null)return null;
var r0=this.GetCellType(h2);
if (r0=="readonly")return null;
var i2=h2.getElementsByTagName("DIV");
if (r0=="MultiColumnComboBoxCellType"){
if (i2!=null&&i2.length>0){
var f7=i2[0];
f7.type="div";
return f7;
}
}
var i2=h2.getElementsByTagName("INPUT");
if (i2!=null&&i2.length>0){
var f7=i2[0];
while (f7!=null&&f7.getAttribute&&f7.getAttribute("FpEditor")==null)
f7=f7.parentNode;
if (!f7.getAttribute)f7=null;
return f7;
}
i2=h2.getElementsByTagName("SELECT");
if (i2!=null&&i2.length>0){
var f7=i2[0];
return f7;
}
return null;
}
this.GetPageActiveSpread=function (){
var s1=document.documentElement.getAttribute("FpActiveSpread");
var f7=null;
if (s1!=null)f7=document.getElementById(s1);
return f7;
}
this.GetPageActiveSheetView=function (){
var s1=document.documentElement.getAttribute("FpActiveSheetView");
var f7=null;
if (s1!=null)f7=document.getElementById(s1);
return f7;
}
this.SetPageActiveSpread=function (e4){
if (e4==null)
document.documentElement.setAttribute("FpActiveSpread",null);
else {
document.documentElement.setAttribute("FpActiveSpread",e4.id);
document.documentElement.setAttribute("FpActiveSheetView",e4.id);
}
}
this.DoResize=function (event){
if (the_fpSpread.spreads==null)return ;
var e8=the_fpSpread.spreads.length;
for (var e9=0;e9<e8;e9++){
if (the_fpSpread.spreads[e9]!=null)the_fpSpread.SizeSpread(the_fpSpread.spreads[e9]);
}
}
this.DblClick=function (event){
var h2=this.GetCell(this.GetTarget(event),true,event);
var e4=this.GetSpread(h2);
if (h2!=null&&!this.IsHeaderCell(h2)&&this.GetOperationMode(e4)=="RowMode"&&this.GetEnableRowEditTemplate(e4)=="true"){
var s2=h2.getElementsByTagName("DIV");
if (s2!=null&&s2.length>0&&s2[0].id==e4.id+"_RowEditTemplateContainer")return ;
this.Edit(e4,this.GetRowKeyFromCell(e4,h2));
var f6=this.GetCmdBtn(e4,"Cancel");
if (f6!=null)
this.UpdateCmdBtnState(f6,false);
return ;
}
if (h2!=null&&!this.IsHeaderCell(h2)&&h2==e4.d1)this.StartEdit(e4,h2);
}
this.GetEvent=function (g0){
if (g0!=null)return g0;
return window.event;
}
this.GetTarget=function (g0){
g0=this.GetEvent(g0);
if (g0.target==document){
if (g0.currentTarget!=null)return g0.currentTarget;
}
if (g0.target!=null)return g0.target;
return g0.srcElement;
}
this.StartEdit=function (e4,editCell){
var s3=this.GetOperationMode(e4);
if (s3=="SingleSelect"||s3=="ReadOnly"||this.a7)return ;
if (s3=="RowMode"&&this.GetEnableRowEditTemplate(e4)=="true")return ;
var h2=editCell;
if (h2==null)h2=e4.d1;
if (h2==null)return ;
this.b1=-1;
var i2=this.GetEditor(h2);
if (i2!=null){
this.a7=true;
this.a8=i2;
this.b1=1;
}
var j5=this.IsXHTML(e4);
if (h2!=null){
var g9=this.GetRowFromCell(e4,h2);
var h1=this.GetColFromCell(e4,h2);
var g0=this.CreateEvent("EditStart");
g0.cell=h2;
g0.row=this.GetSheetIndex(e4,g9);
g0.col=h1;
g0.cancel=false;
this.FireEvent(e4,g0);
if (g0.cancel)return ;
var r0=this.GetCellType(h2);
if (r0=="readonly")return ;
if (e4.d1!=h2){
e4.d1=h2;
this.SetActiveRow(e4,this.GetRowKeyFromCell(e4,h2));
this.SetActiveCol(e4,this.GetColKeyFromCell(e4,h2));
}
if (i2==null){
var j0=this.GetRender(h2);
var s4=this.GetValueFromRender(e4,j0);
if (s4==" ")s4="";
this.a9=s4;
this.b0=this.GetFormulaFromCell(h2);
if (this.b0!=null)s4=this.b0;
try {
if (j0!=h2){
j0.style.display="none";
}
else {
j0.innerHTML="";
}
}catch (g0){
return ;
}
var s5=this.GetCellEditorID(e4,h2);
if (s5!=null&&s5.length>0){
this.a8=this.GetCellEditor(e4,s5,true);
if (!this.a8.getAttribute("MccbId")&&!this.a8.getAttribute("Extenders"))
this.a8.style.display="inline";
else 
this.a8.style.display="block";
this.a8.id=s5+"Editor";
}else {
this.a8=document.createElement("INPUT");
this.a8.type="text";
}
this.a8.style.fontFamily=j0.style.fontFamily;
this.a8.style.fontSize=j0.style.fontSize;
this.a8.style.fontWeight=j0.style.fontWeight;
this.a8.style.fontStyle=j0.style.fontStyle;
this.a8.style.textDecoration=j0.style.textDecoration;
this.a8.style.position="";
if (j5){
var s6=h2.clientWidth-2;
var s7=parseInt(h2.style.paddingLeft);
if (!isNaN(s7))
s6-=s7;
s7=parseInt(h2.style.paddingRight);
if (!isNaN(s7))
s6-=s7;
this.a8.style.width=""+s6+"px";
}
else 
this.a8.style.width=h2.clientWidth-2;
this.SaveMargin(h2);
if (this.a8.tagName=="TEXTAREA")
this.a8.style.height=""+(h2.offsetHeight-4)+"px";
if ((this.a8.tagName=="INPUT"&&this.a8.type=="text")||this.a8.tagName=="TEXTAREA"){
if (this.a8.style.backgroundColor==""||this.a8.backColorSet!=null){
var s8="";
if (document.defaultView!=null&&document.defaultView.getComputedStyle!=null)s8=document.defaultView.getComputedStyle(h2,'').getPropertyValue("background-color");
if (s8!="")
this.a8.style.backgroundColor=s8;
else 
this.a8.style.backgroundColor=h2.bgColor;
this.a8.backColorSet=true;
}
if (this.a8.style.color==""||this.a8.colorSet!=null){
var s9="";
if (document.defaultView!=null&&document.defaultView.getComputedStyle!=null)s9=document.defaultView.getComputedStyle(h2,'').getPropertyValue("color");
this.a8.style.color=s9;
this.a8.colorSet=true;
}
this.a8.style.borderWidth="0px";
this.RestoreMargin(this.a8,false);
}
this.b1=0;
h2.insertBefore(this.a8,h2.firstChild);
this.SetEditorValue(this.a8,s4);
if (this.a8.offsetHeight<h2.clientHeight&&this.a8.tagName!="TEXTAREA"){
if (h2.vAlign=="middle")
this.a8.style.posTop+=(h2.clientHeight-this.a8.offsetHeight)/2;
else if (h2.vAlign=="bottom")
this.a8.style.posTop+=(h2.clientHeight-this.a8.offsetHeight);
}
this.SizeAll(this.GetTopSpread(e4));
}
this.SetEditorFocus(this.a8);
if (e4.getAttribute("EditMode")=="replace"){
if ((this.a8.tagName=="INPUT"&&this.a8.type=="text")||this.a8.tagName=="TEXTAREA")
this.a8.select();
}
this.a7=true;
var f6=this.GetCmdBtn(e4,"Update");
if (f6!=null&&f6.disabled)
this.UpdateCmdBtnState(f6,false);
f6=this.GetCmdBtn(e4,"Copy");
if (f6!=null&&!f6.disabled)
this.UpdateCmdBtnState(f6,true);
f6=this.GetCmdBtn(e4,"Paste");
if (f6!=null&&!f6.disabled)
this.UpdateCmdBtnState(f6,true);
f6=this.GetCmdBtn(e4,"Clear");
if (f6!=null&&!f6.disabled)
this.UpdateCmdBtnState(f6,true);
}
this.ScrollView(e4);
}
this.GetCurrency=function (validator){
var t0=validator.CurrencySymbol;
if (t0!=null)return t0;
var f7=document.getElementById(validator.id+"cs");
if (f7!=null){
return f7.innerText;
}
return "";
}
this.GetValueFromRender=function (e4,rd){
var q5=this.GetCellType2(this.GetCell(rd));
if (q5!=null){
if (q5=="text")q5="TextCellType";
var i1=null;
if (q5=="ExtenderCellType"){
i1=this.GetFunction(q5+"_getEditor")
if (i1!=null){
if (i1(rd)!=null)
i1=this.GetFunction(q5+"_getEditorValue");
else 
i1=null;
}
}else 
i1=this.GetFunction(q5+"_getValue");
if (i1!=null){
return i1(rd,e4);
}
}
var f7=rd;
while (f7.firstChild!=null&&f7.firstChild.nodeName!="#text")f7=f7.firstChild;
if (f7.innerHTML=="&nbsp;")return "";
var s4=f7.value;
if ((typeof(s4)=="undefined")&&q5=="readonly"&&f7.parentNode!=null&&f7.parentNode.getAttribute("CellType2")=="TagCloudCellType")
s4=f7.textContent;
if (s4==null){
s4=this.ReplaceAll(f7.innerHTML,"&nbsp;"," ");
s4=this.ReplaceAll(s4,"<br>"," ");
s4=this.HTMLDecode(s4);
}
return s4;
}
this.ReplaceAll=function (val,src,dest){
if (val==null)return val;
var t1=val.length;
while (true){
val=val.replace(src,dest);
if (val.length==t1)break ;
t1=val.length;
}
return val;
}
this.GetFormula=function (e4,g9,h1){
g9=this.GetDisplayIndex(e4,g9);
var h2=this.GetCellFromRowCol(e4,g9,h1);
var t2=this.GetFormulaFromCell(h2);
return t2;
}
this.SetFormula=function (e4,g9,h1,i1,recalc,clientOnly){
g9=this.GetDisplayIndex(e4,g9);
var h2=this.GetCellFromRowCol(e4,g9,h1);
h2.setAttribute("FpFormula",i1);
if (!clientOnly)
this.SetCellValue(e4,h2,i1,null,recalc);
}
this.GetFormulaFromCell=function (rd){
var s4=null;
if (rd.getAttribute("FpFormula")!=null){
s4=rd.getAttribute("FpFormula");
}
if (s4!=null)
s4=this.Trim(new String(s4));
return s4;
}
this.IsDouble=function (val,decimalchar,negsign,possign,minimumvalue,maximumvalue){
if (val==null||val.length==0)return true;
val=val.replace(" ","");
if (val.length==0)return true;
if (negsign!=null)val=val.replace(negsign,"-");
if (possign!=null)val=val.replace(possign,"+");
if (val.charAt(val.length-1)=="-")val="-"+val.substring(0,val.length-1);
var t3=new RegExp("^\\s*([-\\+])?(\\d+)?(\\"+decimalchar+"(\\d+))?([eE]([-\\+])?(\\d+))?\\s*$");
var t4=val.match(t3);
if (t4==null)
return false;
if ((t4[2]==null||t4[2].length==0)&&(t4[4]==null||t4[4].length==0))return false;
var t5="";
if (t4[1]!=null&&t4[1].length>0)t5=t4[1];
if (t4[2]!=null&&t4[2].length>0)
t5+=t4[2];
else 
t5+="0";
if (t4[4]!=null&&t4[4].length>0)
t5+=("."+t4[4]);
if (t4[6]!=null&&t4[6].length>0){
t5+=("E"+t4[6]);
if (t4[7]!=null)
t5+=(t4[7]);
else 
t5+="0";
}
var t6=parseFloat(t5);
if (isNaN(t6))return false;
var f7=true;
if (minimumvalue!=null){
var t7=parseFloat(minimumvalue);
f7=(!isNaN(t7)&&t6>=t7);
}
if (f7&&maximumvalue!=null){
var i7=parseFloat(maximumvalue);
f7=(!isNaN(i7)&&t6<=i7);
}
return f7;
}
this.GetFunction=function (fn){
if (fn==null||fn=="")return null;
try {
var f7=eval(fn);
return f7;
}catch (g0){
return null;
}
}
this.SetValueToRender=function (rd,val,valueonly){
var i1=null;
var q5=this.GetCellType2(this.GetCell(rd));
if (q5!=null){
if (q5=="text")q5="TextCellType";
if (q5=="ExtenderCellType"){
i1=this.GetFunction(q5+"_getEditor")
if (i1!=null){
if (i1(rd)!=null)
i1=this.GetFunction(q5+"_setEditorValue");
else 
i1=null;
}
}else 
i1=this.GetFunction(q5+"_setValue");
}
if (i1!=null){
i1(rd,val);
}else {
if (typeof(rd.value)!="undefined"){
if (val==null)val="";
rd.value=val;
}else {
var f7=rd;
while (f7.firstChild!=null&&f7.firstChild.nodeName!="#text")f7=f7.firstChild;
f7.innerHTML=this.ReplaceAll(val," ","&nbsp;");
}
}
if ((valueonly==null||!valueonly)&&rd.getAttribute("FpFormula")!=null){
rd.setAttribute("FpFormula",val);
}
}
this.Trim=function (r1){
var t4=r1.match(new RegExp("^\\s*(\\S+(\\s+\\S+)*)\\s*$"));
return (t4==null)?"":t4[1];
}
this.GetOffsetLeft=function (e4,h2,i4){
var e5=i4;
if (e5==null)e5=this.GetViewportFromCell(e4,h2);
var p5=0;
var f7=h2;
while (f7!=null&&f7!=e5){
p5+=f7.offsetLeft;
f7=f7.offsetParent;
}
return p5;
}
this.GetOffsetTop=function (e4,h2,i4){
var e5=i4;
if (e5==null)e5=this.GetViewportFromCell(e4,h2);
var t8=0;
var f7=h2;
while (f7!=null&&f7!=e5){
t8+=f7.offsetTop;
f7=f7.offsetParent;
}
return t8;
}
this.SetEditorFocus=function (f7){
if (f7==null)return ;
var t9=true;
var h2=this.GetCell(f7,true);
var q5=this.GetCellType(h2);
if (q5!=null){
var i1=this.GetFunction(q5+"_setFocus");
if (i1!=null){
i1(f7);
t9=false;
}
}
if (t9){
try {
f7.focus();
}catch (g0){}
}
}
this.SetEditorValue=function (f7,val){
var h2=this.GetCell(f7,true);
var q5=this.GetCellType(h2);
if (q5!=null){
var i1=this.GetFunction(q5+"_setEditorValue");
if (i1!=null){
i1(f7,val);
return ;
}
}
f7.value=val;
}
this.GetEditorValue=function (f7){
var h2=this.GetCell(f7,true);
var q5=this.GetCellType(h2);
if (q5!=null){
var i1=this.GetFunction(q5+"_getEditorValue");
if (i1!=null){
return i1(f7);
}
}
if (f7.type=="checkbox"){
if (f7.checked)
return "True";
else 
return "False";
}
else 
{
return f7.value;
}
}
this.CreateMsg=function (){
if (this.b2!=null)return ;
var f7=this.b2=document.createElement("div");
f7.style.position="absolute";
f7.style.background="yellow";
f7.style.color="red";
f7.style.border="1px solid black";
f7.style.display="none";
f7.style.width="120px";
}
this.SetMsg=function (msg){
this.CreateMsg();
this.b2.innerHTML=msg;
}
this.ShowMsg=function (show){
this.CreateMsg();
if (show){
this.b2.style.display="block";
}
else 
this.b2.style.display="none";
}
this.EndEdit=function (){
if (this.a8!=null&&this.a8.parentNode!=null){
var h2=this.GetCell(this.a8.parentNode);
var e4=this.GetSpread(h2,false);
if (e4==null)return true;
var u0=this.GetEditorValue(this.a8);
var u1=u0;
if (typeof(u0)=="string")u1=this.Trim(u0);
var u2=(e4.getAttribute("AcceptFormula")=="true"&&u1!=null&&u1.charAt(0)=='=');
var i2=(this.b1!=0);
if (!u2&&!i2){
var u3=null;
var q5=this.GetCellType(h2);
if (q5!=null){
var i1=this.GetFunction(q5+"_isValid");
if (i1!=null){
u3=i1(h2,u0);
}
}
if (u3!=null&&u3!=""){
this.SetMsg(u3);
this.GetViewport(e4).parentNode.insertBefore(this.b2,this.GetViewport(e4).parentNode.firstChild);
this.ShowMsg(true);
this.SetValidatorPos(e4);
this.a8.focus();
return false;
}else {
this.ShowMsg(false);
}
}
if (!i2){
h2.removeChild(this.a8);
var u4=this.GetRender(h2);
if (u4.style.display=="none")u4.style.display="block";
if (this.b0!=null&&this.b0==u0){
this.SetValueToRender(u4,this.a9,true);
}else {
this.SetValueToRender(u4,u0);
}
this.RestoreMargin(h2);
}
if ((this.b0!=null&&this.b0!=u0)||(this.b0==null&&this.a9!=u0)){
this.SetCellValue(e4,h2,u0);
if (u0!=null&&u0.length>0&&u0.indexOf("=")==0)h2.setAttribute("FpFormula",u0);
}
if (!i2)
this.SizeAll(this.GetTopSpread(e4));
this.a8=null;
this.a7=false;
var g0=this.CreateEvent("EditStopped");
g0.cell=h2;
this.FireEvent(e4,g0);
this.Focus(e4);
var u5=e4.getAttribute("autoCalc");
if (u5!="false"){
if ((this.b0!=null&&this.b0!=u0)||(this.b0==null&&this.a9!=u0)){
this.UpdateValues(e4);
}
}
}
this.b1=-1;
return true;
}
this.SetValidatorPos=function (e4){
if (this.a8==null)return ;
var h2=this.GetCell(this.a8.parentNode);
if (h2==null)return ;
var f7=this.b2;
if (f7!=null&&f7.style.display!="none"){
if (f7!=null){
f7.style.left=""+(h2.offsetLeft)+"px";
f7.style.top=""+(h2.offsetTop+h2.offsetHeight)+"px";
}
}
}
this.SaveMargin=function (editCell){
if (editCell.style.paddingLeft!=null&&editCell.style.paddingLeft!=""){
this.b3.left=editCell.style.paddingLeft;
editCell.style.paddingLeft=0;
}
if (editCell.style.paddingRight!=null&&editCell.style.paddingRight!=""){
this.b3.right=editCell.style.paddingRight;
editCell.style.paddingRight=0;
}
if (editCell.style.paddingTop!=null&&editCell.style.paddingTop!=""){
this.b3.top=editCell.style.paddingTop;
editCell.style.paddingTop=0;
}
if (editCell.style.paddingBottom!=null&&editCell.style.paddingBottom!=""){
this.b3.bottom=editCell.style.paddingBottom;
editCell.style.paddingBottom=0;
}
}
this.RestoreMargin=function (h2,reset){
if (this.b3.left!=null&&this.b3.left!=-1){
h2.style.paddingLeft=this.b3.left;
if (reset==null||reset)this.b3.left=-1;
}
if (this.b3.right!=null&&this.b3.right!=-1){
h2.style.paddingRight=this.b3.right;
if (reset==null||reset)this.b3.right=-1;
}
if (this.b3.top!=null&&this.b3.top!=-1){
h2.style.paddingTop=this.b3.top;
if (reset==null||reset)this.b3.top=-1;
}
if (this.b3.bottom!=null&&this.b3.bottom!=-1){
h2.style.paddingBottom=this.b3.bottom;
if (reset==null||reset)this.b3.bottom=-1;
}
}
this.PaintSelectedCell=function (e4,h2,select,anchor){
if (h2==null)return ;
var u6=anchor?e4.getAttribute("anchorBackColor"):e4.getAttribute("selectedBackColor");
if (select){
if (h2.getAttribute("bgColorBak")==null)
h2.setAttribute("bgColorBak",document.defaultView.getComputedStyle(h2,"").getPropertyValue("background-color"));
if (h2.bgColor1==null)
h2.bgColor1=h2.style.backgroundColor;
h2.style.backgroundColor=u6;
if (h2.getAttribute("bgSelImg"))
h2.style.backgroundImage=h2.getAttribute("bgSelImg");
}else {
if (h2.bgColor1!=null)
h2.style.backgroundColor="";
if (h2.bgColor1!=null&&h2.bgColor1!="")
h2.style.backgroundColor=h2.bgColor1;
h2.style.backgroundImage="";
if (h2.getAttribute("bgImg")!=null)
h2.style.backgroundImage=h2.getAttribute("bgImg");
}
}
this.PaintAnchorCell=function (e4){
var u7=this.GetOperationMode(e4);
if (e4.d1==null||(u7!="Normal"&&u7!="RowMode"))return ;
if (u7=="MultiSelect"||u7=="ExtendedSelect")return ;
if (!this.IsChild(e4.d1,e4))return ;
var u8=(this.GetTopSpread(e4).getAttribute("hierView")!="true");
if (e4.getAttribute("showFocusRect")=="false")u8=false;
if (u8){
this.PaintSelectedCell(e4,e4.d1,false);
this.PaintFocusRect(e4);
this.PaintAnchorCellHeader(e4,true);
return ;
}
var f7=e4.d1.parentNode.cells[0].firstChild;
if (f7!=null&&f7.nodeName!="#text"&&f7.getAttribute("FpSpread")=="Spread")return ;
this.PaintSelectedCell(e4,e4.d1,true,true);
this.PaintAnchorCellHeader(e4,true);
}
this.ClearSelection=function (e4,thisonly){
var u9=this.GetParentSpread(e4);
if (thisonly==null&&u9!=null&&u9.getAttribute("hierView")=="true"){
this.ClearSelection(u9);
return ;
}
var i3=this.GetViewport(e4);
var g6=this.GetRowCount(e4);
if (i3!=null&&i3.rows.length>g6){
for (var e9=0;e9<i3.rows.length;e9++){
if (i3.rows[e9].cells.length>0&&i3.rows[e9].cells[0]!=null&&i3.rows[e9].cells[0].firstChild!=null&&i3.rows[e9].cells[0].firstChild.nodeName!="#text"){
var f7=i3.rows[e9].cells[0].firstChild;
if (f7.getAttribute("FpSpread")=="Spread"){
this.ClearSelection(f7,true);
}
}
}
}
this.DoclearSelection(e4);
if (e4.d1!=null){
var s3=this.GetOperationMode(e4);
if (s3=="RowMode"||s3=="SingleSelect"||s3=="ExtendedSelect"||s3=="MultiSelect"){
var h3=this.GetRowFromCell(e4,e4.d1);
this.PaintSelection(e4,h3,-1,1,-1,false);
}
this.PaintSelectedCell(e4,e4.d1,false);
this.PaintAnchorCellHeader(e4,false);
}else {
var h2=this.GetCellFromRowCol(e4,1,0);
if (h2!=null)this.PaintSelectedCell(e4,h2,false);
}
this.PaintFocusRect(e4);
e4.selectedCols=[];
e4.e2=true;
}
this.SetSelectedRange=function (e4,g9,h1,m7,g8){
this.ClearSelection(e4);
var g9=this.GetDisplayIndex(e4,g9);
var v0=0;
var v1=m7;
var i3=this.GetViewport(e4);
if (i3!=null){
for (e9=g9;e9<i3.rows.length&&v0<v1;e9++){
if (this.IsChildSpreadRow(e4,i3,e9)){;
m7++;
}else {
v0++;
}
}
}
this.PaintSelection(e4,g9,h1,m7,g8,true);
this.SetSelection(e4,g9,h1,m7,g8);
}
this.AddSelection=function (e4,g9,h1,m7,g8){
var g9=this.GetDisplayIndex(e4,g9);
var v0=0;
var v1=m7;
var i3=this.GetViewport(e4);
if (i3!=null){
for (e9=g9;e9<i3.rows.length&&v0<v1;e9++){
if (this.IsChildSpreadRow(e4,i3,e9)){;
m7++;
}else {
v0++;
}
}
}
this.PaintSelection(e4,g9,h1,m7,g8,true);
this.SetSelection(e4,g9,h1,m7,g8,true);
}
this.SelectRow=function (e4,index,v0,select,ignoreAnchor){
e4.d5=index;
e4.d6=-1;
if (!ignoreAnchor)this.UpdateAnchorCell(e4,index,0,false);
e4.d7="r";
this.PaintSelection(e4,index,-1,v0,-1,select);
if (select)
{
this.SetSelection(e4,index,-1,v0,-1);
}else {
this.RemoveSelection(e4,index,-1,v0,-1);
this.PaintFocusRect(e4);
}
}
this.SelectColumn=function (e4,index,v0,select,ignoreAnchor){
e4.d5=-1;
e4.d6=index;
if (!ignoreAnchor)this.UpdateAnchorCell(e4,0,index,false);
e4.d7="c";
this.PaintSelection(e4,-1,index,-1,v0,select);
if (select)
{
this.SetSelection(e4,-1,index,-1,v0);
this.AddColSelection(e4,index);
}
}
this.AddColSelection=function (e4,index){
var v2=0;
for (var e9=0;e9<e4.selectedCols.length;e9++){
if (e4.selectedCols[e9]==index)return ;
if (index>e4.selectedCols[e9])v2=e9+1;
}
e4.selectedCols.length++;
for (var e9=e4.selectedCols.length-1;e9>v2;e9--)
e4.selectedCols[e9]=e4.selectedCols[e9-1];
e4.selectedCols[v2]=index;
}
this.IsColSelected=function (e4,r5){
for (var e9=0;e9<e4.selectedCols.length;e9++)
if (e4.selectedCols[e9]==r5)return true;
return false;
}
this.SyncColSelection=function (e4){
e4.selectedCols=[];
var v3=this.GetSelectedRanges(e4);
for (var e9=0;e9<v3.length;e9++){
var h7=v3[e9];
if (h7.type=="Column"){
for (var h9=h7.col;h9<h7.col+h7.colCount;h9++){
this.AddColSelection(e4,h9);
}
}
}
}
this.InitMovingCol=function (e4,r5,isGroupBar,n4){
if (e4.getAttribute("LayoutMode")&&r5==-1)return ;
if (this.GetOperationMode(e4)!="Normal"){
e4.selectedCols=[];
e4.selectedCols.push(r5);
}
if (isGroupBar){
this.dragCol=r5;
this.dragViewCol=this.GetColByKey(e4,r5);
}else {
this.dragCol=parseInt(this.GetSheetColIndex(e4,r5));
this.dragViewCol=r5;
}
var v4=this.GetMovingCol(e4);
if (isGroupBar){
this.ClearSelection(e4);
v4.innerHTML="";
var v5=document.createElement("DIV");
v5.innerHTML=n4.innerHTML;
v5.style.borderTop="0px solid";
v5.style.borderLeft="0px solid";
v5.style.borderRight="#808080 1px solid";
v5.style.borderBottom="#808080 1px solid";
v5.style.width=""+Math.max(this.GetPreferredCellWidth(e4,n4),80)+"px";
v4.appendChild(v5);
if (e4.getAttribute("DragColumnCssClass")==null){
v4.style.backgroundColor=n4.style.backgroundColor;
v4.style.paddingTop="1px";
v4.style.paddingBottom="1px";
}
v4.style.top="-50px";
v4.style.left="-100px";
}else {
var v6=0;
v4.style.top="0px";
v4.style.left="-1000px";
v4.style.display="";
v4.innerHTML="";
var v7=document.createElement("TABLE");
v4.appendChild(v7);
var i5=document.createElement("TR");
v7.appendChild(i5);
for (var e9=0;e9<e4.selectedCols.length;e9++){
var h2=document.createElement("TD");
i5.appendChild(h2);
var v8;
var v9;
if (e4.getAttribute("columnHeaderAutoTextIndex")!=null)
v8=parseInt(e4.getAttribute("columnHeaderAutoTextIndex"));
else 
v8=e4.getAttribute("ColHeaders")-1;
v9=e4.selectedCols[e9];
var w0=this.GetHeaderCellFromRowCol(e4,v8,v9,true);
if (w0.getAttribute("FpCellType")=="ExtenderCellType"&&w0.getElementsByTagName("DIV").length>0){
var w1=this.GetEditor(w0);
var w2=this.GetFunction("ExtenderCellType_getEditorValue");
if (w1!==null&&w2!==null){
h2.innerHTML=w2(w1);
}
}
else 
h2.innerHTML=w0.innerHTML;
h2.style.cssText=w0.style.cssText;
h2.style.borderTop="0px solid";
h2.style.borderLeft="0px solid";
h2.style.borderRight="#808080 1px solid";
h2.style.borderBottom="#808080 1px solid";
h2.style.whiteSpace="nowrap";
h2.setAttribute("align","center");
var i9=Math.max(this.GetPreferredCellWidth(e4,w0),80);
h2.style.width=""+i9+"px";
v6+=i9;
}
if (e4.getAttribute("DragColumnCssClass")==null){
v4.style.backgroundColor=e4.getAttribute("SelectedBackColor");
v4.style.tableLayout="fixed";
v4.style.width=""+v6+"px";
}
}
e4.selectedCols.context=[];
var w3=e4.selectedCols.context;
var p5=0;
m3=this.GetColGroup(this.GetColHeader(e4));
if (m3!=null){
for (var e9=0;e9<m3.childNodes.length;e9++){
var w4=m3.childNodes[e9].offsetWidth;
w3.push({left:p5,width:w4});
p5+=w4;
}
}
}
this.SelectTable=function (e4,select){
if (select)this.UpdateAnchorCell(e4,0,0,false);
e4.d7="t";
this.PaintSelection(e4,-1,-1,-1,-1,select);
if (select)
{
this.SetSelection(e4,-1,-1,-1,-1);
}
}
this.GetSpanCell=function (g9,h1,span){
if (span==null){
return null;
}
var v0=span.length;
for (var e9=0;e9<v0;e9++){
var p7=span[e9];
var w5=(p7.row<=g9&&g9<p7.row+p7.rowCount&&p7.col<=h1&&h1<p7.col+p7.colCount);
if (w5)return p7;
}
return null;
}
this.IsCovered=function (e4,g9,h1,span){
var p7=this.GetSpanCell(g9,h1,span);
if (p7==null){
return false;
}else {
if (p7.row==g9&&p7.col==h1)return false;
return true;
}
}
this.IsSpanCell=function (e4,g9,h1){
var d9=e4.d9;
var v0=d9.length;
for (var e9=0;e9<v0;e9++){
var p7=d9[e9];
var w5=(p7.row==g9&&p7.col==h1);
if (w5)return p7;
}
return null;
}
this.SelectRange=function (e4,g9,h1,m7,g8,select){
e4.d7="";
this.UpdateRangeSelection(e4,g9,h1,m7,g8,select);
if (select){
this.SetSelection(e4,g9,h1,m7,g8);
this.PaintAnchorCell(e4);
}
}
this.UpdateRangeSelection=function (e4,g9,h1,m7,g8,select){
var i3=this.GetViewport(e4);
this.UpdateRangeSelection(e4,g9,h1,m7,g8,select,i3);
}
this.GetSpanCells=function (e4,i3){
if (i3==this.GetViewport(e4))
return e4.d9;
else if (i3==this.GetColHeader(e4))
return e4.e1;
else if (i3==this.GetColFooter(e4))
return e4.footerSpanCells;
else if (i3==this.GetRowHeader(e4))
return e4.e0;
return null;
}
this.UpdateRangeSelection=function (e4,g9,h1,m7,g8,select,i3){
if (i3==null)return ;
for (var e9=g9;e9<g9+m7&&e9<i3.rows.length;e9++){
if (this.IsChildSpreadRow(e4,i3,e9))continue ;
var w6=this.GetCellIndex(e4,e9,h1,this.GetSpanCells(e4,i3));
for (var h9=0;h9<g8;h9++){
if (this.IsCovered(e4,e9,h1+h9,this.GetSpanCells(e4,i3)))continue ;
if (w6<i3.rows[e9].cells.length){
this.PaintSelectedCell(e4,i3.rows[e9].cells[w6],select);
}
w6++;
}
}
}
this.GetColFromCell=function (e4,h2){
if (h2==null)return -1;
var g9=this.GetRowFromCell(e4,h2);
return this.GetColIndex(e4,g9,h2.cellIndex,this.GetSpanCells(e4,h2.parentNode.parentNode.parentNode),false,this.IsChild(h2,this.GetRowHeader(e4)));
}
this.GetRowFromCell=function (e4,h2){
if (h2==null||h2.parentNode==null)return -1;
var g9=h2.parentNode.rowIndex;
return g9;
}
this.GetColIndex=function (e4,e9,cellIndex,span,frozArea,c5){
var w7=false;
var e5=this.GetViewport(e4);
if (e5!=null)w7=e5.parentNode.getAttribute("hiddenCells");
if (w7&&span==e4.d9){
return cellIndex;
}
var w8=0;
var v0=this.GetColCount(e4);
var w9=0;
if (c5){
w9=0;
var m3=null;
if (this.GetRowHeader(e4)!=null)
m3=this.GetColGroup(this.GetRowHeader(e4));
if (m3!=null)
v0=m3.childNodes.length;
}
for (var h9=w9;h9<v0;h9++){
if (this.IsCovered(e4,e9,h9,span))continue ;
if (w8==cellIndex){
return h9;
}
w8++;
}
return v0;
}
this.GetCellIndex=function (e4,e9,r5,span){
var w7=false;
var e5=this.GetViewport(e4);
if (e5!=null)w7=e5.parentNode.getAttribute("hiddenCells");
if (w7&&span==e4.d9){
return r5;
}else {
var w9=0;
var v0=r5;
var w8=0;
for (var h9=0;h9<v0;h9++){
if (this.IsCovered(e4,e9,w9+h9,span))continue ;
w8++;
}
return w8;
}
}
this.NextCell=function (e4,event,key){
if (event.altKey)return ;
var x0=this.GetParent(this.GetViewport(e4));
if (e4.d1==null){
var i1=this.FireActiveCellChangingEvent(e4,0,0);
if (!i1){
e4.SetActiveCell(0,0);
var g0=this.CreateEvent("ActiveCellChanged");
g0.cmdID=e4.id;
g0.row=g0.Row=0;
g0.col=g0.Col=0;
this.FireEvent(e4,g0);
}
return ;
}
if (event.shiftKey&&key!=this.tab){
var p3=this.GetOperationMode(e4);
if (p3=="RowMode"||p3=="SingleSelect"||p3=="MultiSelect"||(p3=="Normal"&&this.GetSelectionPolicy(e4)=="Single"))return ;
var p7=this.GetSpanCell(e4.d3,e4.d4,this.GetSpanCells(e4,this.GetViewportFromCell(e4,e4.d1)));
switch (key){
case this.right:
var g9=e4.d3;
var h1=e4.d4+1;
if (p7!=null){
h1=p7.col+p7.colCount;
}
if (h1>this.GetColCount(e4)-1)return ;
e4.d4=h1;
e4.d2=this.GetCellFromRowCol(e4,g9,h1);
this.Select(e4,e4.d1,e4.d2);
break ;
case this.left:
var g9=e4.d3;
var h1=e4.d4-1;
if (p7!=null){
h1=p7.col-1;
}
p7=this.GetSpanCell(g9,h1,this.GetSpanCells(e4,this.GetViewportFromCell(e4,e4.d1)));
if (p7!=null){
if (this.IsSpanCell(e4,g9,p7.col))h1=p7.col;
}
if (h1<0)return ;
e4.d4=h1;
e4.d2=this.GetCellFromRowCol(e4,g9,h1);
this.Select(e4,e4.d1,e4.d2);
break ;
case this.down:
var g9=e4.d3+1;
var h1=e4.d4;
if (p7!=null){
g9=p7.row+p7.rowCount;
}
g9=this.GetNextRow(e4,g9);
if (g9>this.GetRowCountInternal(e4)-1)return ;
e4.d3=g9;
e4.d2=this.GetCellFromRowCol(e4,g9,h1);
this.Select(e4,e4.d1,e4.d2);
break ;
case this.up:
var g9=e4.d3-1;
var h1=e4.d4;
if (p7!=null){
g9=p7.row-1;
}
g9=this.GetPrevRow(e4,g9);
p7=this.GetSpanCell(g9,h1,this.GetSpanCells(e4,this.GetViewportFromCell(e4,e4.d1)));
if (p7!=null){
if (this.IsSpanCell(e4,p7.row,h1))g9=p7.row;
}
if (g9<0)return ;
e4.d3=g9;
e4.d2=this.GetCellFromRowCol(e4,g9,h1);
this.Select(e4,e4.d1,e4.d2);
break ;
case this.home:
if (e4.d1.parentNode.rowIndex>=0){
e4.d4=0;
e4.d2=this.GetCellFromRowCol(e4,e4.d3,e4.d4);
this.Select(e4,e4.d1,e4.d2);
}
break ;
case this.end:
if (e4.d1.parentNode.rowIndex>=0){
e4.d4=this.GetColCount(e4)-1;
e4.d2=this.GetCellFromRowCol(e4,e4.d3,e4.d4);
this.Select(e4,e4.d1,e4.d2);
}
break ;
case this.pdn:
if (this.GetViewport(e4)!=null&&e4.d1.parentNode.rowIndex>=0){
g9=0;
for (g9=0;g9<this.GetViewport(e4).rows.length;g9++){
if (this.GetViewport(e4).rows[g9].offsetTop+this.GetViewport(e4).rows[g9].offsetHeight>this.GetViewport(e4).parentNode.offsetHeight+this.GetViewport(e4).parentNode.scrollTop){
break ;
}
}
g9=this.GetNextRow(e4,g9);
if (g9<this.GetViewport(e4).rows.length){
this.GetViewport(e4).parentNode.scrollTop=this.GetViewport(e4).rows[g9].offsetTop;
e4.d3=g9;
}else {
g9=this.GetRowCountInternal(e4)-1;
e4.d3=g9;
}
e4.d2=this.GetCellFromRowCol(e4,e4.d3,e4.d4);
this.Select(e4,e4.d1,e4.d2);
}
break ;
case this.pup:
if (this.GetViewport(e4)!=null&&e4.d1.parentNode.rowIndex>0){
g9=0;
for (g9=0;g9<this.GetViewport(e4).rows.length;g9++){
if (this.GetViewport(e4).rows[g9].offsetTop+this.GetViewport(e4).rows[g9].offsetHeight>this.GetViewport(e4).parentNode.scrollTop){
break ;
}
}
if (g9<this.GetViewport(e4).rows.length){
var j4=0;
while (g9>0){
j4+=this.GetViewport(e4).rows[g9].offsetHeight;
if (j4>this.GetViewport(e4).parentNode.offsetHeight){
break ;
}
g9--;
}
g9=this.GetPrevRow(e4,g9);
if (g9>=0){
this.GetViewport(e4).parentNode.scrollTop=this.GetViewport(e4).rows[g9].offsetTop;
e4.d3=g9;
e4.d2=this.GetCellFromRowCol(e4,e4.d3,e4.d4);
this.Select(e4,e4.d1,e4.d2);
}
}
}
break ;
}
this.SyncColSelection(e4);
}else {
if (key==this.tab){
if (event.shiftKey)key=this.left;
else key=this.right;
}
var x1=e4.d1;
var g9=e4.d3;
var h1=e4.d4;
switch (key){
case this.right:
if (event.keyCode==this.tab){
var x2=g9;
var x3=h1;
do {
this.MoveRight(e4,g9,h1);
g9=e4.d3;
h1=e4.d4;
}while (!(x2==g9&&x3==h1)&&this.GetCellFromRowCol(e4,g9,h1).getAttribute("TabStop")!=null&&this.GetCellFromRowCol(e4,g9,h1).getAttribute("TabStop")=="false")
}
else {
this.MoveRight(e4,g9,h1);
}
break ;
case this.left:
if (event.keyCode==this.tab){
var x2=g9;
var x3=h1;
do {
this.MoveLeft(e4,g9,h1);
g9=e4.d3;
h1=e4.d4;
}while (!(x2==g9&&x3==h1)&&this.GetCellFromRowCol(e4,g9,h1).getAttribute("TabStop")!=null&&this.GetCellFromRowCol(e4,g9,h1).getAttribute("TabStop")=="false")
}
else {
this.MoveLeft(e4,g9,h1);
}
break ;
case this.down:
this.MoveDown(e4,g9,h1);
break ;
case this.up:
this.MoveUp(e4,g9,h1);
break ;
case this.home:
if (e4.d1.parentNode.rowIndex>=0){
this.UpdateLeadingCell(e4,g9,0);
}
break ;
case this.end:
if (e4.d1.parentNode.rowIndex>=0){
h1=this.GetColCount(e4)-1;
this.UpdateLeadingCell(e4,g9,h1);
}
break ;
case this.pdn:
if (this.GetViewport(e4)!=null&&e4.d1.parentNode.rowIndex>=0){
g9=0;
for (g9=0;g9<this.GetViewport(e4).rows.length;g9++){
if (this.GetViewport(e4).rows[g9].offsetTop+this.GetViewport(e4).rows[g9].offsetHeight>this.GetViewport(e4).parentNode.offsetHeight+this.GetViewport(e4).parentNode.scrollTop){
break ;
}
}
g9=this.GetNextRow(e4,g9);
if (g9<this.GetViewport(e4).rows.length){
var f7=this.GetViewport(e4).rows[g9].offsetTop;
this.UpdateLeadingCell(e4,g9,e4.d4);
this.GetViewport(e4).parentNode.scrollTop=f7;
}else {
g9=this.GetPrevRow(e4,this.GetRowCount(e4)-1);
this.UpdateLeadingCell(e4,g9,e4.d4);
}
}
break ;
case this.pup:
if (this.GetViewport(e4)!=null&&e4.d1.parentNode.rowIndex>=0){
g9=0;
for (g9=0;g9<this.GetViewport(e4).rows.length;g9++){
if (this.GetViewport(e4).rows[g9].offsetTop+this.GetViewport(e4).rows[g9].offsetHeight>this.GetViewport(e4).parentNode.scrollTop){
break ;
}
}
if (g9<this.GetViewport(e4).rows.length){
var j4=0;
while (g9>=0){
j4+=this.GetViewport(e4).rows[g9].offsetHeight;
if (j4>this.GetViewport(e4).parentNode.offsetHeight){
break ;
}
g9--;
}
g9=this.GetPrevRow(e4,g9);
if (g9>=0){
var f7=this.GetViewport(e4).rows[g9].offsetTop;
this.UpdateLeadingCell(e4,g9,e4.d4);
this.GetViewport(e4).parentNode.scrollTop=f7;
}
}
}
break ;
}
if (x1!=e4.d1){
var g0=this.CreateEvent("ActiveCellChanged");
g0.cmdID=e4.id;
g0.Row=g0.row=this.GetSheetIndex(e4,this.GetRowFromCell(e4,e4.d1));
g0.Col=g0.col=this.GetColFromCell(e4,e4.d1);
if (e4.getAttribute("LayoutMode"))
g0.InnerRow=g0.innerRow=e4.d1.parentNode.getAttribute("row");
this.FireEvent(e4,g0);
}
}
var h2=this.GetCellFromRowCol(e4,e4.d3,e4.d4);
if (key==this.left&&h2.offsetLeft<x0.scrollLeft){
if (h2.cellIndex>0)
x0.scrollLeft=e4.d1.offsetLeft;
else 
x0.scrollLeft=0;
}else if (h2.cellIndex==0){
x0.scrollLeft=0;
}
if (key==this.right&&h2.offsetLeft+h2.offsetWidth>x0.scrollLeft+x0.offsetWidth-10){
x0.scrollLeft+=h2.offsetWidth;
}
if (key==this.up&&h2.parentNode.offsetTop<x0.scrollTop){
if (h2.parentNode.rowIndex>1)
x0.scrollTop=h2.parentNode.offsetTop;
else 
x0.scrollTop=0;
}else if (h2.parentNode.rowIndex==1){
x0.scrollTop=0;
}
var x4=this.GetParent(this.GetViewport(e4));
x0=this.GetParent(this.GetViewport(e4));
if (key==this.down&&this.IsChild(h2,x0)&&h2.offsetTop+h2.offsetHeight>x0.scrollTop+x0.clientHeight){
x4.scrollTop+=h2.offsetHeight;
}
if (h2!=null&&h2.offsetWidth<x0.clientWidth){
if (this.IsChild(h2,x0)&&h2.offsetLeft+h2.offsetWidth>x0.scrollLeft+x0.clientWidth){
x4.scrollLeft=h2.offsetLeft+h2.offsetWidth-x0.clientWidth;
}
}
if (this.IsChild(h2,x0)&&h2.offsetTop+h2.offsetHeight>x0.scrollTop+x0.clientHeight&&h2.offsetHeight<x0.clientHeight){
x4.scrollTop=h2.offsetTop+h2.offsetHeight-x0.clientHeight;
}
if (h2.offsetTop<x0.scrollTop){
x4.scrollTop=h2.offsetTop;
}
this.ScrollView(e4);
this.EnableButtons(e4);
this.SaveData(e4);
if (e4.d1!=null){
var i2=this.GetEditor(e4.d1);
if (i2!=null){
if (i2.tagName!="SELECT")
i2.focus();
if (!i2.disabled&&(i2.type==null||i2.type=="checkbox"||i2.type=="radio"||i2.type=="text"||i2.type=="password")){
this.a7=true;
this.a8=i2;
this.a9=this.GetEditorValue(i2);
}
}
}
this.Focus(e4);
}
this.MoveUp=function (e4,g9,h1){
var m7=this.GetRowCountInternal(e4);
var g8=this.GetColCount(e4);
g9--;
g9=this.GetPrevRow(e4,g9);
if (g9>=0){
e4.d3=g9;
this.UpdateLeadingCell(e4,e4.d3,e4.d4);
}
}
this.MoveDown=function (e4,g9,h1){
var m7=this.GetRowCountInternal(e4);
var g8=this.GetColCount(e4);
var p7=this.GetSpanCell(g9,h1,this.GetSpanCells(e4,this.GetViewportFromCell(e4,e4.d1)));
if (p7!=null){
g9=p7.row+p7.rowCount;
}else {
g9++;
}
g9=this.GetNextRow(e4,g9);
if (g9==m7)g9=m7-1;
if (g9<m7){
e4.d3=g9;
this.UpdateLeadingCell(e4,e4.d3,e4.d4);
}
}
this.MoveLeft=function (e4,g9,h1){
var x5=g9;
var m7=this.GetRowCountInternal(e4);
var g8=this.GetColCount(e4);
var p7=this.GetSpanCell(g9,h1,this.GetSpanCells(e4,this.GetViewportFromCell(e4,e4.d1)));
if (p7!=null){
h1=p7.col-1;
}else {
h1--;
}
if (h1<0){
h1=g8-1;
g9--;
if (g9<0){
g9=m7-1;
}
g9=this.GetPrevRow(e4,g9);
e4.d3=g9;
}
var x6=this.UpdateLeadingCell(e4,e4.d3,h1);
if (x6)e4.d3=x5;
}
this.MoveRight=function (e4,g9,h1){
var x5=g9;
var m7=this.GetRowCountInternal(e4);
var g8=this.GetColCount(e4);
var p7=this.GetSpanCell(g9,h1,this.GetSpanCells(e4,this.GetViewportFromCell(e4,e4.d1)));
if (p7!=null){
h1=p7.col+p7.colCount;
}else {
h1++;
}
if (h1>=g8){
h1=0;
g9++;
if (g9>=m7)g9=0;
e4.d3=this.GetNextRow(e4,g9);
}
var x6=this.UpdateLeadingCell(e4,e4.d3,h1);
if (x6)e4.d3=x5;
}
this.UpdateLeadingCell=function (e4,g9,h1){
var x7=0;
if (e4.getAttribute("LayoutMode")){
x7=this.GetRowFromViewPort(e4,g9,h1);
e4.d1=this.GetCellFromRowCol(e4,g9,h1);
var x8=this.GetCellFromRowCol(e4,x7,h1);
if (x8)x7=x8.parentNode.getAttribute("row");
}
var i1=this.FireActiveCellChangingEvent(e4,g9,h1,x7);
if (!i1){
var u7=this.GetOperationMode(e4);
if (u7!="MultiSelect")
this.ClearSelection(e4);
e4.d4=h1;
e4.d3=g9;
e4.d6=h1;
e4.d5=g9;
this.UpdateAnchorCell(e4,g9,h1);
}
return i1;
}
this.GetPrevRow=function (e4,g9){
if (g9<0)return 0;
var i3=this.GetViewport(e4);
if (i3!=null){
while (g9>0&&i3.rows[g9].cells.length>0){
if (this.IsChildSpreadRow(e4,i3,g9))
g9--;
else 
break ;
}
}
if (i3!=null&&g9>=0&&g9<i3.rows.length){
if (i3.rows[g9].getAttribute("previewrow")){
g9--;
}
}
return g9;
}
this.GetNextRow=function (e4,g9){
var i3=this.GetViewport(e4);
while (i3!=null&&g9<i3.rows.length){
if (this.IsChildSpreadRow(e4,i3,g9))g9++;
else 
break ;
}
if (i3!=null&&g9>=0&&g9<i3.rows.length){
if (i3.rows[g9].getAttribute("previewrow")){
g9++;
}
}
return g9;
}
this.FireActiveCellChangingEvent=function (e4,i5,m4,innerRow){
var g0=this.CreateEvent("ActiveCellChanging");
g0.cancel=false;
g0.cmdID=e4.id;
g0.row=this.GetSheetIndex(e4,i5);
g0.col=m4;
if (e4.getAttribute("LayoutMode"))
g0.innerRow=innerRow;
this.FireEvent(e4,g0);
return g0.cancel;
}
this.GetSheetRowIndex=function (e4,g9){
g9=this.GetDisplayIndex(e4,g9);
if (g9<0)return -1;
var l6=this.GetViewport(e4).rows[g9];
if (l6!=null){
return l6.getAttribute("FpKey");
}else {
return -1;
}
}
this.GetSheetColIndex=function (e4,h1){
var m4=-1;
var m3=null;
var x9=this.GetColHeader(e4);
if (x9!=null&&x9.rows.length>0){
m3=this.GetColGroup(x9);
}else {
var e5=this.GetViewport(e4);
if (e5!=null&&e5.rows.length>0){
m3=this.GetColGroup(e5);
}
}
if (m3!=null&&h1>=0&&h1<m3.childNodes.length){
m4=m3.childNodes[h1].getAttribute("FpCol");
}
return m4;
}
this.GetCellByRowCol=function (e4,g9,h1){
g9=this.GetDisplayIndex(e4,g9);
return this.GetCellFromRowCol(e4,g9,h1);
}
this.GetHeaderCellFromRowCol=function (e4,g9,h1,c6){
if (g9<0||h1<0)return null;
var e5=null;
if (c6){
e5=this.GetColHeader(e4);
}else {
e5=this.GetRowHeader(e4);
}
var p7=this.GetSpanCell(g9,h1,this.GetSpanCells(e4,e5));
if (p7!=null){
g9=p7.row;
h1=p7.col;
}
var y0=this.GetCellIndex(e4,g9,h1,this.GetSpanCells(e4,e5));
return e5.rows[g9].cells[y0];
}
this.GetCellFromRowCol=function (e4,g9,h1,prevCell){
if (g9<0||h1<0)return null;
var e5=null;
{
e5=this.GetViewport(e4);
}
var d9=e4.d9;
var p7=this.GetSpanCell(g9,h1,d9);
if (p7!=null){
g9=p7.row;
h1=p7.col;
}
var y0=0;
var w7=false;
if (e5!=null)w7=e5.parentNode.getAttribute("hiddenCells");
if (prevCell!=null&&!w7){
if (prevCell.cellIndex<prevCell.parentNode.cells.length-1)
y0=prevCell.cellIndex+1;
}
else 
{
y0=this.GetCellIndex(e4,g9,h1,d9);
}
if (g9>=0&&g9<e5.rows.length)
return e5.rows[g9].cells[y0];
else 
return null;
}
this.GetHiddenValue=function (e4,g9,colName){
if (colName==null)return ;
g9=this.GetDisplayIndex(e4,g9);
var s4=null;
var e5=null;
e5=this.GetViewport(e4);
if (e5!=null&&g9>=0&&g9<e5.rows.length){
var l6=e5.rows[g9];
s4=l6.getAttribute("hv"+colName);
}
return s4;
}
this.GetValue=function (e4,g9,h1){
g9=this.GetDisplayIndex(e4,g9);
var h2=this.GetCellFromRowCol(e4,g9,h1);
var j0=this.GetRender(h2);
var s4=this.GetValueFromRender(e4,j0);
if (s4!=null)s4=this.Trim(s4.toString());
return s4;
}
this.SetValue=function (e4,g9,h1,u0,noEvent,recalc){
g9=this.GetDisplayIndex(e4,g9);
if (u0!=null&&typeof(u0)!="string")u0=new String(u0);
var h2=this.GetCellFromRowCol(e4,g9,h1);
if (this.ValidateCell(e4,h2,u0)){
this.SetCellValueFromView(h2,u0);
if (u0!=null){
this.SetCellValue(e4,h2,""+u0,noEvent,recalc);
}else {
this.SetCellValue(e4,h2,"",noEvent,recalc);
}
this.SizeSpread(e4);
}else {
if (e4.getAttribute("lcidMsg")!=null)
alert(e4.getAttribute("lcidMsg"));
else 
alert("Can't set the data into the cell. The data type is not correct for the cell.");
}
}
this.SetActiveCell=function (e4,g9,h1){
this.ClearSelection(e4,true);
g9=this.GetDisplayIndex(e4,g9);
this.UpdateAnchorCell(e4,g9,h1);
this.ResetLeadingCell(e4);
}
this.GetOperationMode=function (e4){
var u7=e4.getAttribute("OperationMode");
return u7;
}
this.SetOperationMode=function (e4,u7){
e4.setAttribute("OperationMode",u7);
}
this.GetEnableRowEditTemplate=function (e4){
var y1=e4.getAttribute("EnableRowEditTemplate");
return y1;
}
this.GetSelectionPolicy=function (e4){
var y2=e4.getAttribute("SelectionPolicy");
return y2;
}
this.UpdateAnchorCell=function (e4,g9,h1,select){
if (g9<0||h1<0)return ;
e4.d1=this.GetCellFromRowCol(e4,g9,h1);
if (e4.d1==null)return ;
this.SetActiveRow(e4,this.GetRowKeyFromCell(e4,e4.d1));
this.SetActiveCol(e4,this.GetColKeyFromCell(e4,e4.d1));
if (select==null||select){
var u7=this.GetOperationMode(e4);
if (u7=="RowMode"||u7=="SingleSelect"||u7=="ExtendedSelect")
this.SelectRow(e4,g9,1,true,true);
else if (u7!="MultiSelect")
this.SelectRange(e4,g9,h1,1,1,true);
else 
this.PaintFocusRect(e4);
}
}
this.ResetLeadingCell=function (e4){
if (e4.d1==null||!this.IsChild(e4.d1,e4))return ;
e4.d3=this.GetRowFromCell(e4,e4.d1);
e4.d4=this.GetColFromCell(e4,e4.d1);
this.SelectRange(e4.d3,e4.d4,1,1,true);
}
this.Edit=function (e4,i5){
var u7=this.GetOperationMode(e4);
if (u7!="RowMode")return ;
var s1=e4.getAttribute("name");
var y3=(e4.getAttribute("ajax")!="false");
if (y3){
if (FarPoint&&FarPoint.System.WebControl.MultiColumnComboBoxCellTypeUtilitis)
FarPoint.System.WebControl.MultiColumnComboBoxCellTypeUtilitis.CloseAll();
this.SyncData(s1,"Edit,"+i5,e4);
}
else 
__doPostBack(s1,"Edit,"+i5);
}
this.Update=function (e4){
if (this.a7&&this.GetOperationMode(e4)!="RowMode"&&this.GetEnableRowEditTemplate(e4)!="true")return ;
this.SaveData(e4);
var s1=e4.getAttribute("name");
__doPostBack(s1,"Update");
}
this.Cancel=function (e4){
var f7=document.getElementById(e4.id+"_data");
f7.value="";
this.SaveData(e4);
var s1=e4.getAttribute("name");
var y3=(e4.getAttribute("ajax")!="false");
if (y3)
this.SyncData(s1,"Cancel",e4);
else 
__doPostBack(s1,"Cancel");
}
this.Add=function (e4){
if (this.a7)return ;
var s1=null;
var o2=this.GetPageActiveSpread();
if (o2!=null){
s1=o2.getAttribute("name");
}else {
s1=e4.getAttribute("name");
}
var y3=(e4.getAttribute("ajax")!="false");
if (y3)
this.SyncData(s1,"Add",e4);
else 
__doPostBack(s1,"Add");
}
this.Insert=function (e4){
if (this.a7)return ;
var s1=null;
var o2=this.GetPageActiveSpread();
if (o2!=null){
s1=o2.getAttribute("name");
}else {
s1=e4.getAttribute("name");
}
var y3=(e4.getAttribute("ajax")!="false");
if (y3)
this.SyncData(s1,"Insert",e4);
else 
__doPostBack(s1,"Insert");
}
this.Delete=function (e4){
if (this.a7)return ;
var s1=null;
var o2=this.GetPageActiveSpread();
if (o2!=null){
s1=o2.getAttribute("name");
}else {
s1=e4.getAttribute("name");
}
var y3=(e4.getAttribute("ajax")!="false");
if (y3)
this.SyncData(s1,"Delete",e4);
else 
__doPostBack(s1,"Delete");
}
this.Print=function (e4){
if (this.a7)return ;
this.SaveData(e4);
if (document.printSpread==null){
var f7=document.createElement("IFRAME");
f7.name="printSpread";
f7.style.position="absolute";
f7.style.left="-10px";
f7.style.width="0px";
f7.style.height="0px";
document.printSpread=f7;
document.body.insertBefore(f7,null);
f7.addEventListener("load",function (){the_fpSpread.PrintSpread();},false);
}
var y4=document.getElementsByTagName("FORM");
if (y4!=null&&y4.length>0){
var i1=y4[0];
i1.__EVENTTARGET.value=e4.getAttribute("name");
i1.__EVENTARGUMENT.value="Print";
var y5=i1.target;
i1.target="printSpread";
i1.submit();
i1.target=y5;
}
}
this.PrintSpread=function (){
document.printSpread.contentWindow.focus();
document.printSpread.contentWindow.print();
window.focus();
var o2=this.GetPageActiveSpread();
if (o2!=null)this.Focus(o2);
}
this.GotoPage=function (e4,e8){
if (this.a7)return ;
var s1=e4.getAttribute("name");
var y3=(e4.getAttribute("ajax")!="false");
if (y3)
this.SyncData(s1,"Page,"+e8,e4);
else 
__doPostBack(s1,"Page,"+e8);
}
this.Next=function (e4){
if (this.a7)return ;
var s1=e4.getAttribute("name");
var y3=(e4.getAttribute("ajax")!="false");
if (y3)
this.SyncData(s1,"Next",e4);
else 
__doPostBack(s1,"Next");
}
this.Prev=function (e4){
if (this.a7)return ;
var s1=e4.getAttribute("name");
var y3=(e4.getAttribute("ajax")!="false");
if (y3)
this.SyncData(s1,"Prev",e4);
else 
__doPostBack(s1,"Prev");
}
this.GetViewportFromCell=function (e4,i8){
if (i8!=null){
var f7=i8;
while (f7!=null){
if (f7.tagName=="TABLE")break ;
f7=f7.parentNode;
}
if (f7==this.GetViewport(e4))
return f7;
}
return null;
}
this.IsChild=function (h2,i4){
if (h2==null||i4==null)return false;
var f7=h2.parentNode;
while (f7!=null){
if (f7==i4)return true;
f7=f7.parentNode;
}
return false;
}
this.GetCorner=function (e4){
return e4.c4;
}
this.Select=function (e4,cl1,cl2){
if (this.GetSpread(cl1)!=e4||this.GetSpread(cl2)!=e4)return ;
var h3=e4.d5;
var h4=e4.d6;
var y6=this.GetRowFromCell(e4,cl2);
var l7=0;
if (e4.d7=="r"){
l7=-1;
if (this.IsChild(cl2,this.GetColHeader(e4)))
y6=0;
}else if (e4.d7=="c"){
if (this.IsChild(cl2,this.GetRowHeader(e4)))
l7=0;
else 
l7=this.GetColFromCell(e4,cl2);
y6=-1;
}
else {
if (this.IsChild(cl2,this.GetColHeader(e4))){
y6=0;l7=this.GetColFromCell(e4,cl2);
}else if (this.IsChild(cl2,this.GetRowHeader(e4))){
l7=0;
}else {
l7=this.GetColFromCell(e4,cl2);
}
}
if (e4.d7=="t"){
h4=l7=h3=y6=-1;
}
var f7=Math.max(h3,y6);
h3=Math.min(h3,y6);
y6=f7;
f7=Math.max(h4,l7);
h4=Math.min(h4,l7);
l7=f7;
var h7=null;
var m5=this.GetSelection(e4);
var m6=m5.lastChild;
if (m6!=null){
var g9=this.GetRowByKey(e4,m6.getAttribute("row"));
var h1=this.GetColByKey(e4,m6.getAttribute("col"));
var m7=parseInt(m6.getAttribute("rowcount"));
var g8=parseInt(m6.getAttribute("colcount"));
h7=new this.Range();
this.SetRange(h7,"cell",g9,h1,m7,g8);
}
if (h7!=null&&h7.col==-1&&h7.row==-1)return ;
if (h7!=null&&h7.col==-1&&h7.row>=0){
if (h7.row>y6||h7.row+h7.rowCount-1<h3){
this.PaintSelection(e4,h7.row,h7.col,h7.rowCount,h7.colCount,false);
this.PaintSelection(e4,h3,h4,y6-h3+1,l7-h4+1,true);
}else {
if (h3>h7.row){
var f7=h3-h7.row;
this.PaintSelection(e4,h7.row,h7.col,f7,h7.colCount,false);
if (y6<h7.row+h7.rowCount-1){
this.PaintSelection(e4,y6,h7.col,h7.row+h7.rowCount-y6,h7.colCount,false);
}else {
this.PaintSelection(e4,h7.row+h7.rowCount,h7.col,y6-h7.row-h7.rowCount+1,h7.colCount,true);
}
}else {
this.PaintSelection(e4,h3,h7.col,h7.row-h3,h7.colCount,true);
if (y6<h7.row+h7.rowCount-1){
this.PaintSelection(e4,y6+1,h7.col,h7.row+h7.rowCount-y6-1,h7.colCount,false);
}else {
this.PaintSelection(e4,h7.row+h7.rowCount,h7.col,y6-h7.row-h7.rowCount+1,h7.colCount,true);
}
}
}
}else if (h7!=null&&h7.row==-1&&h7.col>=0){
if (h7.col>l7||h7.col+h7.colCount-1<h4){
this.PaintSelection(e4,h7.row,h7.col,h7.rowCount,h7.colCount,false);
this.PaintSelection(e4,h3,h4,y6-h3+1,l7-h4+1,true);
}else {
if (h4>h7.col){
this.PaintSelection(e4,h7.row,h7.col,h7.rowCount,h4-h7.col,false);
if (l7<h7.col+h7.colCount-1){
this.PaintSelection(e4,h7.row,l7,h7.rowCount,h7.col+h7.colCount-l7,false);
}else {
this.PaintSelection(e4,h7.row,h7.col+h7.colCount,h7.rowCount,l7-h7.col-h7.colCount,true);
}
}else {
this.PaintSelection(e4,h7.row,h4,h7.rowCount,h7.col-h4,true);
if (l7<h7.col+h7.colCount-1){
this.PaintSelection(e4,h7.row,l7+1,h7.rowCount,h7.col+h7.colCount-l7-1,false);
}else {
this.PaintSelection(e4,h7.row,h7.col+h7.colCount,h7.rowCount,l7-h7.col-h7.colCount+1,true);
}
}
}
}else if (h7!=null&&h7.row>=0&&h7.col>=0){
this.ExtendSelection(e4,h7,h3,h4,y6-h3+1,l7-h4+1);
}else {
this.PaintSelection(e4,h3,h4,y6-h3+1,l7-h4+1,true);
}
this.SetSelection(e4,h3,h4,y6-h3+1,l7-h4+1,h7==null);
}
this.ExtendSelection=function (e4,h7,newRow,newCol,newRowCount,newColCount)
{
var p5=Math.max(h7.col,newCol);
var p6=Math.min(h7.col+h7.colCount-1,newCol+newColCount-1);
var t8=Math.max(h7.row,newRow);
var y7=Math.min(h7.row+h7.rowCount-1,newRow+newRowCount-1);
if (h7.row<t8){
this.PaintSelection(e4,h7.row,h7.col,t8-h7.row,h7.colCount,false);
}
if (h7.col<p5){
this.PaintSelection(e4,h7.row,h7.col,h7.rowCount,p5-h7.col,false);
}
if (h7.row+h7.rowCount-1>y7){
this.PaintSelection(e4,y7+1,h7.col,h7.row+h7.rowCount-y7-1,h7.colCount,false);
}
if (h7.col+h7.colCount-1>p6){
this.PaintSelection(e4,h7.row,p6+1,h7.rowCount,h7.col+h7.colCount-p6-1,false);
}
if (newRow<t8){
this.PaintSelection(e4,newRow,newCol,t8-newRow,newColCount,true);
}
if (newCol<p5){
this.PaintSelection(e4,newRow,newCol,newRowCount,p5-newCol,true);
}
if (newRow+newRowCount-1>y7){
this.PaintSelection(e4,y7+1,newCol,newRow+newRowCount-y7-1,newColCount,true);
}
if (newCol+newColCount-1>p6){
this.PaintSelection(e4,newRow,p6+1,newRowCount,newCol+newColCount-p6-1,true);
}
}
this.PaintAnchorCellHeader=function (e4,select){
var g9,h1;
g9=this.GetRowFromCell(e4,e4.d1);
h1=this.GetColFromCell(e4,e4.d1);
if (select&&e4.d1.getAttribute("group")!=null){
var p7=this.GetSpanCell(g9,h1,e4.d9);
if (p7!=null&&p7.colCount>1){
var y8=this.GetSelectedRange(e4);
if (g9<y8.row||g9>=y8.row+y8.rowCount||h1<y8.col||h1>=y8.col+y8.colCount)
return ;
}
}
if (this.GetColHeader(e4)!=null)this.PaintHeaderSelection(e4,g9,h1,1,1,select,true);
if (this.GetRowHeader(e4)!=null)this.PaintHeaderSelection(e4,g9,h1,1,1,select,false);
}
this.LineIntersection=function (s1,h4,s2,l7){
var r1,g0;
r1=Math.max(s1,s2);
g0=Math.min(s1+h4,s2+l7);
if (r1<g0)
return {s:r1,c:g0-r1};
return null;
}
this.RangeIntersection=function (h3,h4,v1,cc1,y6,l7,rc2,cc2){
var y9=this.LineIntersection(h3,v1,y6,rc2);
var z0=this.LineIntersection(h4,cc1,l7,cc2);
if (y9&&z0)
return {row:y9.s,col:z0.s,rowCount:y9.c,colCount:z0.c};
return null;
}
this.PaintSelection=function (e4,g9,h1,m7,g8,select){
if (g9<0&&h1<0){
this.PaintCornerSelection(e4,select);
}
var z1=false;
var z2=false;
if (g9<0){
g9=0;
m7=this.GetRowCountInternal(e4);
}
if (h1<0){
h1=0;
g8=this.GetColCount(e4);
}
this.PaintViewportSelection(e4,g9,h1,m7,g8,select);
var m5=this.GetSelection(e4);
var m6;
var y6;
var l7;
var z3;
var z4;
var h7;
var z5;
for (var e9=m5.childNodes.length-1;e9>=0;e9--){
m6=m5.childNodes[e9];
if (m6){
y6=parseInt(m6.getAttribute("rowIndex"));
l7=parseInt(m6.getAttribute("colIndex"));
z3=parseInt(m6.getAttribute("rowcount"));
z4=parseInt(m6.getAttribute("colcount"));
if (y6<0||z3<0){y6=0;z3=this.GetRowCountInternal(e4);}
if (l7<0||z4<0){l7=0;z4=this.GetColCount(e4);}
if (e9>=m5.childNodes.length-1){
if (g9<=y6&&m7>=z3){
if (this.GetColHeader(e4)!=null&&this.GetOperationMode(e4)=="Normal"){
this.PaintHeaderSelection(e4,g9,h1,m7,g8,select,true);
z1=true;
}
}
if (h1<=l7&&g8>=z4){
if (this.GetRowHeader(e4)!=null){
this.PaintHeaderSelection(e4,g9,h1,m7,g8,select,false);
z2=true;
}
}
if (!z1&&!z2){
if (this.GetColHeader(e4)!=null&&this.GetOperationMode(e4)=="Normal"){
this.PaintHeaderSelection(e4,g9,h1,m7,g8,select,true);
z1=true;
}
if (this.GetRowHeader(e4)!=null){
this.PaintHeaderSelection(e4,g9,h1,m7,g8,select,false);
z2=true;
}
}
}
else {
if (!select&&this.GetOperationMode(e4)=="Normal"){
h7=this.RangeIntersection(g9,h1,m7,g8,y6,l7,z3,z4);
if (h7){
this.PaintViewportSelection(e4,h7.row,h7.col,h7.rowCount,h7.colCount,true);
}
if (z1){
z5=this.LineIntersection(h1,g8,l7,z4);
if (z5)this.PaintHeaderSelection(e4,g9,z5.s,m7,z5.c,true,true);
}
if (z2){
z5=this.LineIntersection(g9,m7,y6,z3);
if (z5)this.PaintHeaderSelection(e4,z5.s,h1,z5.c,g8,true,false);
}
}
}
}
}
if (m5.childNodes.length<=0){
if (this.GetColHeader(e4)!=null&&this.GetOperationMode(e4)=="Normal")this.PaintHeaderSelection(e4,g9,h1,m7,g8,select,true);
if (this.GetRowHeader(e4)!=null)this.PaintHeaderSelection(e4,g9,h1,m7,g8,select,false);
}
this.PaintAnchorCell(e4);
}
this.PaintFocusRect=function (e4){
var g3=document.getElementById(e4.id+"_focusRectT");
if (g3==null)return ;
var z6=this.GetSelectedRange(e4);
if (e4.d1==null&&(z6==null||(z6.rowCount==0&&z6.colCount==0))){
g3.style.left="-1000px";
var s1=e4.id;
g3=document.getElementById(s1+"_focusRectB");
g3.style.left="-1000px";
g3=document.getElementById(s1+"_focusRectL");
g3.style.left="-1000px";
g3=document.getElementById(s1+"_focusRectR");
g3.style.left="-1000px";
return ;
}
var i0=this.GetOperationMode(e4);
if (i0=="RowMode"||i0=="SingleSelect"||i0=="MultiSelect"||i0=="ExtendedSelect"){
var g9=e4.GetActiveRow();
z6=new this.Range();
this.SetRange(z6,"Row",g9,-1,1,-1);
}else if (z6==null||(z6.rowCount==0&&z6.colCount==0)){
var g9=e4.GetActiveRow();
var h1=e4.GetActiveCol();
z6=new this.Range();
this.SetRange(z6,"Cell",g9,h1,e4.d1.rowSpan,e4.d1.colSpan);
}
if (z6.row<0){
z6.row=0;
z6.rowCount=this.GetRowCountInternal(e4);
}
if (z6.col<0){
z6.col=0;
z6.colCount=this.GetColCount(e4);
}
var h2=this.GetCellFromRowCol(e4,z6.row,z6.col);
if (h2==null)return ;
if (z6.rowCount==1&&z6.colCount==1){
z6.rowCount=h2.rowSpan;
z6.colCount=h2.colSpan;
if (h2.colSpan>1){
var z7=parseInt(h2.getAttribute("col"));
if (z7!=z6.col&&!isNaN(z7))z6.col=z7;
}
}
var f7=this.GetOffsetTop(e4,h2);
var z8=this.GetOffsetLeft(e4,h2);
if (h2.rowSpan>1){
z6.row=h2.parentNode.rowIndex;
var h4=this.GetCellFromRowCol(e4,z6.row,z6.col+z6.colCount-1);
if (h4!=null&&h4.parentNode.rowIndex>h2.parentNode.rowIndex){
f7=this.GetOffsetTop(e4,h4);
}
}
if (h2.colSpan>1){
var h4=this.GetCellFromRowCol(e4,z6.row+z6.rowCount-1,z6.col);
var j3=this.GetOffsetLeft(e4,h4);
if (j3>z8){
z8=j3;
h2=h4;
}
}
var j4=0;
var g7=this.GetViewport(e4).rows;
for (var g9=z6.row;g9<z6.row+z6.rowCount&&g9<g7.length;g9++){
j4+=g7[g9].offsetHeight;
if (g9>z6.row)j4+=parseInt(this.GetViewport(e4).cellSpacing);
}
var i9=0;
var m3=this.GetColGroup(this.GetViewport(e4));
if (m3.childNodes==null||m3.childNodes.length==0)return ;
for (var h1=z6.col;h1<z6.col+z6.colCount&&h1<m3.childNodes.length;h1++){
i9+=m3.childNodes[h1].offsetWidth;
if (h1>z6.col)i9+=parseInt(this.GetViewport(e4).cellSpacing);
}
if (z6.col>h2.cellIndex&&z6.type=="Column"){
var l7=parseInt(h2.getAttribute("col"));
for (var h1=l7;h1<z6.col;h1++){
z8+=m3.childNodes[h1].offsetWidth;
if (h1>l7)z8+=parseInt(this.GetViewport(e4).cellSpacing);
}
}
if (z6.row>0)f7-=2;
else j4-=2;
if (z6.col>0)z8-=2;
else i9-=2;
if (parseInt(this.GetViewport(e4).cellSpacing)>0){
f7+=1;z8+=1;
}else {
i9+=1;
j4+=1;
}
if (i9<0)i9=0;
if (j4<0)j4=0;
g3.style.left=""+z8+"px";
g3.style.top=""+f7+"px";
g3.style.width=""+i9+"px";
g3=document.getElementById(e4.id+"_focusRectB");
g3.style.left=""+z8+"px";
g3.style.top=""+(f7+j4)+"px";
g3.style.width=""+i9+"px";
g3=document.getElementById(e4.id+"_focusRectL");
g3.style.left=""+z8+"px";
g3.style.top=""+f7+"px";
g3.style.height=""+j4+"px";
g3=document.getElementById(e4.id+"_focusRectR");
g3.style.left=""+(z8+i9)+"px";
g3.style.top=""+f7+"px";
g3.style.height=""+j4+"px";
}
this.PaintCornerSelection=function (e4,select){
var z9=true;
if (e4.getAttribute("ShowHeaderSelection")=="false")z9=false;
if (!z9)return ;
var m2=this.GetCorner(e4);
if (m2!=null&&m2.rows.length>0){
for (var e9=0;e9<m2.rows.length;e9++){
for (var h9=0;h9<m2.rows[0].cells.length;h9++){
if (m2.rows[e9].cells[h9]!=null)
this.PaintSelectedCell(e4,m2.rows[e9].cells[h9],select);
}
}
}
}
this.PaintHeaderSelection=function (e4,g9,h1,m7,g8,select,c6){
var z9=true;
if (e4.getAttribute("ShowHeaderSelection")=="false")z9=false;
if (!z9)return ;
var aa0=this.GetRowCountInternal(e4);
var aa1=this.GetColCount(e4);
if (c6){
if (this.GetColHeader(e4)==null)return ;
g9=0;
m7=aa0=this.GetColHeader(e4).rows.length;
}else {
if (this.GetRowHeader(e4)==null)return ;
h1=0;
g8=aa1=this.GetColGroup(this.GetRowHeader(e4)).childNodes.length;
}
var aa2=c6?e4.e1:e4.e0;
for (var e9=g9;e9<g9+m7&&e9<aa0;e9++){
if (!c6&&this.IsChildSpreadRow(e4,this.GetViewport(e4),e9))continue ;
for (var h9=h1;h9<h1+g8&&h9<aa1;h9++){
if (this.IsCovered(e4,e9,h9,aa2))continue ;
var h2=this.GetHeaderCellFromRowCol(e4,e9,h9,c6);
if (h2!=null)this.PaintSelectedCell(e4,h2,select);
}
}
}
this.PaintViewportSelection=function (e4,g9,h1,m7,g8,select){
var aa0=this.GetRowCountInternal(e4);
var aa1=this.GetColCount(e4);
for (var e9=g9;e9<g9+m7&&e9<aa0;e9++){
if (this.IsChildSpreadRow(e4,this.GetViewport(e4),e9))continue ;
var h2=null;
for (var h9=h1;h9<h1+g8&&h9<aa1;h9++){
if (this.IsCovered(e4,e9,h9,e4.d9))continue ;
h2=this.GetCellFromRowCol(e4,e9,h9,h2);
this.PaintSelectedCell(e4,h2,select);
}
}
}
this.Copy=function (e4){
var o2=this.GetPageActiveSpread();
if (o2!=null&&o2!=e4&&this.GetTopSpread(o2)==e4){
this.Copy(o2);
return ;
}
var m5=this.GetSelection(e4);
var m6=m5.lastChild;
if (m6!=null){
var g9=this.GetRowByKey(e4,m6.getAttribute("row"));
var h1=this.GetColByKey(e4,m6.getAttribute("col"));
var m7=parseInt(m6.getAttribute("rowcount"));
var g8=parseInt(m6.getAttribute("colcount"));
if (g9<0){
g9=0;
m7=this.GetRowCountInternal(e4);
}
if (h1<0){
h1=0;
g8=this.GetColCount(e4);
}
var f3="";
for (var e9=g9;e9<g9+m7;e9++){
if (this.IsChildSpreadRow(e4,this.GetViewport(e4),e9))continue ;
var h2=null;
for (var h9=h1;h9<h1+g8;h9++){
if (this.IsCovered(e4,e9,h9,e4.d9))
f3+="";
else 
{
h2=this.GetCellFromRowCol(e4,e9,h9,h2);
if (h2!=null&&h2.parentNode.getAttribute("previewrow")=="true")continue ;
var q5=this.GetCellType(h2);
if (q5=="TextCellType"&&h2.getAttribute("password")!=null)
f3+="";
else 
f3+=this.GetCellValueFromView(e4,h2);
}
if (h9+1<h1+g8)f3+="\t";
}
f3+="\r\n";
}
this.b9=f3;
}else {
if (e4.d1!=null){
var f3=this.GetCellValueFromView(e4,e4.d1);
this.b9=f3;
}
}
}
this.GetCellValueFromView=function (e4,h2){
var u0=null;
if (h2!=null){
var aa3=this.GetRender(h2);
u0=this.GetValueFromRender(e4,aa3);
if (u0==null||u0==" ")u0="";
}
return u0;
}
this.SetCellValueFromView=function (h2,u0,ignoreLock){
if (h2!=null){
var aa3=this.GetRender(h2);
var r0=this.GetCellType(h2);
if ((r0!="readonly"||ignoreLock)&&aa3!=null&&aa3.getAttribute("FpEditor")!="Button")
this.SetValueToRender(aa3,u0);
}
}
this.Paste=function (e4){
var o2=this.GetPageActiveSpread();
if (o2!=null&&o2!=e4&&this.GetTopSpread(o2)==e4){
this.Paste(o2);
return ;
}
if (e4.d1==null)return ;
var f3=this.b9;
if (f3==null)return ;
var e5=this.GetViewportFromCell(e4,e4.d1);
var g9=this.GetRowFromCell(e4,e4.d1);
var h1=this.GetColFromCell(e4,e4.d1);
var g8=this.GetColCount(e4);
var m7=this.GetRowCountInternal(e4);
var aa4=g9;
var w6=h1;
var aa5=new String(f3);
if (aa5.length==0)return ;
var e8=aa5.lastIndexOf("\r\n");
if (e8>=0&&e8==aa5.length-2)aa5=aa5.substring(0,e8);
var aa6=0;
var aa7=aa5.split("\r\n");
for (var e9=0;e9<aa7.length&&aa4<m7;e9++){
if (typeof(aa7[e9])=="string"){
aa7[e9]=aa7[e9].split("\t");
if (aa7[e9].length>aa6)aa6=aa7[e9].length;
}
aa4++;
}
aa4=this.GetSheetIndex(e4,g9);
for (var e9=0;e9<aa7.length&&aa4<m7;e9++){
var aa8=aa7[e9];
if (aa8!=null){
w6=h1;
var h2=null;
var y6=this.GetDisplayIndex(e4,aa4);
for (var h9=0;h9<aa8.length&&w6<g8;h9++){
if (!this.IsCovered(e4,y6,w6,e4.d9)){
h2=this.GetCellFromRowCol(e4,y6,w6,h2);
if (h2!=null&&h2.parentNode.getAttribute("previewrow")=="true")continue ;
if (h2==null)return ;
var aa9=aa8[h9];
if (!this.ValidateCell(e4,h2,aa9)){
if (e4.getAttribute("lcidMsg")!=null)
alert(e4.getAttribute("lcidMsg"));
else 
alert("Can't set the data into the cell. The data type is not correct for the cell.");
return ;
}
}
w6++;
}
}
aa4++;
}
if (aa7.length==0)return ;
aa4=this.GetSheetIndex(e4,g9);
for (var e9=0;e9<aa7.length&&aa4<m7;e9++){
w6=h1;
var aa8=aa7[e9];
var h2=null;
var y6=this.GetDisplayIndex(e4,aa4);
for (var h9=0;h9<aa6&&w6<g8;h9++){
if (!this.IsCovered(e4,y6,w6,e4.d9)){
h2=this.GetCellFromRowCol(e4,y6,w6,h2);
if (h2!=null&&h2.parentNode.getAttribute("previewrow")=="true")continue ;
var r0=this.GetCellType(h2);
var aa3=this.GetRender(h2);
if (r0!="readonly"&&aa3.getAttribute("FpEditor")!="Button"){
var aa9=null;
if (aa8!=null&&h9<aa8.length)aa9=aa8[h9];
this.SetCellValueFromView(h2,aa9);
if (aa9!=null){
this.SetCellValue(e4,h2,""+aa9);
}else {
this.SetCellValue(e4,h2,"");
}
}
}
w6++;
}
aa4++;
}
var u5=e4.getAttribute("autoCalc");
if (u5!="false"){
this.UpdateValues(e4);
}
var e7=this.GetTopSpread(e4);
var f8=document.getElementById(e7.id+"_textBox");
if (f8!=null){
f8.blur();
}
this.Focus(e4);
}
this.UpdateValues=function (e4){
if (e4.d8==null&&this.GetParentSpread(e4)==null&&e4.getAttribute("rowFilter")!="true"&&e4.getAttribute("hierView")!="true"&&e4.getAttribute("IsNewRow")!="true"){
this.SaveData(e4);
this.StorePostData(e4);
this.SyncData(e4.getAttribute("name"),"UpdateValues",e4);
}
}
this.ValidateCell=function (e4,h2,u0){
if (h2==null||u0==null||u0=="")return true;
var u3=null;
var q5=this.GetCellType(h2);
if (q5!=null){
var i1=this.GetFunction(q5+"_isValid");
if (i1!=null){
u3=i1(h2,u0);
}
}
return (u3==null||u3=="");
}
this.DoclearSelection=function (e4){
var m5=this.GetSelection(e4);
var m6=m5.lastChild;
while (m6!=null){
var g9=this.GetRowByKey(e4,m6.getAttribute("row"));
var h1=this.GetColByKey(e4,m6.getAttribute("col"));
var m7=parseInt(m6.getAttribute("rowcount"));
var g8=parseInt(m6.getAttribute("colcount"));
this.PaintSelection(e4,g9,h1,m7,g8,false);
m5.removeChild(m6);
m6=m5.lastChild;
}
}
this.Clear=function (e4){
var o2=this.GetPageActiveSpread();
if (o2!=null&&o2!=e4&&this.GetTopSpread(o2)==e4){
this.Clear(o2);
return ;
}
var r0=this.GetCellType(e4.d1);
if (r0=="readonly")return ;
var m5=this.GetSelection(e4);
var m6=m5.lastChild;
if (this.AnyReadOnlyCell(e4,m6)){
return ;
}
this.Copy(e4);
if (m6!=null){
var g9=this.GetRowByKey(e4,m6.getAttribute("row"));
var h1=this.GetColByKey(e4,m6.getAttribute("col"));
var m7=parseInt(m6.getAttribute("rowcount"));
var g8=parseInt(m6.getAttribute("colcount"));
if (g9<0){
g9=0;
m7=this.GetRowCountInternal(e4);
}
if (h1<0){
h1=0;
g8=this.GetColCount(e4);
}
for (var e9=g9;e9<g9+m7;e9++){
if (this.IsChildSpreadRow(e4,this.GetViewport(e4),e9))continue ;
var h2=null;
for (var h9=h1;h9<h1+g8;h9++){
if (!this.IsCovered(e4,e9,h9,e4.d9)){
h2=this.GetCellFromRowCol(e4,e9,h9,h2);
if (h2!=null&&h2.parentNode.getAttribute("previewrow")=="true")continue ;
var r0=this.GetCellType(h2);
if (r0!="readonly"){
var ab0=this.GetEditor(h2);
if (ab0!=null&&ab0.getAttribute("FpEditor")=="Button")continue ;
this.SetCellValueFromView(h2,null);
this.SetCellValue(e4,h2,"");
}
}
}
}
var u5=e4.getAttribute("autoCalc");
if (u5!="false"){
this.UpdateValues(e4);
}
}
}
this.AnyReadOnlyCell=function (e4,m6){
if (m6!=null){
var g9=this.GetRowByKey(e4,m6.getAttribute("row"));
var h1=this.GetColByKey(e4,m6.getAttribute("col"));
var m7=parseInt(m6.getAttribute("rowcount"));
var g8=parseInt(m6.getAttribute("colcount"));
if (g9<0){
g9=0;
m7=this.GetRowCountInternal(e4);
}
if (h1<0){
h1=0;
g8=this.GetColCount(e4);
}
for (var e9=g9;e9<g9+m7;e9++){
if (this.IsChildSpreadRow(e4,this.GetViewport(e4),e9))continue ;
var h2=null;
for (var h9=h1;h9<h1+g8;h9++){
if (!this.IsCovered(e4,e9,h9,e4.d9)){
h2=this.GetCellFromRowCol(e4,e9,h9,h2);
var r0=this.GetCellType(h2);
if (r0=="readonly"){
return true;
}
}
}
}
}
return false;
}
this.MoveSliderBar=function (e4,g0){
var l5=this.GetElementById(this.activePager,e4.id+"_slideBar");
var f7=(g0.clientX-this.GetOffsetLeft(e4,e4,document.body)+window.scrollX-8);
if (f7<e4.slideLeft)f7=e4.slideLeft;
if (f7>e4.slideRight)f7=e4.slideRight;
var l9=parseInt(this.activePager.getAttribute("totalPage"))-1;
var ab1=parseInt(((f7-e4.slideLeft)/(e4.slideRight-e4.slideLeft))*l9)+1;
if (e4.style.position!="absolute"&&e4.style.position!="relative")
f7+=this.GetOffsetLeft(e4,e4,document.body)
l5.style.left=f7+"px";
return ab1;
}
this.MouseMove=function (event){
if (window.fpPostOn!=null)return ;
event=this.GetEvent(event);
var n4=this.GetTarget(event);
if (n4!=null&&n4.tagName=="scrollbar")
return ;
var e4=this.GetSpread(n4,true);
if (e4!=null&&this.dragSlideBar)
{
if (this.activePager!=null){
var ab1=this.MoveSliderBar(e4,event);
var ab2=this.GetElementById(this.activePager,e4.id+"_posIndicator");
ab2.innerHTML=this.activePager.getAttribute("pageText")+ab1;
}
return ;
}
if (this.a6)e4=this.GetSpread(this.b8);
if (e4==null||(!this.a6&&this.HitCommandBar(n4)))return ;
if (e4.getAttribute("OperationMode")=="ReadOnly")return ;
var j5=this.IsXHTML(e4);
if (this.a6){
if (this.dragCol!=null&&this.dragCol>=0){
var v4=this.GetMovingCol(e4);
if (v4!=null){
if (v4.style.display=="none")v4.style.display="";
if (e4.style.position!="absolute"&&e4.style.position!="relative"){
v4.style.top=""+(event.clientY+window.scrollY)+"px";
v4.style.left=""+(event.clientX+window.scrollX+5)+"px";
}else {
v4.style.top=""+(event.clientY-this.GetOffsetTop(e4,e4,document.body)+window.scrollY)+"px";
v4.style.left=""+(event.clientX-this.GetOffsetLeft(e4,e4,document.body)+window.scrollX+5)+"px";
}
}
var e5=this.GetViewport(e4);
var ab3=document.body;
var ab4=this.GetGroupBar(e4);
var f7=-1;
var l4=event.clientX;
var t8=0;
var p5=0;
if (e4.style.position!="absolute"&&e4.style.position!="relative"){
t8=this.GetOffsetTop(e4,e4,document.body)-e5.parentNode.scrollTop;
p5=this.GetOffsetLeft(e4,e4,document.body)-e5.parentNode.scrollLeft;
l4+=Math.max(document.body.scrollLeft,document.documentElement.scrollLeft);
}else {
l4-=(this.GetOffsetLeft(e4,e4,document.body)-Math.max(document.body.scrollLeft,document.documentElement.scrollLeft));
}
var ab5=false;
var j5=this.IsXHTML(e4);
var ab6=j5?document.body.parentNode.scrollTop:document.body.scrollTop;
var k5=document.getElementById(e4.id+"_titleBar");
if (k5)ab6-=k5.parentNode.parentNode.offsetHeight;
if (this.GetPager1(e4)!=null)ab6-=this.GetPager1(e4).offsetHeight;
if (ab4!=null&&event.clientY<this.GetOffsetTop(e4,e4,document.body)-e5.parentNode.scrollTop+ab4.offsetHeight-ab6){
if (e4.style.position!="absolute"&&e4.style.position!="relative")
p5=this.GetOffsetLeft(e4,e4,document.body);
t8+=10;
ab5=true;
var v7=ab4.getElementsByTagName("TABLE")[0];
if (v7!=null){
for (var e9=0;e9<v7.rows[0].cells[0].childNodes.length;e9++){
var i9=v7.rows[0].cells[0].childNodes[e9].offsetWidth;
if (i9==null)continue ;
if (p5<=l4&&l4<p5+i9){
f7=e9;
break ;
}
p5+=i9;
}
}
if (f7==-1&&l4>=p5)f7=-2;
e4.targetCol=f7;
}else {
if (e4.style.position=="absolute"||e4.style.position=="relative")
p5=-e5.parentNode.scrollLeft;
if (this.GetRowHeader(e4)!=null)p5+=this.GetRowHeader(e4).offsetWidth;
if (ab4!=null)t8+=ab4.offsetHeight;
if (l4<p5){
f7=0;
}else {
var m3=this.GetColGroup(this.GetColHeader(e4));
if (m3!=null){
for (var e9=0;e9<m3.childNodes.length;e9++){
var i9=parseInt(m3.childNodes[e9].width);
if (i9==null)continue ;
if (p5<=l4&&l4<p5+i9){
f7=e9;
break ;
}
p5+=i9;
}
}
}
if (f7>=0&&f7!=this.dragViewCol){
if (this.dragViewCol<f7){
f7++;
if (f7<m3.childNodes.length)
p5+=i9;
}
}
p5-=5;
var ab7=parseInt(this.GetSheetColIndex(e4,f7));
if (ab7<0)ab7=f7;
e4.targetCol=ab7;
}
if (k5)t8+=k5.parentNode.parentNode.offsetHeight;
if (this.GetPager1(e4)!=null)t8+=this.GetPager1(e4).offsetHeight;
var ab2=this.GetPosIndicator(e4);
ab2.style.left=""+p5+"px";
ab2.style.top=""+t8+"px";
if (ab4!=null&&ab5&&ab4.getElementsByTagName("TABLE").length==0){
ab2.style.display="none";
}else {
if (ab5||e4.allowColMove)
ab2.style.display="";
else 
ab2.style.display="none";
}
return ;
}
if (this.b4==null&&this.b5==null){
if (e4.d1!=null){
var i3=this.GetParent(this.GetViewport(e4));
if (i3!=null){
var r2=e4.offsetTop+i3.offsetTop+i3.offsetHeight-10;
if (event.clientY>r2){
i3.scrollTop=i3.scrollTop+10;
this.ScrollView(e4);
}else if (event.clientY<e4.offsetTop+i3.offsetTop+5){
i3.scrollTop=i3.scrollTop-10;
this.ScrollView(e4);
}
var ab8=e4.offsetLeft+i3.offsetLeft+i3.offsetWidth-20;
if (event.clientX>ab8){
i3.scrollLeft=i3.scrollLeft+10;
this.ScrollView(e4);
}else if (event.clientX<e4.offsetLeft+i3.offsetLeft+5){
i3.scrollLeft=i3.scrollLeft-10;
this.ScrollView(e4);
}
}
var h2=this.GetCell(n4,null,event);
if (h2!=null&&h2!=e4.d2){
var i0=this.GetOperationMode(e4);
if (i0!="MultiSelect"){
if (i0=="SingleSelect"||i0=="RowMode"){
this.ClearSelection(e4);
var h3=this.GetRowFromCell(e4,h2);
this.UpdateAnchorCell(e4,h3,0);
this.SelectRow(e4,h3,1,true,true);
}else {
if (!(i0=="Normal"&&this.GetSelectionPolicy(e4)=="Single")){
this.Select(e4,e4.d1,h2);
this.SyncColSelection(e4);
}
}
e4.d2=h2;
}
}
}
}else if (this.b4!=null){
var ab9=event.clientX-this.b6;
var w4=parseInt(this.b4.width)+ab9;
var t7=0;
var ac0=(w4>t7);
if (ac0){
this.b4.width=w4;
var k0=parseInt(this.b4.getAttribute("index"));
this.SetWidthFix(this.GetColHeader(e4),k0,w4);
this.b6=event.clientX;
}
}else if (this.b5!=null){
var ab9=event.clientY-this.b7;
var ac1=parseInt(this.b5.style.height)+ab9;
var t7=0;
var ac0=(t7<ac1);
if (ac0){
this.b5.cells[0].style.posHeight=this.b5.cells[1].style.posHeight=(this.b5.cells[0].style.posHeight+ab9);
this.b7=event.clientY;
}
}
}else {
this.b8=n4;
if (this.b8==null||this.GetSpread(this.b8)!=e4)return ;
var n4=this.GetSizeColumn(e4,this.b8,event);
if (n4!=null){
this.b4=n4;
this.b8.style.cursor=this.GetResizeCursor(false);
}else {
var n4=this.GetSizeRow(e4,this.b8,event);
if (n4!=null){
this.b5=n4;
if (this.b8!=null&&this.b8.style!=null)this.b8.style.cursor=this.GetResizeCursor(true);
}else {
if (this.b8!=null&&this.b8.style!=null){
var h2=this.GetCell(this.b8);
if (h2!=null&&this.IsHeaderCell(e4,h2))this.b8.style.cursor="default";
if (this.b8!=null&&(this.b8.getAttribute("FpSpread")=="rowpadding"||this.b8.getAttribute("ControlType")=="chgrayarea"))
this.b8.style.cursor=this.GetgrayAreaCursor(e4);
}
}
}
}
}
this.GetgrayAreaCursor=function (e4){
if (e4.c3!=null&&e4.c3.style.cursor!=null){
if (e4.c3.style.cursor=="auto")
e4.c3.style.cursor="default";
return e4.c3.style.cursor;
}
else return "default";
}
this.GetResizeCursor=function (i5){
if (i5){
return "n-resize";
}else {
return "w-resize";
}
}
this.HitCommandBar=function (n4){
var f7=n4;
var e4=this.GetTopSpread(this.GetSpread(f7,true));
if (e4==null)return false;
var p1=this.GetCommandBar(e4);
while (f7!=null&&f7!=e4){
if (f7==p1)return true;
f7=f7.parentNode;
}
return false;
}
this.OpenWaitMsg=function (e4){
var i1=document.getElementById(e4.id+"_waitmsg");
if (i1==null)return ;
var i9=e4.offsetWidth;
var j4=e4.offsetHeight;
var i6=this.CreateTestBox(e4);
i6.style.fontFamily=i1.style.fontFamily;
i6.style.fontSize=i1.style.fontSize;
i6.style.fontWeight=i1.style.fontWeight;
i6.style.fontStyle=i1.style.fontStyle;
i6.innerHTML=i1.innerHTML;
i1.style.width=""+(i6.offsetWidth+2)+"px";
var z8=Math.max(10,(i9-parseInt(i1.style.width))/2);
var f7=Math.max(10,(j4-parseInt(i1.style.height))/2);
if (e4.style.position!="absolute"&&e4.style.position!="relative"){
z8+=e4.offsetLeft;
f7+=e4.offsetTop;
}
i1.style.top=""+f7+"px";
i1.style.left=""+z8+"px";
i1.style.display="block";
}
this.CloseWaitMsg=function (e4){
var i1=document.getElementById(e4.id+"_waitmsg");
if (i1==null)return ;
i1.style.display="none";
this.Focus(e4);
}
this.MouseDown=function (event){
if (window.fpPostOn!=null)return ;
event=this.GetEvent(event);
var n4=this.GetTarget(event);
var e4=this.GetSpread(n4,true);
e4.mouseY=event.clientY;
var ac2=this.GetPageActiveSpread();
if (this.GetViewport(e4)==null)return ;
if (e4!=null&&n4.parentNode!=null&&n4.parentNode.getAttribute("name")==e4.id+"_slideBar"){
if (this.IsChild(n4,this.GetPager1(e4)))
this.activePager=this.GetPager1(e4);
else if (this.IsChild(n4,this.GetPager2(e4)))
this.activePager=this.GetPager2(e4);
if (this.activePager!=null){
var n7=true;
if (this.a7)n7=this.EndEdit(e4);
if (n7){
this.UpdatePostbackData(e4);
this.dragSlideBar=true;
}
}
return this.CancelDefault(event);
}
if (this.GetOperationMode(e4)=="ReadOnly")return ;
var j5=false;
if (e4!=null)j5=this.IsXHTML(e4);
if (this.a7&&e4.getAttribute("mcctCellType")!="true"){
var f7=this.GetCell(n4);
if (f7!=e4.d1){
var n7=this.EndEdit();
if (!n7)return ;
}else 
return ;
}
if (n4==this.GetParent(this.GetViewport(e4))){
if (this.GetTopSpread(ac2)!=e4){
this.SetActiveSpread(event);
}
return ;
}
var ac3=(ac2==e4);
this.SetActiveSpread(event);
ac2=this.GetPageActiveSpread();
if (this.HitCommandBar(n4))return ;
if (event.button==2)return ;
if (this.IsChild(n4,this.GetGroupBar(e4))){
var h4=parseInt(n4.id.replace(e4.id+"_group",""));
if (!isNaN(h4)){
this.dragCol=h4;
this.dragViewCol=this.GetColByKey(e4,h4);
var v4=this.GetMovingCol(e4);
v4.innerHTML=n4.innerHTML;
v4.style.width=""+Math.max(this.GetPreferredCellWidth(e4,n4),80)+"px";
if (e4.getAttribute("DragColumnCssClass")==null)
v4.style.backgroundColor=n4.style.backgroundColor;
v4.style.top="-50px";
v4.style.left="-100px";
this.a6=true;
e4.dragFromGroupbar=true;
this.CancelDefault(event);
return ;
}
}
this.b4=this.GetSizeColumn(e4,n4,event);
if (this.b4!=null){
this.a6=true;
this.b6=this.b7=event.clientX;
if (this.b4.style!=null)this.b4.style.cursor=this.GetResizeCursor(false);
this.b8=n4;
}else {
this.b5=this.GetSizeRow(e4,n4,event);
if (this.b5!=null){
this.a6=true;
this.b6=this.b7=event.clientY;
this.b5.style.cursor=this.GetResizeCursor(true);
this.b8=n4;
}else {
var ac4=this.GetCell(n4,null,event);
if (ac4==null){
var c4=this.GetCorner(e4);
if (c4!=null&&this.IsChild(n4,c4)){
if (this.GetOperationMode(e4)=="Normal")
this.SelectTable(e4,true);
}
return ;
}
var ac5=this.GetColFromCell(e4,ac4);
if (ac4.parentNode.getAttribute("FpSpread")=="ch"&&this.GetColFromCell(e4,ac4)>=this.GetColCount(e4))return ;
if (ac4.parentNode.getAttribute("FpSpread")=="rh"&&this.IsChildSpreadRow(e4,this.GetViewport(e4),ac4.parentNode.rowIndex))return ;
if (ac4.parentNode.getAttribute("FpSpread")=="ch"&&(this.GetOperationMode(e4)=="RowMode"||this.GetOperationMode(e4)=="SingleSelect"||this.GetOperationMode(e4)=="ExtendedSelect")){
if (!e4.allowColMove&&!e4.allowGroup)
return ;
}else {
var n5=this.FireActiveCellChangingEvent(e4,this.GetRowFromCell(e4,ac4),this.GetColFromCell(e4,ac4),ac4.parentNode.getAttribute("row"));
if (n5)return ;
var u7=this.GetOperationMode(e4);
var e7=this.GetTopSpread(e4);
if (!event.ctrlKey||e4.getAttribute("multiRange")!="true"){
if (u7!="MultiSelect"){
if (!(
(e4.allowColMove||e4.allowGroup)&&ac4.parentNode.getAttribute("FpSpread")=="ch"&&
u7=="Normal"&&(e4.getAttribute("SelectionPolicy")=="Range"||e4.getAttribute("SelectionPolicy")=="MultiRange")&&
e4.selectedCols.length!=0&&this.IsColSelected(e4,ac5)
))
this.ClearSelection(e4);
}
}else {
if (u7!="ExtendedSelect"&&u7!="MultiSelect"){
if (e4.d1!=null)this.PaintSelectedCell(e4,e4.d1,true);
}
}
}
e4.d1=ac4;
var h2=e4.d1;
var x0=this.GetParent(this.GetViewport(e4));
if (x0!=null&&!this.IsControl(n4)&&(n4!=null&&n4.tagName!="scrollbar")){
if (this.IsChild(h2,x0)&&h2.offsetLeft+h2.offsetWidth>x0.scrollLeft+x0.clientWidth){
x0.scrollLeft=h2.offsetLeft+h2.offsetWidth-x0.clientWidth;
}
if (this.IsChild(h2,x0)&&h2.offsetTop+h2.offsetHeight>x0.scrollTop+x0.clientHeight&&h2.offsetHeight<x0.clientHeight){
x0.scrollTop=h2.offsetTop+h2.offsetHeight-x0.clientHeight;
}
if (h2.offsetTop<x0.scrollTop){
x0.scrollTop=h2.offsetTop;
}
if (h2.offsetLeft<x0.scrollLeft){
x0.scrollLeft=h2.offsetLeft;
}
this.ScrollView(e4);
}
if (ac4.parentNode.getAttribute("FpSpread")!="ch")this.SetActiveRow(e4,this.GetRowKeyFromCell(e4,e4.d1));
if (ac4.parentNode.getAttribute("FpSpread")=="rh")
this.SetActiveCol(e4,0);
else {
this.SetActiveCol(e4,this.GetColKeyFromCell(e4,e4.d1));
}
var u7=this.GetOperationMode(e4);
if (e4.d1.parentNode.getAttribute("FpSpread")=="r"){
if (u7=="ExtendedSelect"||u7=="MultiSelect"){
var ac6=this.IsRowSelected(e4,this.GetRowFromCell(e4,e4.d1));
if (ac6)
this.SelectRow(e4,this.GetRowFromCell(e4,e4.d1),1,false,true);
else 
this.SelectRow(e4,this.GetRowFromCell(e4,e4.d1),1,true,true);
}
else if (u7=="RowMode"||u7=="SingleSelect")
this.SelectRow(e4,this.GetRowFromCell(e4,e4.d1),1,true,true);
else {
this.SelectRange(e4,this.GetRowFromCell(e4,e4.d1),this.GetColFromCell(e4,e4.d1),1,1,true);
}
e4.d5=this.GetRowFromCell(e4,e4.d1);
e4.d6=this.GetColFromCell(e4,e4.d1);
}else if (e4.d1.parentNode.getAttribute("FpSpread")=="ch"){
if (n4.tagName=="INPUT"||n4.tagName=="TEXTAREA"||n4.tagName=="SELECT")
return ;
var r5=this.GetColFromCell(e4,e4.d1);
if (e4.allowColMove||e4.allowGroup)
{
if (u7=="Normal"&&(e4.getAttribute("SelectionPolicy")=="Range"||e4.getAttribute("SelectionPolicy")=="MultiRange")){
if (this.IsColSelected(e4,r5)){
this.InitMovingCol(e4,r5);
}else 
this.SelectColumn(e4,r5,1,true);
}
}else {
if (u7=="Normal"||u7=="ReadOnly"){
this.SelectColumn(e4,r5,1,true);
}
else 
return ;
}
}else if (e4.d1.parentNode.getAttribute("FpSpread")=="rh"){
if (n4.tagName=="INPUT"||n4.tagName=="TEXTAREA"||n4.tagName=="SELECT")
return ;
if (u7=="ExtendedSelect"||u7=="MultiSelect"){
if (this.IsRowSelected(e4,this.GetRowFromCell(e4,e4.d1)))
this.SelectRow(e4,this.GetRowFromCell(e4,e4.d1),1,false,true);
else 
this.SelectRow(e4,this.GetRowFromCell(e4,e4.d1),1,true,true);
}else {
this.SelectRow(e4,this.GetRowFromCell(e4,e4.d1),1,true);
}
}
if (e4.d1!=null){
var g0=this.CreateEvent("ActiveCellChanged");
g0.cmdID=e4.id;
g0.Row=g0.row=this.GetSheetIndex(e4,this.GetRowFromCell(e4,e4.d1));
g0.Col=g0.col=this.GetColFromCell(e4,e4.d1);
if (e4.getAttribute("LayoutMode"))
g0.InnerRow=g0.innerRow=e4.d1.parentNode.getAttribute("row");
this.FireEvent(e4,g0);
}
e4.d2=e4.d1;
if (e4.d1!=null){
e4.d3=this.GetRowFromCell(e4,e4.d1);
e4.d4=this.GetColFromCell(e4,e4.d1);
}
this.b8=n4;
this.a6=true;
}
}
this.EnableButtons(e4);
if (!this.a7&&this.b5==null&&this.b4==null){
if (e4.d1!=null&&this.IsChild(e4.d1,e4)&&!this.IsHeaderCell(this.GetCell(n4))){
var i2=this.GetEditor(e4.d1);
if (i2!=null){
if (i2.type=="submit")this.SaveData(e4);
this.a7=(i2.type!="button"&&i2.type!="submit");
this.a8=i2;
this.a9=this.GetEditorValue(i2);
i2.focus();
}
}
}
if (!this.IsControl(n4)){
if (e4!=null)this.UpdatePostbackData(e4);
return this.CancelDefault(event);
}
}
this.GetMovingCol=function (e4){
var v4=document.getElementById(e4.id+"movingCol");
if (v4==null){
v4=document.createElement("DIV");
v4.style.display="none";
v4.style.position="absolute";
v4.style.top="0px";
v4.style.left="0px";
v4.id=e4.id+"movingCol";
v4.align="center";
e4.insertBefore(v4,null);
if (e4.getAttribute("DragColumnCssClass")!=null)
v4.className=e4.getAttribute("DragColumnCssClass");
else 
v4.style.border="1px solid black";
v4.style.MozOpacity=0.50;
}
return v4;
}
this.IsControl=function (f7){
return (f7!=null&&(f7.tagName=="INPUT"||f7.tagName=="TEXTAREA"||f7.tagName=="SELECT"||f7.tagName=="OPTION"));
}
this.EnableButtons=function (e4){
var r0=this.GetCellType(e4.d1);
var m5=this.GetSelection(e4);
var m6=m5.lastChild;
var s3=e4.getAttribute("OperationMode");
var ac7=s3=="ReadOnly"||s3=="SingleSelect"||r0=="readonly";
if (!ac7){
ac7=this.AnyReadOnlyCell(e4,m6);
}
if (ac7){
var f6=this.GetCmdBtn(e4,"Copy");
this.UpdateCmdBtnState(f6,m6==null);
var f3=this.b9;
f6=this.GetCmdBtn(e4,"Paste");
this.UpdateCmdBtnState(f6,(m6==null||f3==null));
f6=this.GetCmdBtn(e4,"Clear");
this.UpdateCmdBtnState(f6,true);
}else {
var f6=this.GetCmdBtn(e4,"Copy");
this.UpdateCmdBtnState(f6,m6==null);
var f3=this.b9;
f6=this.GetCmdBtn(e4,"Paste");
this.UpdateCmdBtnState(f6,(m6==null||f3==null));
f6=this.GetCmdBtn(e4,"Clear");
this.UpdateCmdBtnState(f6,m6==null);
}
}
this.CellClicked=function (h2){
var e4=this.GetSpread(h2);
if (e4!=null){
this.SaveData(e4);
}
}
this.UpdateCmdBtnState=function (f6,disabled){
if (f6==null)return ;
if (f6.tagName=="INPUT"){
var f7=f6.disabled;
if (f7==disabled)return ;
f6.disabled=disabled;
}else {
var f7=f6.getAttribute("disabled");
if (f7==disabled)return ;
f6.setAttribute("disabled",disabled);
}
if (f6.tagName=="IMG"){
var ac8=f6.getAttribute("disabledImg");
if (disabled&&ac8!=null&&ac8!=""){
if (f6.src.indexOf(ac8)<0)f6.src=ac8;
}else {
var ac9=f6.getAttribute("enabledImg");
if (f6.src.indexOf(ac9)<0)f6.src=ac9;
}
}
}
this.MouseUp=function (event){
if (window.fpPostOn!=null)return ;
event=this.GetEvent(event);
var n4=this.GetTarget(event);
var e4=this.GetSpread(n4,true);
if (e4==null&&!this.a6){
return ;
}
if (this.dragSlideBar&&e4!=null)
{
this.dragSlideBar=false;
if (this.activePager!=null){
var ab1=this.MoveSliderBar(e4,event)-1;
this.activePager=null;
this.GotoPage(e4,ab1);
}
return ;
}
if (this.a6&&(this.b4!=null||this.b5!=null)){
if (this.b4!=null)
e4=this.GetSpread(this.b4);
else 
e4=this.GetSpread(this.b5);
}
if (e4==null)return ;
if (this.GetViewport(e4)==null)return ;
var s3=this.GetOperationMode(e4);
if (s3=="ReadOnly")return ;
var i1=true;
if (this.a6){
this.a6=false;
if (this.dragCol!=null&&this.dragCol>=0){
var ad0=(this.IsChild(n4,this.GetGroupBar(e4))||n4==this.GetGroupBar(e4));
if (!ad0&&this.GetGroupBar(e4)!=null){
var ad1=event.clientX;
var ad2=event.clientY;
var p5=e4.offsetLeft;
var t8=e4.offsetTop;
var ad3=this.GetGroupBar(e4).offsetWidth;
var ad4=this.GetGroupBar(e4).offsetHeight;
var p0=window.scrollX;
var o9=window.scrollY;
var k5=document.getElementById(e4.id+"_titleBar");
if (k5)o9-=k5.parentNode.parentNode.offsetHeight;
if (this.GetPager1(e4)!=null)o9-=this.GetPager1(e4).offsetHeight;
ad0=(p5<=p0+ad1&&p0+ad1<=p5+ad3&&t8<=o9+ad2&&o9+ad2<=t8+ad4);
}
if (e4.dragFromGroupbar){
if (ad0){
if (e4.targetCol>0)
this.Regroup(e4,this.dragCol,parseInt((e4.targetCol+1)/2));
else 
this.Regroup(e4,this.dragCol,e4.targetCol);
}else {
this.Ungroup(e4,this.dragCol,e4.targetCol);
}
}else {
if (ad0){
if (e4.allowGroup){
if (e4.targetCol>0)
this.Group(e4,this.dragCol,parseInt((e4.targetCol+1)/2));
else 
this.Group(e4,this.dragCol,e4.targetCol);
}
}else if (e4.allowColMove){
if (e4.targetCol!=null){
var g0=this.CreateEvent("ColumnDragMove");
g0.cancel=false;
g0.col=e4.selectedCols;
this.FireEvent(e4,g0);
if (!g0.cancel){
this.MoveCol(e4,this.dragCol,e4.targetCol);
var g0=this.CreateEvent("ColumnDragMoveCompleted");
g0.col=e4.selectedCols;
this.FireEvent(e4,g0);
}
}
}
}
var v4=this.GetMovingCol(e4);
if (v4!=null)
v4.style.display="none";
this.dragCol=-1;
this.dragViewCol=-1;
var ab2=this.GetPosIndicator(e4);
if (ab2!=null)
ab2.style.display="none";
e4.dragFromGroupbar=false;
e4.targetCol=null;
this.b4=this.b5=null;
}
if (this.b4!=null){
i1=false;
var ab9=event.clientX-this.b6;
var w4=parseInt(this.b4.width);
var ad5=w4;
if (isNaN(w4))w4=0;
w4+=ab9;
if (w4<1)w4=1;
var k0=parseInt(this.b4.getAttribute("index"));
var ad6=this.GetColGroup(this.GetViewport(e4));
if (ad6!=null&&ad6.childNodes.length>0){
ad5=parseInt(ad6.childNodes[k0].width);
}else {
ad5=1;
}
if (this.GetViewport(e4).rules!="rows"){
if (k0==0)ad5+=1;
if (k0==parseInt(this.colCount)-1)ad5-=1;
}
if (w4!=ad5&&event.clientX!=this.b7)
{
this.SetColWidth(e4,k0,w4);
var g0=this.CreateEvent("ColWidthChanged");
g0.col=k0;
g0.width=w4;
this.FireEvent(e4,g0);
}
this.ScrollView(e4);
this.PaintFocusRect(e4);
}else if (this.b5!=null){
i1=false;
var ab9=event.clientY-this.b7;
var ac1=this.b5.offsetHeight+ab9;
if (ac1<1){
ac1=1;
ab9=1-this.b5.offsetHeight;
}
this.b5.cells[0].style.posHeight=this.b5.cells[1].style.posHeight=ac1;
this.b5.style.cursor="auto";
var i3=null;
i3=this.GetViewport(e4);
if (typeof(i3.rows[this.b5.rowIndex])!="undefined"&&
typeof(i3.rows[this.b5.rowIndex].cells[0])!="undefined")
{
i3.rows[this.b5.rowIndex].cells[0].style.height=this.b5.cells[0].style.height;
}
var p4=this.AddRowInfo(e4,this.b5.getAttribute("FpKey"));
if (p4!=null){
if (this.b5.cells[0])
this.SetRowHeight(e4,p4,parseInt(this.b5.cells[0].style.posHeight));
else 
this.SetRowHeight(e4,p4,parseInt(this.b5.style.height));
}
if (this.b6!=event.clientY){
var g0=this.CreateEvent("RowHeightChanged");
g0.row=this.GetRowFromCell(e4,this.b5.cells[0]);
g0.height=this.b5.offsetHeight;
this.FireEvent(e4,g0);
}
var i4=this.GetParentSpread(e4);
if (i4!=null)this.UpdateRowHeight(i4,e4);
var e7=this.GetTopSpread(e4);
this.SizeAll(e7);
this.Refresh(e7);
this.ScrollView(e4);
this.PaintFocusRect(e4);
}else {
}
if (this.b8!=null){
this.b8=null;
}
}
if (i1)i1=!this.IsControl(n4);
if (i1&&this.HitCommandBar(n4))return ;
var ad7=false;
var m5=this.GetSelection(e4);
if (m5!=null){
var m6=m5.firstChild;
var h7=new this.Range();
if (m6!=null){
h7.row=this.GetRowByKey(e4,m6.getAttribute("row"));
h7.col=this.GetColByKey(e4,m6.getAttribute("col"));
h7.rowCount=parseInt(m6.getAttribute("rowcount"));
h7.colCount=parseInt(m6.getAttribute("colcount"));
}
switch (e4.d7){
case "":
var g7=this.GetViewport(e4).rows;
for (var e9=h7.row;e9<h7.row+h7.rowCount&&e9<g7.length;e9++){
if (g7[e9].cells.length>0&&g7[e9].cells[0].firstChild!=null&&g7[e9].cells[0].firstChild.nodeName!="#text"){
if (g7[e9].cells[0].firstChild.getAttribute("FpSpread")=="Spread"){
ad7=true;
break ;
}
}
}
break ;
case "c":
var i3=this.GetViewport(e4);
for (var e9=0;e9<i3.rows.length;e9++){
if (this.IsChildSpreadRow(e4,i3,e9)){
ad7=true;
break ;
}
}
break ;
case "r":
var i3=this.GetViewport(e4);
var v0=h7.rowCount;
for (var e9=h7.row;e9<h7.row+v0&&e9<i3.rows.length;e9++){
if (this.IsChildSpreadRow(e4,i3,e9)){
ad7=true;
break ;
}
}
}
}
if (ad7){
var f6=this.GetCmdBtn(e4,"Copy");
this.UpdateCmdBtnState(f6,true);
f6=this.GetCmdBtn(e4,"Paste");
this.UpdateCmdBtnState(f6,true);
f6=this.GetCmdBtn(e4,"Clear");
this.UpdateCmdBtnState(f6,true);
}
var j5=this.IsXHTML(e4);
if (j5){
var e7=this.GetTopSpread(e4);
var f8=document.getElementById(e7.id+"_textBox");
if (f8!=null){
f8.style.top=event.clientY-e4.offsetTop;
f8.style.left=event.clientX-e4.offsetLeft;
}
}
if (i1)this.Focus(e4);
}
this.UpdateRowHeight=function (i4,child){
var l6=child.parentNode;
while (l6!=null){
if (l6.tagName=="TR")break ;
l6=l6.parentNode;
}
var j5=this.IsXHTML(i4);
if (l6!=null){
var e8=l6.rowIndex;
if (this.GetRowHeader(i4)!=null){
var p2=0;
if (this.GetColHeader(child)!=null)p2=this.GetColHeader(child).offsetHeight;
if (this.GetRowHeader(child)!=null)p2+=this.GetRowHeader(child).offsetHeight;
if (!j5)p2-=this.GetViewport(i4).cellSpacing;
if (this.GetViewport(i4).cellSpacing==0){
this.GetRowHeader(i4).rows[e8].cells[0].style.posHeight=p2;
if (this.GetParentSpread(i4)!=null){
this.GetRowHeader(i4).parentNode.style.posHeight=this.GetRowHeader(i4).offsetHeight;
}
}
else 
this.GetRowHeader(i4).rows[e8].cells[0].style.posHeight=(p2+2);
this.GetViewport(i4).rows[e8].cells[0].style.posHeight=p2;
if (!j5)p2-=1;
child.style.posHeight=p2;
}
}
var ad8=this.GetParentSpread(i4);
if (ad8!=null)
this.UpdateRowHeight(ad8,i4);
}
this.MouseOut=function (){
if (!this.a6&&this.b4!=null&&this.b4.style!=null)this.b4.style.cursor="auto";
}
this.KeyDown=function (e4,event){
if (window.fpPostOn!=null)return ;
if (!e4.ProcessKeyMap(event))return ;
if (event.keyCode==this.space&&e4.d1!=null){
var u7=this.GetOperationMode(e4);
if (u7=="MultiSelect"){
if (this.IsRowSelected(e4,this.GetRowFromCell(e4,e4.d1)))
this.SelectRow(e4,this.GetRowFromCell(e4,e4.d1),1,false,true);
else 
this.SelectRow(e4,this.GetRowFromCell(e4,e4.d1),1,true,true);
return ;
}
}
var i2=false;
if (this.a7&&this.a8!=null){
var ad9=this.GetEditor(this.a8);
i2=(ad9!=null);
}
if (event.keyCode!=this.enter&&event.keyCode!=this.tab&&(this.a7&&!i2)&&this.a8.tagName=="SELECT")return ;
switch (event.keyCode){
case this.left:
case this.right:
if (i2){
var ae0=this.a8.getAttribute("FpEditor");
if (this.a7&&ae0=="ExtenderEditor"){
var ae1=FpExtender.Util.getEditor(this.a8);
if (ae1&&ae1.type!="text")this.EndEdit();
}
if (ae0!="RadioButton"&&ae0!="ExtenderEditor")this.EndEdit();
}
if (!this.a7){
this.NextCell(e4,event,event.keyCode);
}
break ;
case this.up:
case this.down:
case this.enter:
if (this.a8!=null&&this.a8.tagName=="TEXTAREA")return ;
if (event.keyCode!=event.DOM_VK_RETURN&&i2&&this.a7&&this.a8.getAttribute("FpEditor")=="ExtenderEditor"){
var ae2=this.a8.getAttribute("Extenders");
if (ae2&&ae2.indexOf("AutoCompleteExtender")!=-1)return ;
}
if (event.keyCode==event.DOM_VK_RETURN)this.CancelDefault(event);
if (this.a7){
var n7=this.EndEdit();
if (!n7)return ;
}
this.NextCell(e4,event,event.keyCode);
var e7=this.GetTopSpread(e4);
var f8=document.getElementById(e7.id+"_textBox");
if (this.enter==event.keyCode)f8.focus();
break ;
case this.tab:
if (this.a7){
var n7=this.EndEdit();
if (!n7)return ;
}
var n6=this.GetProcessTab(e4);
var ae3=(n6=="true"||n6=="True");
if (ae3)this.NextCell(e4,event,event.keyCode);
break ;
case this.shift:
break ;
case this.home:
case this.end:
case this.pup:
case this.pdn:
this.CancelDefault(event);
if (!this.a7){
this.NextCell(e4,event,event.keyCode);
}
break ;
default :
if (event.keyCode==67&&event.ctrlKey&&(!this.a7||i2))this.Copy(e4);
else if (event.keyCode==86&&event.ctrlKey&&(!this.a7||i2))this.Paste(e4);
else if (event.keyCode==88&&event.ctrlKey&&(!this.a7||i2))this.Clear(e4);
else if (!this.a7&&e4.d1!=null&&!this.IsHeaderCell(e4.d1)&&!event.ctrlKey&&!event.altKey){
this.StartEdit(e4,e4.d1);
}
break ;
}
}
this.GetProcessTab=function (e4){
return e4.getAttribute("ProcessTab");
}
this.ExpandRow=function (e4,i5){
var s1=e4.getAttribute("name");
var y3=(e4.getAttribute("ajax")!="false");
if (y3)
this.SyncData(s1,"ExpandView,"+i5,e4);
else 
__doPostBack(s1,"ExpandView,"+i5);
}
this.SortColumn=function (e4,column){
var s1=e4.getAttribute("name");
var y3=(e4.getAttribute("ajax")!="false");
if (y3)
this.SyncData(s1,"SortColumn,"+column,e4);
else 
__doPostBack(s1,"SortColumn,"+column);
}
this.Filter=function (event,e4){
var n4=this.GetTarget(event);
var f7=n4.value;
if (n4.tagName=="SELECT"){
var y2=new RegExp("\\s*");
var ae4=new RegExp("\\S*");
var s4=n4[n4.selectedIndex].text;
var ae5="";
var e9=0;
var e8=f7.length;
while (e8>0){
var h3=f7.match(y2);
if (h3!=null){
ae5+=h3[0];
e9=h3[0].length;
e8-=e9;
f7=f7.substring(e9);
h3=f7.match(ae4);
if (h3!=null){
e9=h3[0].length;
e8-=e9;
f7=f7.substring(e9);
}
}else {
break ;
}
h3=s4.match(ae4);
if (h3!=null){
ae5+=h3[0];
e9=h3[0].length;
s4=s4.substring(e9);
h3=s4.match(y2);
if (h3!=null){
e9=h3[0].length;
s4=s4.substring(e9);
}
}else {
break ;
}
}
f7=ae5;
}
var y3=(e4.getAttribute("ajax")!="false");
if (y3)
this.SyncData(n4.name,f7,e4);
else 
__doPostBack(n4.name,f7);
}
this.MoveCol=function (e4,from,to){
var s1=e4.getAttribute("name");
if (e4.selectedCols&&e4.selectedCols.length>0){
var ae6=[];
for (var e9=0;e9<e4.selectedCols.length;e9++)
ae6[e9]=this.GetSheetColIndex(e4,e4.selectedCols[e9]);
var ae7=ae6.join("+");
this.MoveMultiCol(e4,ae7,to);
return ;
}
var y3=(e4.getAttribute("ajax")!="false");
if (y3)
this.SyncData(s1,"MoveCol,"+from+","+to,e4);
else 
__doPostBack(s1,"MoveCol,"+from+","+to);
}
this.MoveMultiCol=function (e4,ae7,to){
var s1=e4.getAttribute("name");
var y3=(e4.getAttribute("ajax")!="false");
if (y3)
this.SyncData(s1,"MoveCol,"+ae7+","+to,e4);
else 
__doPostBack(s1,"MoveCol,"+ae7+","+to);
}
this.Group=function (e4,m4,toCol){
var s1=e4.getAttribute("name");
if (e4.selectedCols&&e4.selectedCols.length>0){
var ae6=[];
for (var e9=0;e9<e4.selectedCols.length;e9++)
ae6[e9]=this.GetSheetColIndex(e4,e4.selectedCols[e9]);
var ae7=ae6.join("+");
this.GroupMultiCol(e4,ae7,toCol);
e4.selectedCols=[];
return ;
}
var y3=(e4.getAttribute("ajax")!="false");
if (y3)
this.SyncData(s1,"Group,"+m4+","+toCol,e4);
else 
__doPostBack(s1,"Group,"+m4+","+toCol);
}
this.GroupMultiCol=function (e4,ae7,toCol){
var s1=e4.getAttribute("name");
var y3=(e4.getAttribute("ajax")!="false");
if (y3)
this.SyncData(s1,"Group,"+ae7+","+toCol,e4);
else 
__doPostBack(s1,"Group,"+ae7+","+toCol);
}
this.Ungroup=function (e4,m4,toCol){
var s1=e4.getAttribute("name");
var y3=(e4.getAttribute("ajax")!="false");
if (y3)
this.SyncData(s1,"Ungroup,"+m4+","+toCol,e4);
else 
__doPostBack(s1,"Ungroup,"+m4+","+toCol);
}
this.Regroup=function (e4,fromCol,toCol){
var s1=e4.getAttribute("name");
var y3=(e4.getAttribute("ajax")!="false");
if (y3)
this.SyncData(s1,"Regroup,"+fromCol+","+toCol,e4);
else 
__doPostBack(s1,"Regroup,"+fromCol+","+toCol);
}
this.ProcessData=function (){
try {
var ae8=this;
ae8.removeEventListener("load",the_fpSpread.ProcessData,false);
var n4=window.srcfpspread;
n4=n4.split(":").join("_");
var ae9=window.fpcommand;
var af0=document;
var af1=af0.getElementById(n4+"_buff");
if (af1==null){
af1=af0.createElement("iframe");
af1.id=n4+"_buff";
af1.style.display="none";
af0.body.appendChild(af1);
}
var e4=af0.getElementById(n4);
the_fpSpread.CloseWaitMsg(e4);
if (af1==null)return ;
var af2=ae8.responseText;
af1.contentWindow.document.body.innerHTML=af2;
var n6=af1.contentWindow.document.getElementById(n4+"_values");
if (n6!=null){
var s0=n6.getElementsByTagName("data")[0];
var m6=s0.firstChild;
the_fpSpread.error=false;
while (m6!=null){
var g9=the_fpSpread.GetRowByKey(e4,m6.getAttribute("r"));
var h1=the_fpSpread.GetColByKey(e4,m6.getAttribute("c"));
var y5=the_fpSpread.GetValue(e4,g9,h1);
if (m6.innerHTML!=y5){
var i1=the_fpSpread.GetFormula(e4,g9,h1);
var i8=the_fpSpread.GetCellByRowCol(e4,g9,h1);
the_fpSpread.SetCellValueFromView(i8,m6.innerHTML,true);
i8.setAttribute("FpFormula",i1);
}
m6=m6.nextSibling;
}
the_fpSpread.ClearCellData(e4);
}else {
the_fpSpread.UpdateSpread(af0,af1,n4,af2,ae9);
}
var y4=af0.getElementsByTagName("FORM");
y4[0].__EVENTTARGET.value="";
y4[0].__EVENTARGUMENT.value="";
var y5=af0.getElementsByName("__VIEWSTATE")[0];
var f7=af1.contentWindow.document.getElementsByName("__VIEWSTATE")[0];
if (y5!=null&&f7!=null)y5.value=f7.value;
y5=af0.getElementsByName("__EVENTVALIDATION");
f7=af1.contentWindow.document.getElementsByName("__EVENTVALIDATION");
if (y5!=null&&f7!=null&&y5.length>0&&f7.length>0)
y5[0].value=f7[0].value;
af1.contentWindow.document.location="about:blank";
window.fpPostOn=null;
d8=null;
}catch (g0){
window.fpPostOn=null;
d8=null;
}
var e4=the_fpSpread.GetTopSpread(af0.getElementById(n4));
var g0=the_fpSpread.CreateEvent("CallBackStopped");
g0.command=ae9;
the_fpSpread.FireEvent(e4,g0);
};
this.UpdateSpread=function (af0,af1,n4,af2,ae9){
var e4=the_fpSpread.GetTopSpread(af0.getElementById(n4));
var r1=af1.contentWindow.document.getElementById(e4.id);
if (r1!=null){
if (typeof(Sys)!=='undefined'){
FarPoint.System.ExtenderHelper.saveLoadedExtenderScripts(e4);
}
the_fpSpread.error=(r1.getAttribute("error")=="true");
if (ae9=="LoadOnDemand"&&!the_fpSpread.error){
var af3=this.GetElementById(e4,e4.id+"_data");
var af4=this.GetElementById(r1,e4.id+"_data");
if (af3!=null&&af4!=null)af3.setAttribute("data",af4.getAttribute("data"));
var af5=r1.getElementsByTagName("style");
if (af5!=null){
for (var e9=0;e9<af5.length;e9++){
if (af5[e9]!=null&&af5[e9].innerHTML!=null&&af5[e9].innerHTML.indexOf(e4.id+"msgStyle")<0)
e4.appendChild(af5[e9].cloneNode(true));
}
}
var af6=this.GetElementById(e4,e4.id+"_LoadInfo");
var af7=this.GetElementById(r1,e4.id+"_LoadInfo");
if (af6!=null&&af7!=null)af6.value=af7.value;
var af8=false;
var af9=this.GetElementById(r1,e4.id+"_rowHeader");
if (af9!=null){
af9=af9.firstChild;
af8=(af9.rows.length>1);
var j1=this.GetRowHeader(e4);
this.LoadRows(j1,af9,true);
}
var ag0=this.GetElementById(r1,e4.id+"_viewport");
if (ag0!=null){
af8=(ag0.rows.length>0);
var e5=this.GetViewport(e4);
this.LoadRows(e5,ag0,false);
}
the_fpSpread.Init(e4);
the_fpSpread.LoadScrollbarState(e4);
the_fpSpread.Focus(e4);
if (af8)
e4.LoadState=null;
else 
e4.LoadState="complete";
if (typeof(Sys)!=='undefined'){
FarPoint.System.ExtenderHelper.loadExtenderScripts(e4,af1.contentWindow.document);
}
}else {
e4.innerHTML=r1.innerHTML;
the_fpSpread.CopySpreadAttrs(r1,e4);
if (typeof(Sys)!=='undefined'){
FarPoint.System.ExtenderHelper.loadExtenderScripts(e4,af1.contentWindow.document);
}
var ag1=af1.contentWindow.document.getElementById(e4.id+"_initScript");
eval(ag1.value);
for (var e9=0;e9<af1.contentWindow.document.styleSheets.length;e9++){
for (var h9=0;h9<af1.contentWindow.document.styleSheets[e9].rules.length;h9++){
var ag2=af1.contentWindow.document.styleSheets[e9].rules[h9];
var ag3={styleSheetIndex:-1,ruleIndex:-1};
for (var ag4=0;ag4<af0.styleSheets.length;ag4++){
for (var ag5=0;ag5<af0.styleSheets[ag4].rules.length;ag5++){
if (af0.styleSheets[ag4].rules[ag5].selectorText==ag2.selectorText){
ag3.styleSheetIndex=ag4;
ag3.ruleIndex=ag5;
}
}
}
if (ag3.styleSheetIndex>-1&&ag3.ruleIndex>-1)
af0.styleSheets[ag3.styleSheetIndex].deleteRule(ag3.ruleIndex);
af0.styleSheets[0].addRule(ag2.selectorText,ag2.style.cssText);
}
}
}
}else {
the_fpSpread.error=true;
}
}
this.LoadRows=function (e5,ag0,isHeader){
if (e5==null||ag0==null)return ;
var ag6=e5.tBodies[0];
var v0=ag0.rows.length;
var ag7=null;
if (isHeader){
v0--;
if (ag6.rows.length>0)ag7=ag6.rows[ag6.rows.length-1];
}
for (var e9=0;e9<v0;e9++){
var ag8=ag0.rows[e9].cloneNode(false);
ag6.insertBefore(ag8,ag7);
ag8.innerHTML=ag0.rows[e9].innerHTML;
}
if (!isHeader){
for (var e9=0;e9<ag0.parentNode.childNodes.length;e9++){
var x9=ag0.parentNode.childNodes[e9];
if (x9!=ag0){
e5.parentNode.insertBefore(x9.cloneNode(true),null);
}
}
}
}
this.CopySpreadAttr=function (aa0,dest,attrName){
var ag9=aa0.getAttribute(attrName);
var ah0=dest.getAttribute(attrName);
if (ag9!=null||ah0!=null){
if (ag9==null)
dest.removeAttribute(attrName);
else 
dest.setAttribute(attrName,ag9);
}
}
this.CopySpreadAttrs=function (aa0,dest){
this.CopySpreadAttr(aa0,dest,"totalRowCount");
this.CopySpreadAttr(aa0,dest,"pageCount");
this.CopySpreadAttr(aa0,dest,"loadOnDemand");
this.CopySpreadAttr(aa0,dest,"allowGroup");
this.CopySpreadAttr(aa0,dest,"colMove");
this.CopySpreadAttr(aa0,dest,"showFocusRect");
this.CopySpreadAttr(aa0,dest,"FocusBorderColor");
this.CopySpreadAttr(aa0,dest,"FocusBorderStyle");
this.CopySpreadAttr(aa0,dest,"FpDefaultEditorID");
this.CopySpreadAttr(aa0,dest,"hierView");
this.CopySpreadAttr(aa0,dest,"IsNewRow");
this.CopySpreadAttr(aa0,dest,"cmdTop");
this.CopySpreadAttr(aa0,dest,"ProcessTab");
this.CopySpreadAttr(aa0,dest,"AcceptFormula");
this.CopySpreadAttr(aa0,dest,"EditMode");
this.CopySpreadAttr(aa0,dest,"AllowInsert");
this.CopySpreadAttr(aa0,dest,"AllowDelete");
this.CopySpreadAttr(aa0,dest,"error");
this.CopySpreadAttr(aa0,dest,"ajax");
this.CopySpreadAttr(aa0,dest,"autoCalc");
this.CopySpreadAttr(aa0,dest,"multiRange");
this.CopySpreadAttr(aa0,dest,"rowFilter");
this.CopySpreadAttr(aa0,dest,"OperationMode");
this.CopySpreadAttr(aa0,dest,"selectedForeColor");
this.CopySpreadAttr(aa0,dest,"selectedBackColor");
this.CopySpreadAttr(aa0,dest,"anchorBackColor");
this.CopySpreadAttr(aa0,dest,"columnHeaderAutoTextIndex");
this.CopySpreadAttr(aa0,dest,"SelectionPolicy");
this.CopySpreadAttr(aa0,dest,"ShowHeaderSelection");
this.CopySpreadAttr(aa0,dest,"EnableRowEditTemplate");
this.CopySpreadAttr(aa0,dest,"scrollContent");
this.CopySpreadAttr(aa0,dest,"scrollContentColumns");
this.CopySpreadAttr(aa0,dest,"scrollContentTime");
this.CopySpreadAttr(aa0,dest,"scrollContentMaxHeight");
dest.tabIndex=aa0.tabIndex;
if (dest.style!=null&&aa0.style!=null){
if (dest.style.width!=aa0.style.width)dest.style.width=aa0.style.width;
if (dest.style.height!=aa0.style.height)dest.style.height=aa0.style.height;
if (dest.style.border!=aa0.style.border)dest.style.border=aa0.style.border;
}
}
this.Clone=function (l4){
var f7=document.createElement(l4.tagName);
f7.id=l4.id;
var h1=l4.firstChild;
while (h1!=null){
var j3=this.Clone(h1);
f7.appendChild(j3);
h1=h1.nextSibling;
}
return f7;
}
this.FireEvent=function (e4,g0){
if (e4==null||g0==null)return ;
var e7=this.GetTopSpread(e4);
if (e7!=null){
g0.spread=e4;
e7.dispatchEvent(g0);
}
}
this.SyncData=function (s1,ae9,e4,asyncCallBack){
if (window.fpPostOn!=null){
return ;
}
var g0=this.CreateEvent("CallBackStart");
g0.cancel=false;
g0.command=ae9;
if (asyncCallBack==null)asyncCallBack=false;
g0.async=asyncCallBack;
if (e4==null){
var j3=s1.split(":").join("_");
e4=document.getElementById(j3);
}
if (e4!=null){
var e7=this.GetTopSpread(e4);
this.FireEvent(e4,g0);
}
if (g0.cancel){
the_fpSpread.ClearCellData(e4);
return ;
}
if (ae9!=null&&(ae9.indexOf("SelectView,")==0||ae9=="Next"||ae9=="Prev"||ae9.indexOf("Group,")==0||ae9.indexOf("Page,")==0))
e4.LoadState=null;
var ah1=g0.async;
if (ah1){
this.OpenWaitMsg(e4);
}
window.fpPostOn=true;
if (this.error)ae9="update";
try {
var y4=document.getElementsByTagName("FORM");
if (y4==null&&y4.length==0)return ;
y4[0].__EVENTTARGET.value=s1;
y4[0].__EVENTARGUMENT.value=encodeURIComponent(ae9);
var ah2=y4[0].action;
var f7;
if (ah2.indexOf("?")>-1){
f7="&";
}
else 
{
f7="?";
}
ah2=ah2+f7;
var f3=this.CollectData();
var af2="";
var ae8=(window.XMLHttpRequest)?new XMLHttpRequest():new ActiveXObject("Microsoft.XMLHTTP");
if (ae8==null)return ;
ae8.open("POST",ah2,ah1);
ae8.setRequestHeader("Content-Type","application/x-www-form-urlencoded");
if (e4!=null)
window.srcfpspread=e4.id;
else 
window.srcfpspread=s1;
window.fpcommand=ae9;
this.AttachEvent(ae8,"load",the_fpSpread.ProcessData,false);
ae8.send(f3);
}catch (g0){
window.fpPostOn=false;
d8=null;
}
};
this.CollectData=function (){
var y4=document.getElementsByTagName("FORM");
var f7;
var g2="fpcallback=true&";
for (var e9=0;e9<y4[0].elements.length;e9++){
f7=y4[0].elements[e9];
var ah3=f7.tagName.toLowerCase();
if (ah3=="input"){
var ah4=f7.type;
if (ah4=="hidden"||ah4=="text"||ah4=="password"||((ah4=="checkbox"||ah4=="radio")&&f7.checked)){
g2+=(f7.name+"="+encodeURIComponent(f7.value)+"&");
}
}else if (ah3=="select"){
if (f7.childNodes!=null){
for (var h9=0;h9<f7.childNodes.length;h9++){
var p3=f7.childNodes[h9];
if (p3!=null&&p3.tagName!=null&&p3.tagName.toLowerCase()=="option"&&p3.selected){
g2+=(f7.name+"="+encodeURIComponent(p3.value)+"&");
}
}
}
}else if (ah3=="textarea"){
g2+=(f7.name+"="+encodeURIComponent(f7.value)+"&");
}
}
return g2;
};
this.ClearCellData=function (e4){
var f3=this.GetData(e4);
var ah5=f3.getElementsByTagName("root")[0];
var f4=ah5.getElementsByTagName("data")[0];
if (f4==null)return null;
if (e4.d8!=null){
var i5=e4.d8.firstChild;
while (i5!=null){
var g9=i5.getAttribute("key");
var ah6=i5.firstChild;
while (ah6!=null){
var h1=ah6.getAttribute("key");
var ah7=f4.firstChild;
while (ah7!=null){
var h3=ah7.getAttribute("key");
if (g9==h3){
var ah8=false;
var ah9=ah7.firstChild;
while (ah9!=null){
var h4=ah9.getAttribute("key");
if (h1==h4){
ah7.removeChild(ah9);
ah8=true;
break ;
}
ah9=ah9.nextSibling;
}
if (ah8)break ;
}
ah7=ah7.nextSibling;
}
ah6=ah6.nextSibling;
}
i5=i5.nextSibling;
}
}
e4.d8=null;
var f6=this.GetCmdBtn(e4,"Cancel");
if (f6!=null)
this.UpdateCmdBtnState(f6,true);
}
this.StorePostData=function (e4){
var f3=this.GetData(e4);
var f4=f3.getElementsByTagName("root")[0];
var aa9=f4.getElementsByTagName("data")[0];
if (aa9!=null)e4.d8=aa9.cloneNode(true);
}
this.ShowMessage=function (e4,u3,i5,m4,time){
var m7=e4.GetRowCount();
var g8=e4.GetColCount();
if (i5==null||m4==null||i5<0||i5>=m7||m4<0||m4>=g8){
i5=-1;
m4=-1;
}
this.ShowMessageInner(e4,u3,i5,m4,time);
}
this.HideMessage=function (e4,i5,m4){
var m7=e4.GetRowCount();
var g8=e4.GetColCount();
if (i5==null||m4==null||i5<0||i5>=m7||m4<0||m4>=g8)
if (e4.msgList&&e4.msgList.centerMsg&&e4.msgList.centerMsg.msgBox.IsVisible)
e4.msgList.centerMsg.msgBox.Hide();
var ai0=this.GetMsgObj(e4,i5,m4);
if (ai0&&ai0.msgBox.IsVisible){
ai0.msgBox.Hide();
}
}
this.ShowMessageInner=function (e4,u3,i5,m4,time){
var ai0=this.GetMsgObj(e4,i5,m4);
if (ai0){
if (ai0.timer)
ai0.msgBox.Hide();
}
else 
ai0=this.CreateMsgObj(e4,i5,m4);
var ai1=ai0.msgBox;
ai1.Show(e4,this,u3);
if (time&&time>0)
ai0.timer=setTimeout(function (){ai1.Hide();},time);
this.SetMsgObj(e4,ai0);
}
this.GetMsgObj=function (e4,i5,m4){
var ai0;
var ai2=e4.msgList;
if (ai2){
if (i5==-1&&m4==-1)
ai0=ai2.centerMsg;
else if (i5==-2)
ai0=ai2.hScrollMsg;
else if (m4==-2)
ai0=ai2.vScrollMsg;
else {
if (ai2[i5])
ai0=ai2[i5][m4];
}
}
return ai0;
}
this.SetMsgObj=function (e4,ai0){
var ai2=e4.msgList;
if (ai0.row==-1&&ai0.col==-1)
ai2.centerMsg=ai0;
else if (ai0.row==-2)
ai2.hScrollMsg=ai0;
else if (ai0.col==-2)
ai2.vScrollMsg=ai0;
else {
if (!ai2[ai0.row])ai2[ai0.row]=new Array();
ai2[ai0.row][ai0.col]=ai0;
}
}
var ai3=null;
this.CreateMsgObj=function (e4,i5,m4){
var ai1=document.createElement("div");
var ai0={row:i5,col:m4,msgBox:ai1};
var ai4=null;
if (i5!=-2&&m4!=-2){
ai1.style.border="1px solid black";
ai1.style.background="yellow";
ai1.style.color="red";
}
else {
ai1.style.border="1px solid #55678e";
ai1.style.fontSize="small";
ai1.style.background="#E6E9ED";
ai1.style.color="#4c5b7f";
this.GetScrollingContentStyle(e4);
ai4=ai3;
}
if (ai4!=null){
if (ai4.fontFamily!=null)
ai1.style.fontFamily=ai4.fontFamily;
if (ai4.fontSize!=null)
ai1.style.fontSize=ai4.fontSize;
if (ai4.fontStyle!=null)
ai1.style.fontStyle=ai4.fontStyle;
if (ai4.fontVariant!=null)
ai1.style.fontVariant=ai4.fontVariant;
if (ai4.fontWeight!=null)
ai1.style.fontWeight=ai4.fontWeight;
if (ai4.backgroundColor!=null)
ai1.style.backgroundColor=ai4.backgroundColor;
if (ai4.color!=null)
ai1.style.color=ai4.color;
}
ai1.style.position="absolute";
ai1.style.overflow="hidden";
ai1.style.display="block";
ai1.style.marginLeft=0;
ai1.style.marginTop=2;
ai1.style.marginRight=0;
ai1.style.marginBottom=0;
ai1.msgObj=ai0;
ai1.Show=function (r1,fpObj,u3){
var v2=fpObj.GetMsgPos(r1,this.msgObj.row,this.msgObj.col);
var e6=fpObj.GetCommandBar(r1);
var ai5=fpObj.GetGroupBar(r1);
this.style.visibility="visible";
this.style.display="block";
if (u3){
this.style.left=""+0+"px";
this.style.top=""+0+"px";
this.style.width="auto";
this.innerHTML=u3;
}
var ai6=false;
var ai7=(r1.style.position=="relative"||r1.style.position=="absolute");
var ai8=v2.top;
var ai9=v2.left;
var p3=e4.offsetParent;
while ((p3.tagName=="TD"||p3.tagName=="TR"||p3.tagName=="TBODY"||p3.tagName=="TABLE")&&p3.style.position!="relative"&&p3.style.position!="absolute")
p3=p3.offsetParent;
if (this.msgObj.row>=0&&this.msgObj.col>=0){
if (!ai7&&ai6&&p3){
var aj0=fpObj.GetLocation(r1);
var aj1=fpObj.GetLocation(p3);
ai8+=aj0.y-aj1.y;
ai9+=aj0.x-aj1.x;
if (p3.tagName!=="BODY"){
ai8-=fpObj.GetBorderWidth(p3,0);
ai9-=fpObj.GetBorderWidth(p3,3);
}
}
var aj2=fpObj.GetViewPortByRowCol(r1,this.msgObj.row,this.msgObj.col);
if (!this.parentNode&&aj2&&aj2.parentNode)aj2.parentNode.insertBefore(ai1,null);
var i9=this.offsetWidth;
this.style.left=""+ai9+"px";
if (!ai6&&aj2&&aj2.parentNode&&ai9+i9>aj2.offsetWidth)
this.style.width=""+(v2.a5-2)+"px";
else if (parseInt(this.style.width)!=i9)
this.style.width=""+i9+"px";
if (!ai6&&aj2!=null&&ai8>=aj2.offsetHeight-2)ai8-=v2.a4+this.offsetHeight+3;
this.style.top=""+ai8+"px";
}
else {
if (!ai7&&p3){
var aj0=fpObj.GetLocation(r1);
var aj1=fpObj.GetLocation(p3);
ai8+=aj0.y-aj1.y;
ai9+=aj0.x-aj1.x;
if (p3.tagName!=="BODY"){
ai8-=fpObj.GetBorderWidth(p3,0);
ai9-=fpObj.GetBorderWidth(p3,3);
}
}
var aj3=20;
if (!this.parentNode)r1.insertBefore(ai1,null);
if (this.offsetWidth+aj3<r1.offsetWidth)
ai9+=(r1.offsetWidth-this.offsetWidth-aj3)/(this.msgObj.row==-2?1:2);
else 
this.style.width=""+(r1.offsetWidth-aj3)+"px";
if (this.offsetHeight<r1.offsetHeight)
ai8+=(r1.offsetHeight-this.offsetHeight)/(this.msgObj.col==-2?1:2);
if (this.msgObj.col==-2){
var aj4=fpObj.GetColFooter(r1);
if (aj4)ai8-=aj4.offsetHeight;
var e6=fpObj.GetCommandBar(r1);
if (e6)ai8-=e6.offsetHeight;
ai8-=aj3;
}
this.style.top=""+ai8+"px";
this.style.left=""+ai9+"px";
}
this.IsVisible=true;
};
ai1.Hide=function (){
this.style.visibility="hidden";
this.style.display="none";
this.IsVisible=false;
if (this.msgObj.timer){
clearTimeout(this.msgObj.timer);
this.msgObj.timer=null;
}
this.innerHTML="";
};
return ai0;
}
this.GetLocation=function (ele){
if ((ele.window&&ele.window===ele)||ele.nodeType===9)return {x:0,y:0};
var aj5=0;
var aj6=0;
var aj7=null;
var aj8=null;
var aj9=null;
for (var i4=ele;i4;aj7=i4,aj8=aj9,i4=i4.offsetParent){
var ah3=i4.tagName;
aj9=this.GetCurrentStyle2(i4);
if ((i4.offsetLeft||i4.offsetTop)&&
!((ah3==="BODY")&&
(!aj8||aj8.position!=="absolute"))){
aj5+=i4.offsetLeft;
aj6+=i4.offsetTop;
}
if (aj7!==null&&aj9){
if ((ah3!=="TABLE")&&(ah3!=="TD")&&(ah3!=="HTML")){
aj5+=parseInt(aj9.borderLeftWidth)||0;
aj6+=parseInt(aj9.borderTopWidth)||0;
}
if (ah3==="TABLE"&&
(aj9.position==="relative"||aj9.position==="absolute")){
aj5+=parseInt(aj9.marginLeft)||0;
aj6+=parseInt(aj9.marginTop)||0;
}
}
}
aj9=this.GetCurrentStyle2(ele);
var ak0=aj9?aj9.position:null;
if (!ak0||(ak0!=="absolute")){
for (var i4=ele.parentNode;i4;i4=i4.parentNode){
ah3=i4.tagName;
if ((ah3!=="BODY")&&(ah3!=="HTML")&&(i4.scrollLeft||i4.scrollTop)){
aj5-=(i4.scrollLeft||0);
aj6-=(i4.scrollTop||0);
aj9=this.GetCurrentStyle2(i4);
if (aj9){
aj5+=parseInt(aj9.borderLeftWidth)||0;
aj6+=parseInt(aj9.borderTopWidth)||0;
}
}
}
}
return {x:aj5,y:aj6};
}
var ak1=["borderTopWidth","borderRightWidth","borderBottomWidth","borderLeftWidth"];
var ak2=["borderTopStyle","borderRightStyle","borderBottomStyle","borderLeftStyle"];
var ak3;
this.GetBorderWidth=function (ele,side){
if (!this.GetBorderVisible(ele,side))return 0;
var m2=this.GetCurrentStyle(ele,ak1[side]);
return this.ParseBorderWidth(m2);
}
this.GetBorderVisible=function (ele,side){
return this.GetCurrentStyle(ele,ak2[side])!="none";
}
this.GetWindow=function (ele){
var af0=ele.ownerDocument||ele.document||ele;
return af0.defaultView||af0.parentWindow;
}
this.GetCurrentStyle2=function (ele){
if (ele.nodeType===3)return null;
var i9=this.GetWindow(ele);
if (ele.documentElement)ele=ele.documentElement;
var ak4=(i9&&(ele!==i9))?i9.getComputedStyle(ele,null):ele.style;
return ak4;
}
this.GetCurrentStyle=function (ele,attribute,defaultValue){
var ak5=null;
if (ele){
if (ele.currentStyle){
ak5=ele.currentStyle[attribute];
}
else if (document.defaultView&&document.defaultView.getComputedStyle){
var ak6=document.defaultView.getComputedStyle(ele,null);
if (ak6){
ak5=ak6[attribute];
}
}
if (!ak5&&ele.style.getPropertyValue){
ak5=ele.style.getPropertyValue(attribute);
}
else if (!ak5&&ele.style.getAttribute){
ak5=ele.style.getAttribute(attribute);
}
}
if (!ak5||ak5==""||typeof(ak5)==='undefined'){
if (typeof(defaultValue)!='undefined'){
ak5=defaultValue;
}
else {
ak5=null;
}
}
return ak5;
}
this.ParseBorderWidth=function (m2){
if (!ak3){
var ak7={};
var ak8=document.createElement('div');
ak8.style.visibility='hidden';
ak8.style.position='absolute';
ak8.style.fontSize='1px';
document.body.appendChild(ak8)
var ak9=document.createElement('div');
ak9.style.height='0px';
ak9.style.overflow='hidden';
ak8.appendChild(ak9);
var al0=ak8.offsetHeight;
ak9.style.borderTop='solid black';
ak9.style.borderTopWidth='thin';
ak7['thin']=ak8.offsetHeight-al0;
ak9.style.borderTopWidth='medium';
ak7['medium']=ak8.offsetHeight-al0;
ak9.style.borderTopWidth='thick';
ak7['thick']=ak8.offsetHeight-al0;
ak8.removeChild(ak9);
document.body.removeChild(ak8);
ak3=ak7;
}
if (m2){
switch (m2){
case 'thin':
case 'medium':
case 'thick':
return ak3[m2];
case 'inherit':
return 0;
}
var al1=this.ParseUnit(m2);
if (al1.type!='px')
throw new Error();
return al1.size;
}
return 0;
}
this.ParseUnit=function (m2){
if (!m2)
throw new Error();
m2=this.Trim(m2).toLowerCase();
var z8=m2.length;
var r1=-1;
for (var e9=0;e9<z8;e9++){
var x9=m2.substr(e9,1);
if ((x9<'0'||x9>'9')&&x9!='-'&&x9!='.'&&x9!=',')
break ;
r1=e9;
}
if (r1==-1)
throw new Error();
var ah4;
var al2;
if (r1<(z8-1))
ah4=this.Trim(m2.substring(r1+1));
else 
ah4='px';
al2=parseFloat(m2.substr(0,r1+1));
if (ah4=='px'){
al2=Math.floor(al2);
}
return {size:al2,type:ah4};
}
this.GetViewPortByRowCol=function (e4,i5,m4){
var al3=null;
var f1=null;
var al4=null;
var m2=this.GetViewport(e4);
var h2=this.GetCellByRowCol(e4,i5,m4);
if (m2!=null&&this.IsChild(h2,m2))
return m2;
else if (al4!=null&&this.IsChild(h2,al4))
return al4;
else if (f1!=null&&this.IsChild(h2,f1))
return f1;
else if (al3!=null&&this.IsChild(h2,al3))
return al3;
return ;
}
this.GetMsgPos=function (e4,i5,m4){
if (i5<0||m4<0){
return {left:0,top:0};
}
else {
var al3=null;
var f1=null;
var al4=null;
var m2=this.GetViewport(e4);
var al5=this.GetGroupBar(e4);
var k5=document.getElementById(e4.id+"_titleBar");
var h2=this.GetCellByRowCol(e4,i5,m4);
var f7=h2.offsetTop+h2.offsetHeight;
var z8=h2.offsetLeft;
if ((al3!=null||f1!=null)&&(this.IsChild(h2,al4)||this.IsChild(h2,m2))){
if (al3!=null)
f7+=al3.offsetHeight;
else 
f7+=f1.offsetHeight;
}
if ((al3!=null||al4!=null)&&(this.IsChild(h2,f1)||this.IsChild(h2,m2))){
if (al3!=null)
z8+=al3.offsetWidth;
else 
z8+=al4.offsetWidth;
}
if (m2!=null&&(al3||f1||al4)){
if (k5)f7+=k5.offsetHeight;
if (al5)f7+=al5.offsetHeight;
if (this.GetColHeader(e4))f7+=this.GetColHeader(e4).offsetHeight;
if (this.GetRowHeader(e4))z8+=this.GetRowHeader(e4).offsetWidth;
}
if (m2!=null&&this.IsChild(h2,m2)){
if (f1)
f7-=m2.parentNode.scrollTop;
if (al4)
z8-=m2.parentNode.scrollLeft;
}
if (al4!=null&&this.IsChild(h2,al4)){
f7-=al4.parentNode.scrollTop;
}
if (f1!=null&&this.IsChild(h2,f1)){
z8-=f1.parentNode.scrollLeft;
}
var j4=h2.clientHeight;
var i9=h2.clientWidth;
return {left:z8,top:f7,a4:j4,a5:i9};
}
}
this.SyncMsgs=function (e4){
if (!e4.msgList)return ;
for (e9 in e4.msgList){
if (e4.msgList[e9].constructor==Array){
for (h9 in e4.msgList[e9]){
if (e4.msgList[e9][h9]&&e4.msgList[e9][h9].msgBox&&e4.msgList[e9][h9].msgBox.IsVisible){
e4.msgList[e9][h9].msgBox.Show(e4,this);
}
}
}
}
}
this.GetCellInfo=function (e4,g9,h1,v2){
var f3=this.GetData(e4);
if (f3==null)return null;
var f4=f3.getElementsByTagName("root")[0];
if (f4==null)return null;
var m8=f4.getElementsByTagName("state")[0];
if (m8==null)return null;
var al6=m8.getElementsByTagName("cellinfo")[0];
if (al6==null)return null;
var f7=al6.firstChild;
while (f7!=null){
if ((f7.getAttribute("r")==""+g9)&&(f7.getAttribute("c")==""+h1)&&(f7.getAttribute("pos")==""+v2))return f7;
f7=f7.nextSibling;
}
return null;
}
this.AddCellInfo=function (e4,g9,h1,v2){
var m6=this.GetCellInfo(e4,g9,h1,parseInt(v2));
if (m6!=null)return m6;
var f3=this.GetData(e4);
var f4=f3.getElementsByTagName("root")[0];
if (f4==null)return null;
var m8=f4.getElementsByTagName("state")[0];
if (m8==null)return null;
var al6=m8.getElementsByTagName("cellinfo")[0];
if (al6==null)return null;
if (document.all!=null){
m6=f3.createNode("element","c","");
}else {
m6=document.createElement("c");
m6.style.display="none";
}
m6.setAttribute("r",g9);
m6.setAttribute("c",h1);
m6.setAttribute("pos",v2);
al6.appendChild(m6);
return m6;
}
this.setCellAttribute=function (e4,h2,attname,u0,noEvent,recalc){
if (h2==null)return ;
var g9=this.GetRowKeyFromCell(e4,h2);
var h1=this.GetColKeyFromCell(e4,h2);
if (typeof(g9)=="undefined")return ;
var v2=-1;
if (this.IsChild(h2,this.GetCorner(e4)))
v2=0;
else if (this.IsChild(h2,this.GetRowHeader(e4)))
v2=1;
else if (this.IsChild(h2,this.GetColHeader(e4)))
v2=2;
else if (this.IsChild(h2,this.GetViewport(e4)))
v2=3;
var q7=this.AddCellInfo(e4,g9,h1,v2);
q7.setAttribute(attname,u0);
if (!noEvent){
var g0=this.CreateEvent("DataChanged");
g0.cell=h2;
g0.cellValue=u0;
g0.row=g9;
g0.col=h1;
this.FireEvent(e4,g0);
}
var f6=this.GetCmdBtn(e4,"Update");
if (f6!=null&&f6.getAttribute("disabled")!=null)
this.UpdateCmdBtnState(f6,false);
f6=this.GetCmdBtn(e4,"Cancel");
if (f6!=null&&f6.getAttribute("disabled")!=null)
this.UpdateCmdBtnState(f6,false);
e4.e2=true;
if (recalc){
this.UpdateValues(e4);
}
}
this.updateCellLocked=function (h2,locked){
if (h2==null)return ;
var f7=h2.getAttribute("FpCellType")=="readonly";
if (f7==locked)return ;
var h1=h2.firstChild;
while (h1!=null){
if (typeof(h1.disabled)!="undefined")h1.disabled=locked;
h1=h1.nextSibling;
}
}
this.Cells=function (e4,g9,h1)
{
var al7=this.GetCellByRowCol(e4,g9,h1);
if (al7){
al7.GetValue=function (){
return the_fpSpread.GetValue(e4,g9,h1);
}
al7.SetValue=function (value){
if (typeof(value)=="undefined")return ;
if (this.parentNode.getAttribute("previewRow")!=null)return ;
the_fpSpread.SetValue(e4,g9,h1,value);
the_fpSpread.SaveClientEditedDataRealTime();
}
al7.GetBackColor=function (){
if (this.getAttribute("bgColorBak")!=null)
return this.getAttribute("bgColorBak");
return document.defaultView.getComputedStyle(this,"").getPropertyValue("background-color");
}
al7.SetBackColor=function (value){
if (typeof(value)=="undefined")return ;
this.bgColor=value;
this.setAttribute("bgColorBak",value);
this.style.backgroundColor=value;
the_fpSpread.setCellAttribute(e4,this,"bc",value);
the_fpSpread.SaveClientEditedDataRealTime();
}
al7.GetForeColor=function (){
return document.defaultView.getComputedStyle(this,"").getPropertyValue("color");
}
al7.SetForeColor=function (value){
if (typeof(value)=="undefined")return ;
this.style.color=value;
the_fpSpread.setCellAttribute(e4,this,"fc",value);
the_fpSpread.SaveClientEditedDataRealTime();
}
al7.GetTabStop=function (){
return this.getAttribute("TabStop")!="false";
}
al7.SetTabStop=function (value){
var al8=new String(value);
if (al8.toLocaleLowerCase()=="false"){
this.setAttribute("TabStop","false");
the_fpSpread.setCellAttribute(e4,this,"ts","false");
the_fpSpread.SaveClientEditedDataRealTime();
}else {
this.removeAttribute("TabStop");
}
}
al7.GetCellType=function (){
var al9=the_fpSpread.GetCellType2(this);
if (al9=="text"||al9=="readonly")
{
al9=this.getAttribute("CellType2");
}
if (al9==null)
al9="GeneralCellType";
return al9;
}
al7.GetHAlign=function (){
var am0=document.defaultView.getComputedStyle(this,"").getPropertyValue("text-Align");
if (am0==""||am0=="undefined"||am0==null)
am0=this.style.textAlign;
if (am0==""||am0=="undefined"||am0==null)
am0=this.getAttribute("align");
if (am0!=null&&am0.indexOf("-webkit")!=-1)am0=am0.replace("-webkit-","");
return am0;
}
al7.SetHAlign=function (value){
if (typeof(value)=="undefined")return ;
this.style.textAlign=typeof(value)=="string"?value:value.Name;
the_fpSpread.setCellAttribute(e4,this,"ha",typeof(value)=="string"?value:value.Name);
the_fpSpread.SaveClientEditedDataRealTime();
}
al7.GetVAlign=function (){
var am1=document.defaultView.getComputedStyle(this,"").getPropertyValue("vertical-Align");
if (am1==""||am1=="undefined"||am1==null)
am1=this.style.verticalAlign;
if (am1==""||am1=="undefined"||am1==null)
am1=this.getAttribute("valign");
return am1;
}
al7.SetVAlign=function (value){
if (typeof(value)=="undefined")return ;
this.style.verticalAlign=typeof(value)=="string"?value:value.Name;
the_fpSpread.setCellAttribute(e4,this,"va",typeof(value)=="string"?value:value.Name);
the_fpSpread.SaveClientEditedDataRealTime();
}
al7.GetLocked=function (){
if (al7.GetCellType()=="ButtonCellType"||al7.GetCellType()=="TagCloudCellType"||al7.GetCellType()=="HyperLinkCellType")
return al7.getAttribute("Locked")=="1";
return the_fpSpread.GetCellType(this)=="readonly";
}
al7.GetFont_Name=function (){
return document.defaultView.getComputedStyle(this,"").getPropertyValue("font-family");
}
al7.SetFont_Name=function (value){
if (typeof(value)=="undefined")return ;
this.style.fontFamily=value;
the_fpSpread.setCellAttribute(e4,this,"fn",value);
the_fpSpread.SaveClientEditedDataRealTime();
}
al7.GetFont_Size=function (){
return document.defaultView.getComputedStyle(this,"").getPropertyValue("font-size");
}
al7.SetFont_Size=function (value){
if (typeof(value)=="undefined")return ;
if (typeof(value)=="number")value+="px";
this.style.fontSize=value;
the_fpSpread.setCellAttribute(e4,this,"fs",value);
the_fpSpread.SizeSpread(e4);
the_fpSpread.SaveClientEditedDataRealTime();
}
al7.GetFont_Bold=function (){
return document.defaultView.getComputedStyle(this,"").getPropertyValue("font-weight")=="bold"?true:false;
}
al7.SetFont_Bold=function (value){
if (typeof(value)=="undefined")return ;
this.style.fontWeight=value==true?"bold":"normal";
the_fpSpread.setCellAttribute(e4,this,"fb",new String(value).toLocaleLowerCase());
the_fpSpread.SaveClientEditedDataRealTime();
}
al7.GetFont_Italic=function (){
return document.defaultView.getComputedStyle(this,"").getPropertyValue("font-style")=="italic"?true:false;
}
al7.SetFont_Italic=function (value){
if (typeof(value)=="undefined")return ;
this.style.fontStyle=value==true?"italic":"normal";
the_fpSpread.setCellAttribute(e4,this,"fi",new String(value).toLocaleLowerCase());
the_fpSpread.SaveClientEditedDataRealTime();
}
al7.GetFont_Overline=function (){
return document.defaultView.getComputedStyle(this,"").getPropertyValue("text-decoration").indexOf("overline")>=0?true:false;
}
al7.SetFont_Overline=function (value){
if (value){
var am2=new String("overline");
if (document.defaultView.getComputedStyle(this,"").getPropertyValue("text-decoration").indexOf("line-through")>=0)
am2+=" line-through"
if (document.defaultView.getComputedStyle(this,"").getPropertyValue("text-decoration").indexOf("underline")>=0)
am2+=" underline"
this.style.textDecoration=am2;
}
else {
var am2=new String("");
if (document.defaultView.getComputedStyle(this,"").getPropertyValue("text-decoration").indexOf("line-through")>=0)
am2+=" line-through"
if (document.defaultView.getComputedStyle(this,"").getPropertyValue("text-decoration").indexOf("underline")>=0)
am2+=" underline"
if (am2=="")am2="none";
this.style.textDecoration=am2;
}
the_fpSpread.setCellAttribute(e4,this,"fo",new String(value).toLocaleLowerCase());
the_fpSpread.SaveClientEditedDataRealTime();
}
al7.GetFont_Strikeout=function (){
return document.defaultView.getComputedStyle(this,"").getPropertyValue("text-decoration").indexOf("line-through")>=0?true:false;
}
al7.SetFont_Strikeout=function (value){
if (value){
var am2=new String("line-through");
if (document.defaultView.getComputedStyle(this,"").getPropertyValue("text-decoration").indexOf("overline")>=0)
am2+=" overline"
if (document.defaultView.getComputedStyle(this,"").getPropertyValue("text-decoration").indexOf("underline")>=0)
am2+=" underline"
this.style.textDecoration=am2;
}
else {
var am2=new String("");
if (document.defaultView.getComputedStyle(this,"").getPropertyValue("text-decoration").indexOf("overline")>=0)
am2+=" overline"
if (document.defaultView.getComputedStyle(this,"").getPropertyValue("text-decoration").indexOf("underline")>=0)
am2+=" underline"
if (am2=="")am2="none";
this.style.textDecoration=am2;
}
the_fpSpread.setCellAttribute(e4,this,"fk",new String(value).toLocaleLowerCase());
the_fpSpread.SaveClientEditedDataRealTime();
}
al7.GetFont_Underline=function (){
return document.defaultView.getComputedStyle(this,"").getPropertyValue("text-decoration").indexOf("underline")>=0?true:false;
}
al7.SetFont_Underline=function (value){
if (value){
var am2=new String("underline");
if (document.defaultView.getComputedStyle(this,"").getPropertyValue("text-decoration").indexOf("overline")>=0)
am2+=" overline"
if (document.defaultView.getComputedStyle(this,"").getPropertyValue("text-decoration").indexOf("line-through")>=0)
am2+=" line-through"
this.style.textDecoration=am2;
}
else {
var am2=new String("");
if (document.defaultView.getComputedStyle(this,"").getPropertyValue("text-decoration").indexOf("overline")>=0)
am2+=" overline"
if (document.defaultView.getComputedStyle(this,"").getPropertyValue("text-decoration").indexOf("line-through")>=0)
am2+=" line-through"
if (am2=="")am2="none";
this.style.textDecoration=am2;
}
the_fpSpread.setCellAttribute(e4,this,"fu",new String(value).toLocaleLowerCase());
the_fpSpread.SaveClientEditedDataRealTime();
}
return al7;
}
return null;
}
this.getDomRow=function (e4,g9){
var m7=this.GetRowCount(e4);
if (m7==0)return null;
var h2=this.GetCellByRowCol(e4,g9,0);
if (h2){
var e8=h2.parentNode.rowIndex;
if (e8>=0){
var i5=h2.parentNode.parentNode.rows[e8];
if (this.GetSizable(e4,i5))
return i5;
}
return null;
}
}
this.setRowInfo_RowAttribute=function (e4,g9,attname,u0,recalc){
g9=parseInt(g9);
if (g9<0)return ;
var am3=this.AddRowInfo(e4,g9);
am3.setAttribute(attname,u0);
var f6=this.GetCmdBtn(e4,"Update");
if (f6!=null&&f6.getAttribute("disabled")!=null)
this.UpdateCmdBtnState(f6,false);
f6=this.GetCmdBtn(e4,"Cancel");
if (f6!=null&&f6.getAttribute("disabled")!=null)
this.UpdateCmdBtnState(f6,false);
e4.e2=true;
if (recalc){
this.UpdateValues(e4);
}
}
this.Rows=function (e4,g9)
{
var am4=this.getDomRow(e4,g9);
if (am4){
am4.GetHeight=function (){
return the_fpSpread.GetRowHeightInternal(e4,g9);
}
am4.SetHeight=function (ac1){
if (typeof(ac1)=="undefined")return ;
if (ac1<1)
ac1=1;
g9=the_fpSpread.GetDisplayIndex(e4,g9);
var b5=null;
if (the_fpSpread.GetRowHeader(e4)!=null)b5=the_fpSpread.GetRowHeader(e4).rows[g9];
if (b5!=null)b5.cells[0].style.posHeight=ac1;
var i3=the_fpSpread.GetViewport(e4);
if (b5==null)
b5=i3.rows[g9];
if (b5!=null)b5.cells[0].style.posHeight=ac1;
var p4=the_fpSpread.AddRowInfo(e4,b5.getAttribute("FpKey"));
if (p4!=null){
if (typeof(b5.cells[0].style.posHeight)=="undefined")
the_fpSpread.SetRowHeight(e4,p4,ac1);
else 
the_fpSpread.SetRowHeight(e4,p4,b5.cells[0].style.posHeight);
}
var i4=the_fpSpread.GetParentSpread(e4);
if (i4!=null)i4.UpdateRowHeight(e4);
the_fpSpread.SynRowHeight(e4,the_fpSpread.GetRowHeader(e4),i3,g9,true,false)
var e7=the_fpSpread.GetTopSpread(e4);
the_fpSpread.SizeAll(e7);
the_fpSpread.Refresh(e7);
the_fpSpread.SaveClientEditedDataRealTime();
}
return am4;
}
return null;
}
this.setColInfo_ColumnAttribute=function (e4,h1,attname,u0,recalc){
h1=parseInt(h1);
if (h1<0)return ;
var am5=this.AddColInfo(e4,h1);
am5.setAttribute(attname,u0);
var f6=this.GetCmdBtn(e4,"Update");
if (f6!=null&&f6.getAttribute("disabled")!=null)
this.UpdateCmdBtnState(f6,false);
f6=this.GetCmdBtn(e4,"Cancel");
if (f6!=null&&f6.getAttribute("disabled")!=null)
this.UpdateCmdBtnState(f6,false);
e4.e2=true;
if (recalc){
this.UpdateValues(e4);
}
}
this.Columns=function (e4,h1)
{
var am6={a2:this.GetColByKey(e4,parseInt(h1))};
if (am6){
am6.GetWidth=function (){
return the_fpSpread.GetColWidthFromCol(e4,h1);
}
am6.SetWidth=function (value){
if (typeof(value)=="undefined")return ;
the_fpSpread.SetColWidth(e4,h1,value);
the_fpSpread.SaveClientEditedDataRealTime();
}
return am6;
}
return null;
}
this.GetTitleBar=function (e4){
try {
if (document.getElementById(e4.id+"_title")==null)return null;
var am7=document.getElementById(e4.id+"_titleBar");
if (am7!=null)am7=document.getElementById(e4.id+"_title");
return am7;
}
catch (ex){
return null;
}
}
this.CheckTitleInfo=function (e4){
var f3=this.GetData(e4);
if (f3==null)return null;
var f4=f3.getElementsByTagName("root")[0];
if (f4==null)return null;
var am8=f4.getElementsByTagName("titleinfo")[0];
if (am8==null)return null;
return am8;
}
this.AddTitleInfo=function (e4){
var m6=this.CheckTitleInfo(e4);
if (m6!=null)return m6;
var f3=this.GetData(e4);
var f4=f3.getElementsByTagName("root")[0];
if (f4==null)return null;
if (document.all!=null){
m6=f3.createNode("element","titleinfo","");
}else {
m6=document.createElement("titleinfo");
m6.style.display="none";
}
f4.appendChild(m6);
return m6;
}
this.setTitleInfo_Attribute=function (e4,attname,u0,recalc){
var am9=this.AddTitleInfo(e4);
am9.setAttribute(attname,u0);
var f6=this.GetCmdBtn(e4,"Update");
if (f6!=null&&f6.getAttribute("disabled")!=null)
this.UpdateCmdBtnState(f6,false);
f6=this.GetCmdBtn(e4,"Cancel");
if (f6!=null&&f6.getAttribute("disabled")!=null)
this.UpdateCmdBtnState(f6,false);
e4.e2=true;
if (recalc){
this.UpdateValues(e4);
}
}
this.GetTitleInfo=function (e4)
{
var an0=this.GetTitleBar(e4);
if (an0){
an0.GetHeight=function (){
return this.style.height;
}
an0.SetHeight=function (value){
this.style.height=parseInt(value)+"px";
the_fpSpread.setTitleInfo_Attribute(e4,"ht",value);
var e7=the_fpSpread.GetTopSpread(e4);
the_fpSpread.SizeAll(e7);
the_fpSpread.Refresh(e7);
the_fpSpread.SaveClientEditedDataRealTime();
}
an0.GetVisible=function (){
return (document.defaultView.getComputedStyle(this,"").getPropertyValue("display")=="none")?false:true;
return document.defaultView.getComputedStyle(this,"").getPropertyValue("visibility");
}
an0.SetVisible=function (value){
this.style.display=value?"":"none";
this.style.visibility=value?"visible":"hidden";
the_fpSpread.setTitleInfo_Attribute(e4,"vs",new String(value).toLocaleLowerCase());
var e7=the_fpSpread.GetTopSpread(e4);
the_fpSpread.SizeAll(e7);
the_fpSpread.Refresh(e7);
the_fpSpread.SaveClientEditedDataRealTime();
}
an0.GetValue=function (){
return this.textContent;
}
an0.SetValue=function (value){
this.textContent=""+value;
the_fpSpread.setTitleInfo_Attribute(e4,"tx",value);
the_fpSpread.SaveClientEditedDataRealTime();
}
return an0;
}
return null;
}
this.SaveClientEditedDataRealTime=function (){
var an1=this.GetPageActiveSpread();
if (an1!=null){
this.SaveData(an1);
an1.e2=false;
}
an1=this.GetPageActiveSheetView();
if (an1!=null){
this.SaveData(an1);
an1.e2=false;
}
}
var an2="";
this.ShowScrollingContent=function (e4,hs){
var q8="";
var o1=this.GetTopSpread(e4);
var an3=o1.getAttribute("scrollContentColumns");
var an4=o1.getAttribute("scrollContentMaxHeight");
var an5=o1.getAttribute("scrollContentTime");
var i3=this.GetViewport(o1);
var an6=this.GetColGroup(i3);
var m2=this.GetParent(i3);
var an7=0;
if (hs){
var an8=m2.scrollLeft;
var c6=this.GetColHeader(o1);
var r5=0;
for (;r5<an6.childNodes.length;r5++){
var h1=an6.childNodes[r5];
an7+=parseInt(h1.width);
if (an7>an8)break ;
}
var an9=null;
if (an9)r5+=this.GetColGroup(an9).childNodes.length;
if (c6){
var r4=c6.rows.length-1;
if (e4.getAttribute("LayoutMode")==null)
r4=parseInt(c6.getAttribute("ColTextIndex"))?c6.getAttribute("ColTextIndex"):c6.rows.length-1;
var ao0=this.GetHeaderCellFromRowCol(o1,r4,r5,true);
if (ao0){
if (ao0.getAttribute("FpCellType")=="ExtenderCellType"&&ao0.getElementsByTagName("DIV").length>0){
var w1=this.GetEditor(ao0);
var w2=this.GetFunction("ExtenderCellType_getEditorValue");
if (w1!==null&&w2!==null){
q8="&nbsp;Column:&nbsp;"+w2(w1)+"&nbsp;";
}
}
else 
q8="&nbsp;Column:&nbsp;"+ao0.innerHTML+"&nbsp;";
}
}
if (q8.length<=0)q8="&nbsp;Column:&nbsp;"+(r5+1)+"&nbsp;";
}
else {
var m8=m2.scrollTop;
var c5=this.GetRowHeader(o1);
var r4=0;
var ao1=0;
for (var x2=0;x2<i3.rows.length;x2++){
var g9=i3.rows[x2];
an7+=g9.offsetHeight;
if (an7>m8){
if (g9.getAttribute("fpkey")==null&&g9.getAttribute("previewrow")!="true")
r4--;
else 
ao1=g9.offsetHeight;
break ;
}
if (g9.getAttribute("fpkey")!=null||g9.getAttribute("previewrow")=="true"){
r4++;
ao1=g9.offsetHeight;
}
}
var an9=null;
if (an9)r4+=an9.rows.length;
if (e4.getAttribute("LayoutMode")==null&&an3!=null&&an3.length>0){
ao1=ao1>an4?an4:ao1;
var ao2=an3.split(",");
var ao3=false;
for (var e9=0;e9<ao2.length;e9++){
var h1=parseInt(ao2[e9]);
if (h1==null||h1>=this.GetColCount(e4))continue ;
var h2=o1.GetCellByRowCol(r4,h1);
if (!h2||h2.getAttribute("col")!=null&&h2.getAttribute("col")!=h1)continue ;
var ao4=(h2.getAttribute("group")==1);
var ab1=(h2.parentNode.getAttribute("previewrow")=="true");
var g0=(h2.getAttribute("RowEditTemplate")!=null);
var j5=this.IsXHTML(e4);
if (!j5&&an2==""){
this.GetScrollingContentStyle(e4);
if (ai3!=null){
if (ai3.fontFamily!=null&&ai3.fontFamily!="")an2+="fontFamily:"+ai3.fontFamily+";";
if (ai3.fontSize!=null&&ai3.fontSize!="")an2+="fontSize:"+ai3.fontSize+";";
if (ai3.fontStyle!=null&&ai3.fontStyle!="")an2+="fontStyle:"+ai3.fontStyle+";";
if (ai3.fontVariant!=null&&ai3.fontVariant!="")an2+="fontVariant:"+ai3.fontVariant+";";
if (ai3.fontWeight!=null&&ai3.fontWeight!="")an2+="fontWeight:"+ai3.fontWeight+";";
if (ai3.backgroundColor!=null&&ai3.backgroundColor!="")an2+="backgroundColor:"+ai3.backgroundColor+";";
if (ai3.color!=null&&ai3.color!="")an2+="color:"+ai3.color;
}
}
if (!ao3){
q8+="<table cellPadding='0' cellSpacing='0' style='height:"+ao1+"px;"+(ao4?"":"table-layout:fixed;")+an2+"'><tr>";
}
q8+="<td style='width:"+(ao4?0:h2.offsetWidth)+"px;'>";
if (ao4)
q8+="&nbsp;<i>GroupBar:</i>&nbsp;"+h2.textContent+"&nbsp;";
else if (ab1)
q8+="&nbsp;<i>PreviewRow:</i>&nbsp;"+h2.textContent+"&nbsp;";
else if (g0){
var ao5=this.parseCell(e4,h2);
q8+="&nbsp;<i>RowEditTemplate:</i>&nbsp;"+ao5+"&nbsp;";
}
else {
if (h2.getAttribute("fpcelltype"))this.UpdateCellTypeDOM(h2);
if (h2.getAttribute("fpcelltype")=="MultiColumnComboBoxCellType"&&h2.childNodes[0]&&h2.childNodes[0].childNodes.length>0&&h2.childNodes[0].getAttribute("MccbId"))
q8+=o1.GetValue(r4,h1);
else if (h2.getAttribute("fpcelltype")=="RadioButtonListCellType"||h2.getAttribute("fpcelltype")=="ExtenderCellType"||h2.getAttribute("fpeditorid")!=null){
var ao6=this.parseCell(e4,h2);
q8+=ao6;
}
else 
q8+=h2.innerHTML;
}
q8+="</td>";
ao3=true;
if (ao4||ab1||g0)break ;
}
if (ao3)
q8+="</tr></table>";
}
if (q8.length<=0&&c5){
var r5=this.GetColGroup(c5).childNodes.length-1;
if (e4.getAttribute("LayoutMode")==null)
r5=c5.getAttribute("RowTextIndex")?parseInt(c5.getAttribute("RowTextIndex"))+1:this.GetColGroup(c5).childNodes.length-1;
var ao0=this.GetHeaderCellFromRowCol(e4,r4,r5,false);
if (ao0)q8="&nbsp;Row:&nbsp;"+ao0.textContent+"&nbsp;";
}
if (q8.length<=0)q8="&nbsp;Row:&nbsp;"+(r4+1)+"&nbsp;";
}
this.ShowMessageInner(o1,q8,(hs?-1:-2),(hs?-2:-1),an5);
}
this.parseCell=function (e4,h2){
var q8=h2.innerHTML;
var o1=this.GetTopSpread(e4);
var ao7=o1.id;
if (q8.length>0){
q8=q8.replace(new RegExp("=\""+ao7,"g"),"=\""+ao7+"src");
q8=q8.replace(new RegExp("name="+ao7,"g"),"name="+ao7+"src");
}
return q8;
}
this.UpdateCellTypeDOM=function (h2){
for (var e9=0;e9<h2.childNodes.length;e9++){
if (h2.childNodes[e9].tagName&&(h2.childNodes[e9].tagName=="INPUT"||h2.childNodes[e9].tagName=="SELECT"))
this.UpdateDOM(h2.childNodes[e9]);
if (h2.childNodes[e9].childNodes&&h2.childNodes[e9].childNodes.length>0)
this.UpdateCellTypeDOM(h2.childNodes[e9]);
}
}
this.UpdateDOM=function (inputField){
if (typeof(inputField)=="string"){
inputField=document.getElementById(inputField);
}
if (inputField.type=="select-one"){
for (var e9=0;e9<inputField.options.length;e9++){
if (e9==inputField.selectedIndex){
inputField.options[inputField.selectedIndex].setAttribute("selected","selected");
}
}
}
else if (inputField.type=="text"){
inputField.setAttribute("value",inputField.value);
}
else if (inputField.type=="textarea"){
inputField.setAttribute("value",inputField.value);
}
else if ((inputField.type=="checkbox")||(inputField.type=="radio")){
if (inputField.checked){
inputField.setAttribute("checked","checked");
}else {
inputField.removeAttribute("checked");
}
}
}
this.GetScrollingContentStyle=function (e4){
if (ai3!=null)return ;
var e8=document.styleSheets.length;
for (var e9=0;e9<e8;e9++){
var ao8=document.styleSheets[e9];
for (var h9=0;h9<ao8.cssRules.length;h9++){
var ao9=ao8.cssRules[h9];
if (ao9.selectorText=="."+e4.id+"scrollContentStyle"||ao9.selectorText=="."+e4.id.toLowerCase()+"scrollcontentstyle"){
ai3=ao9.style;
break ;
}
}
if (ai3!=null)break ;
}
}
}
function CheckBoxCellType_setFocus(h2){
var i2=h2.getElementsByTagName("INPUT");
if (i2!=null&&i2.length>0&&i2[0].type=="checkbox"){
i2[0].focus();
}
}
function CheckBoxCellType_getCheckBoxEditor(h2){
var i2=h2.getElementsByTagName("INPUT");
if (i2!=null&&i2.length>0&&i2[0].type=="checkbox"){
return i2[0];
}
return null;
}
function CheckBoxCellType_isValid(h2,u0){
if (u0==null)return "";
u0=the_fpSpread.Trim(u0);
if (u0=="")return "";
if (u0.toLowerCase()=="true"||u0.toLowerCase()=="false")
return "";
else 
return "invalid value";
}
function CheckBoxCellType_getValue(u4,e4){
return CheckBoxCellType_getEditorValue(u4,e4);
}
function CheckBoxCellType_getEditorValue(u4,e4){
var h2=the_fpSpread.GetCell(u4);
var i2=CheckBoxCellType_getCheckBoxEditor(h2);
if (i2!=null&&i2.checked){
return "true";
}
return "false";
}
function CheckBoxCellType_setValue(u4,u0){
var h2=the_fpSpread.GetCell(u4);
var i2=CheckBoxCellType_getCheckBoxEditor(h2);
if (i2!=null){
i2.checked=(u0!=null&&u0.toLowerCase()=="true");
return ;
}
}
function IntegerCellType_getValue(u4){
var f7=u4;
while (f7.firstChild!=null&&f7.firstChild.nodeName!="#text")f7=f7.firstChild;
if (f7.innerHTML=="&nbsp;")return "";
var s4=f7.innerHTML;
u4=the_fpSpread.GetCell(u4);
if (u4.getAttribute("FpRef")!=null)u4=document.getElementById(u4.getAttribute("FpRef"));
var ap0=u4.getAttribute("groupchar");
if (ap0==null)ap0=",";
var t1=s4.length;
while (true){
s4=s4.replace(ap0,"");
if (s4.length==t1)break ;
t1=s4.length;
}
if (s4.charAt(0)=='('&&s4.charAt(s4.length-1)==')'){
var ap1=u4.getAttribute("negsign");
if (ap1==null)ap1="-";
s4=ap1+s4.substring(1,s4.length-1);
}
s4=the_fpSpread.ReplaceAll(s4,"&nbsp;"," ");
return s4;
}
function IntegerCellType_isValid(h2,u0){
if (u0==null||u0.length==0)return "";
u0=u0.replace(" ","");
if (u0.length==0)return "";
var an7=h2;
var ap2=h2.getAttribute("FpRef");
if (ap2!=null)an7=document.getElementById(ap2);
var ap1=an7.getAttribute("negsign");
var v2=an7.getAttribute("possign");
if (ap1!=null)u0=u0.replace(ap1,"-");
if (v2!=null)u0=u0.replace(v2,"+");
if (u0.charAt(u0.length-1)=="-")u0="-"+u0.substring(0,u0.length-1);
var t3=new RegExp("^\\s*[-\\+]?\\d+\\s*$");
var n7=(u0.match(t3)!=null);
if (n7)n7=!isNaN(u0);
if (n7){
var t7=an7.getAttribute("MinimumValue");
var i7=an7.getAttribute("MaximumValue");
var t6=parseInt(u0);
if (t7!=null){
t7=parseInt(t7);
n7=(!isNaN(t7)&&t6>=t7);
}
if (n7&&i7!=null){
i7=parseInt(i7);
n7=(!isNaN(i7)&&t6<=i7);
}
}
if (!n7){
if (an7.getAttribute("error")!=null)
return an7.getAttribute("error");
else 
return "Integer";
}
return "";
}
function DoubleCellType_isValid(h2,u0){
if (u0==null||u0.length==0)return "";
var an7=h2;
if (h2.getAttribute("FpRef")!=null)an7=document.getElementById(h2.getAttribute("FpRef"));
var ap3=an7.getAttribute("decimalchar");
if (ap3==null)ap3=".";
var ap0=an7.getAttribute("groupchar");
if (ap0==null)ap0=",";
u0=the_fpSpread.Trim(u0);
var n7=true;
n7=(u0.length==0||u0.charAt(0)!=ap0);
if (n7){
var t1=u0.length;
while (true){
u0=u0.replace(ap0,"");
if (u0.length==t1)break ;
t1=u0.length;
}
}
var n7=true;
if (u0.length==0){
n7=false;
}else if (n7){
var ap1=an7.getAttribute("negsign");
var v2=an7.getAttribute("possign");
var t7=an7.getAttribute("MinimumValue");
var i7=an7.getAttribute("MaximumValue");
n7=the_fpSpread.IsDouble(u0,ap3,ap1,v2,t7,i7);
}
if (!n7){
if (an7.getAttribute("error")!=null)
return an7.getAttribute("error");
else 
return "Double";
}
return "";
}
function DoubleCellType_getValue(u4){
var f7=u4;
while (f7.firstChild!=null&&f7.firstChild.nodeName!="#text")f7=f7.firstChild;
if (f7.innerHTML=="&nbsp;")return "";
var s4=f7.innerHTML;
u4=the_fpSpread.GetCell(u4);
if (u4.getAttribute("FpRef")!=null)u4=document.getElementById(u4.getAttribute("FpRef"));
var ap0=u4.getAttribute("groupchar");
if (ap0==null)ap0=",";
var t1=s4.length;
while (true){
s4=s4.replace(ap0,"");
if (s4.length==t1)break ;
t1=s4.length;
}
if (s4.charAt(0)=='('&&s4.charAt(s4.length-1)==')'){
var ap1=u4.getAttribute("negsign");
if (ap1==null)ap1="-";
s4=ap1+s4.substring(1,s4.length-1);
}
s4=the_fpSpread.ReplaceAll(s4,"&nbsp;"," ");
return s4;
}
function CurrencyCellType_isValid(h2,u0){
if (u0!=null&&u0.length>0){
var an7=h2;
if (h2.getAttribute("FpRef")!=null)an7=document.getElementById(h2.getAttribute("FpRef"));
var t0=an7.getAttribute("currencychar");
if (t0==null)t0="$";
u0=u0.replace(t0,"");
var ap0=an7.getAttribute("groupchar");
if (ap0==null)ap0=",";
u0=the_fpSpread.Trim(u0);
var n7=true;
n7=(u0.length==0||u0.charAt(0)!=ap0);
if (n7){
var t1=u0.length;
while (true){
u0=u0.replace(ap0,"");
if (u0.length==t1)break ;
t1=u0.length;
}
}
if (u0.length==0){
n7=false;
}else if (n7){
var ap3=an7.getAttribute("decimalchar");
if (ap3==null)ap3=".";
var ap1=an7.getAttribute("negsign");
var v2=an7.getAttribute("possign");
var t7=an7.getAttribute("MinimumValue");
var i7=an7.getAttribute("MaximumValue");
n7=the_fpSpread.IsDouble(u0,ap3,ap1,v2,t7,i7);
}
if (!n7){
if (an7.getAttribute("error")!=null)
return an7.getAttribute("error");
else 
return "Currency ("+t0+"100"+ap3+"10) ";
}
}
return "";
}
function CurrencyCellType_getValue(u4){
var f7=u4;
while (f7.firstChild!=null&&f7.firstChild.nodeName!="#text")f7=f7.firstChild;
if (f7.innerHTML=="&nbsp;")return "";
var s4=f7.innerHTML;
u4=the_fpSpread.GetCell(u4);
if (u4.getAttribute("FpRef")!=null)u4=document.getElementById(u4.getAttribute("FpRef"));
var t0=u4.getAttribute("currencychar");
if (t0!=null){
var ap4=document.createElement("SPAN");
ap4.innerHTML=t0;
t0=ap4.innerHTML;
}
if (t0==null)t0="$";
var ap0=u4.getAttribute("groupchar");
if (ap0==null)ap0=",";
s4=s4.replace(t0,"");
var t1=s4.length;
while (true){
s4=s4.replace(ap0,"");
if (s4.length==t1)break ;
t1=s4.length;
}
var ap1=u4.getAttribute("negsign");
if (ap1==null)ap1="-";
if (s4.charAt(0)=='('&&s4.charAt(s4.length-1)==')'){
s4=ap1+s4.substring(1,s4.length-1);
}
s4=the_fpSpread.ReplaceAll(s4,"&nbsp;"," ");
return s4;
}
function RegExpCellType_isValid(h2,u0){
if (u0==null||u0=="")
return "";
var an7=h2;
if (h2.getAttribute("FpRef")!=null)an7=document.getElementById(h2.getAttribute("FpRef"));
var ap5=new RegExp(an7.getAttribute("fpexpression"));
var t4=u0.match(ap5);
var m2=(t4!=null&&t4.length>0&&u0==t4[0]);
if (!m2){
if (an7.getAttribute("error")!=null)
return an7.getAttribute("error");
else 
return "invalid";
}
return "";
}
function PercentCellType_getValue(u4){
var f7=u4;
while (f7.firstChild!=null&&f7.firstChild.nodeName!="#text")f7=f7.firstChild;
if (f7.innerHTML=="&nbsp;")return "";
f7=f7.innerHTML;
var h2=the_fpSpread.GetCell(u4);
var an7=h2;
if (h2.getAttribute("FpRef")!=null)an7=document.getElementById(h2.getAttribute("FpRef"));
var ap6=an7.getAttribute("percentchar");
if (ap6==null)ap6="%";
f7=f7.replace(ap6,"");
var ap0=an7.getAttribute("groupchar");
if (ap0==null)ap0=",";
var t1=f7.length;
while (true){
f7=f7.replace(ap0,"");
if (f7.length==t1)break ;
t1=f7.length;
}
var ap1=an7.getAttribute("negsign");
var v2=an7.getAttribute("possign");
f7=the_fpSpread.ReplaceAll(f7,"&nbsp;"," ");
var g2=f7;
if (ap1!=null)
f7=f7.replace(ap1,"-");
if (v2!=null)
f7=f7.replace(v2,"+");
var ap3=an7.getAttribute("decimalchar");
if (ap3!=null)
f7=f7.replace(ap3,".");
if (!isNaN(f7))
return g2;
else 
return u4.innerHTML;
}
function PercentCellType_setValue(u4,u0){
var f7=u4;
while (f7.firstChild!=null&&f7.firstChild.nodeName!="#text")f7=f7.firstChild;
u4=f7;
if (u0!=null&&u0!=""){
var an7=the_fpSpread.GetCell(u4);
if (an7.getAttribute("FpRef")!=null)an7=document.getElementById(an7.getAttribute("FpRef"));
var ap6=an7.getAttribute("percentchar");
if (ap6==null)ap6="%";
u0=u0.replace(" ","");
u0=u0.replace(ap6,"");
u4.innerHTML=u0+ap6;
}else {
u4.innerHTML="";
}
}
function PercentCellType_isValid(h2,u0){
if (u0!=null){
var an7=the_fpSpread.GetCell(h2);
if (an7.getAttribute("FpRef")!=null)an7=document.getElementById(an7.getAttribute("FpRef"));
var ap6=an7.getAttribute("percentchar");
if (ap6==null)ap6="%";
u0=u0.replace(ap6,"");
var ap0=an7.getAttribute("groupchar");
if (ap0==null)ap0=",";
var t1=u0.length;
while (true){
u0=u0.replace(ap0,"");
if (u0.length==t1)break ;
t1=u0.length;
}
var ap7=u0;
var ap1=an7.getAttribute("negsign");
var v2=an7.getAttribute("possign");
if (ap1!=null)u0=u0.replace(ap1,"-");
if (v2!=null)u0=u0.replace(v2,"+");
var ap3=an7.getAttribute("decimalchar");
if (ap3!=null)
u0=u0.replace(ap3,".");
var n7=!isNaN(u0);
if (n7){
var ap8=an7.getAttribute("MinimumValue");
var ap9=an7.getAttribute("MaximumValue");
if (ap8!=null||ap9!=null){
var t7=parseFloat(ap8);
var i7=parseFloat(ap9);
n7=!isNaN(t7)&&!isNaN(i7);
if (n7){
if (ap3==null)ap3=".";
n7=the_fpSpread.IsDouble(ap7,ap3,ap1,v2,t7*100,i7*100);
}
}
}
if (!n7){
if (an7.getAttribute("error")!=null)
return an7.getAttribute("error");
else 
return "Percent:(ex,10"+ap6+")";
}
}
return "";
}
function ListBoxCellType_getValue(u4){
var f7=u4.getElementsByTagName("TABLE");
if (f7.length>0)
{
var g7=f7[0].rows;
for (var h9=0;h9<g7.length;h9++){
var h2=g7[h9].cells[0];
if (h2.selected=="true")
{
var aq0=h2;
while (aq0.firstChild!=null)aq0=aq0.firstChild;
var an7=aq0.nodeValue;
return an7;
}
}
}
return "";
}
function ListBoxCellType_setValue(u4,u0){
var f7=u4.getElementsByTagName("TABLE");
if (f7.length>0)
{
f7[0].style.width=(u4.clientWidth-6)+"px";
var g7=f7[0].rows;
for (var h9=0;h9<g7.length;h9++){
var h2=g7[h9].cells[0];
var aq0=h2;
while (aq0.firstChild!=null)aq0=aq0.firstChild;
var an7=aq0.nodeValue;
if (an7==u0){
h2.selected="true";
if (f7[0].parentNode.getAttribute("selectedBackColor")!="undefined")
h2.style.backgroundColor=f7[0].parentNode.getAttribute("selectedBackColor");
if (f7[0].parentNode.getAttribute("selectedForeColor")!="undefined")
h2.style.color=f7[0].parentNode.getAttribute("selectedForeColor");
}else {
h2.style.backgroundColor="";
h2.style.color="";
h2.selected="";
h2.bgColor="";
}
}
}
}
function TextCellType_getValue(u4){
var h2=the_fpSpread.GetCell(u4,true);
if (h2!=null&&h2.getAttribute("password")!=null){
if (h2!=null&&h2.getAttribute("value")!=null)
return h2.getAttribute("value");
else 
return "";
}else {
var f7=u4;
while (f7.firstChild!=null&&f7.firstChild.nodeName!="#text")f7=f7.firstChild;
if (f7.innerHTML=="&nbsp;")return "";
var f7=the_fpSpread.ReplaceAll(f7.innerHTML,"&nbsp;"," ");
var f7=the_fpSpread.ReplaceAll(f7,"<br>","\n");
return f7;
}
}
function TextCellType_setValue(u4,u0){
var h2=the_fpSpread.GetCell(u4,true);
if (h2==null)return ;
var f7=u4;
while (f7.firstChild!=null&&f7.firstChild.nodeName!="#text")f7=f7.firstChild;
u4=f7;
if (h2.getAttribute("password")!=null){
if (u0!=null&&u0!=""){
u0=u0.replace(" ","");
u4.innerHTML="";
for (var e9=0;e9<u0.length;e9++)
u4.innerHTML+="*";
h2.setAttribute("value",u0);
}else {
u4.innerHTML="";
h2.setAttribute("value","");
}
}else {
u0=the_fpSpread.ReplaceAll(u0,"\n","<br>");
u4.innerHTML=the_fpSpread.ReplaceAll(u0," ","&nbsp;");
}
}
function RadioButtonListCellType_getValue(u4){
var h2=the_fpSpread.GetCell(u4,true);
if (h2==null)return ;
var aq1=h2.getElementsByTagName("INPUT");
for (var e9=0;e9<aq1.length;e9++){
if (aq1[e9].tagName=="INPUT"&&aq1[e9].checked){
return aq1[e9].value;
}
}
return "";
}
function RadioButtonListCellType_getEditorValue(u4){
return RadioButtonListCellType_getValue(u4);
}
function RadioButtonListCellType_setValue(u4,u0){
var h2=the_fpSpread.GetCell(u4,true);
if (h2==null)return ;
if (u0!=null)u0=the_fpSpread.Trim(u0);
var aq1=h2.getElementsByTagName("INPUT");
for (var e9=0;e9<aq1.length;e9++){
if (aq1[e9].tagName=="INPUT"&&u0==the_fpSpread.Trim(aq1[e9].value)){
aq1[e9].checked=true;
break ;
}else {
if (aq1[e9].checked)aq1[e9].checked=false;
}
}
}
function RadioButtonListCellType_setFocus(u4){
var h2=the_fpSpread.GetCell(u4,true);
if (h2==null)return ;
var i2=h2.getElementsByTagName("INPUT");
if (i2==null)return ;
for (var e9=0;e9<i2.length;e9++){
if (i2[e9].type=="radio"&&i2[e9].checked){
i2[e9].focus();
return ;
}
}
}
function MultiColumnComboBoxCellType_setValue(u4,u0,e4){
var h2=the_fpSpread.GetCell(u4,true);
if (h2==null)return ;
var aq2=h2.getElementsByTagName("DIV");
if (aq2!=null&&aq2.length>0){
var aq3=h2.getElementsByTagName("input");
if (aq3!=null&&aq3.length>0)
aq3[0].value=u0;
return ;
}
if (u0!=null&&u0!="")
u4.textContent=u0;
else 
u4.innerHTML="&nbsp;";
}
function MultiColumnComboBoxCellType_getValue(u4,e4){
var s4=u4.textContent;
var i8=the_fpSpread.GetCell(u4,true);
var aq2=i8.getElementsByTagName("DIV");
if (aq2!=null&&aq2.length>0){
var aq3=i8.getElementsByTagName("input");
if (aq3!=null&&aq3.length>0)
return aq3[0].value;
return ;
}
if (!e4)return null;
var s5=the_fpSpread.GetCellEditorID(e4,i8);
var a8=null;
if (s5!=null&&typeof(s5)!="undefined"){
a8=the_fpSpread.GetCellEditor(e4,s5,true);
if (a8!=null){
var aq4=a8.getAttribute("MccbId");
if (aq4){
FarPoint.System.WebControl.MultiColumnComboBoxCellType.CheckInit(aq4);
var aq5=eval(aq4+"_Obj");
if (aq5!=null&&aq5.SetText!=null){
aq5.SetText(s4);
return s4;
}
}
}
return null;
}
return s4;
}
function MultiColumnComboBoxCellType_getEditorValue(u4,e4){
var h2=the_fpSpread.GetCell(u4,true);
if (h2==null)return ;
var aq6=h2.getElementsByTagName("INPUT");
if (aq6!=null&&aq6.length>0){
var f7=aq6[0];
return f7.value;
}
return null;
}
function MultiColumnComboBoxCellType_setFocus(u4){
var h2=the_fpSpread.GetCell(u4);
var e4=the_fpSpread.GetSpread(h2);
if (h2==null)return ;
var aq7=h2.getElementsByTagName("DIV");
if (aq7!=null&&aq7.length>0){
var aq4=aq7[0].getAttribute("MccbId");
if (aq4){
var aq5=eval(aq4+"_Obj");
if (aq5!=null&&typeof(aq5.FocusForEdit)!="undefined"){
aq5.FocusForEdit();
}
}
}
}
function TagCloudCellType_getValue(u4,e4){
var s4=u4.textContent;
if (typeof(s4)!="undefined"&&s4!=null&&s4.length>0)
{
s4=the_fpSpread.ReplaceAll(s4,"<br>","");
s4=the_fpSpread.ReplaceAll(s4,"\n","");
s4=the_fpSpread.ReplaceAll(s4,"\t","");
var q9=new RegExp("\xA0","g");
s4=s4.replace(q9,String.fromCharCode(32));
s4=the_fpSpread.HTMLDecode(s4);
}
else 
s4="";
return s4;
}
