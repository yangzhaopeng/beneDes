//
//	Copyright?2005. FarPoint Technologies.	All rights reserved.
//
var the_fpSpread = new Fpoint_FPSpread();
function FpSpread_EventHandlers(){
var e3=the_fpSpread;
this.TranslateKeyPress=function (event){
e3.TranslateKeyPress(event);
}
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
var e5=window.navigator.userAgent;
var e6=(e5.indexOf("Firefox/3.")>=0);
if (e6)
e3.AttachEvent(document,"keypress",this.TranslateKeyPress,true);
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
var e7=e3.GetViewport(e4);
if (e7!=null){
e3.AttachEvent(e3.GetViewport(e4).parentNode,"DOMAttrModified",this.DoPropertyChange,true);
e3.AttachEvent(e3.GetViewport(e4).parentNode,"scroll",this.ScrollViewport);
}
var e8=e3.GetCommandBar(e4);
if (e8!=null){
e3.AttachEvent(e8,"mouseover",this.CmdbarMouseOver,false);
e3.AttachEvent(e8,"mouseout",this.CmdbarMouseOut,false);
}
}
this.DetachEvents=function (e4){
e3.DetachEvent(e4,"mousedown",this.MouseDown,false);
e3.DetachEvent(e4,"mouseup",this.MouseUp,false);
e3.DetachEvent(document,"mouseup",this.MouseUp,false);
e3.DetachEvent(e4,"mousemove",this.MouseMove,false);
e3.DetachEvent(e4,"dblclick",this.DblClick,false);
e3.DetachEvent(e4,"focus",this.Focus,false);
var e7=e3.GetViewport(e4);
if (e7!=null){
e3.DetachEvent(e3.GetViewport(e4).parentNode,"DOMAttrModified",this.DoPropertyChange,true);
e3.DetachEvent(e3.GetViewport(e4).parentNode,"scroll",this.ScrollViewport);
}
var e8=e3.GetCommandBar(e4);
if (e8!=null){
e3.DetachEvent(e8,"mouseover",this.CmdbarMouseOver,false);
e3.DetachEvent(e8,"mouseout",this.CmdbarMouseOut,false);
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
this.activePager=null;
this.dragSlideBar=false;
e4.allowColMove=(e4.getAttribute("colMove")=="true");
e4.allowGroup=(e4.getAttribute("allowGroup")=="true");
e4.selectedCols=[];
e4.msgList=new Array();
e4.mouseY=null;
e4.copymulticol=false;
}
this.RegisterSpread=function (e4){
var e9=this.GetTopSpread(e4);
if (e9!=e4)return ;
if (this.spreads==null){
this.spreads=new Array();
}
var f0=this.spreads.length;
for (var f1=0;f1<f0;f1++){
if (this.spreads[f1]==e4)return ;
}
this.spreads.length=f0+1;
this.spreads[f0]=e4;
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
e4.c1=document.createElement('XML');
e4.c1.id=e4.id+"_XMLDATA";
e4.c1.style.display="none";
document.body.insertBefore(e4.c1,null);
}
var f2=document.getElementById(e4.id+"_data");
if (f2!=null&&f2.getAttribute("data")!=null){
e4.c1.innerHTML=f2.getAttribute("data");
f2.value="";
}
this.SaveData(e4);
e4.c2=document.getElementById(e4.id+"_viewport");
if (e4.c2!=null){
e4.c3=e4.c2.parentNode;
}
e4.frozColHeader=document.getElementById(e4.id+"_frozColHeader");
e4.frozRowHeader=document.getElementById(e4.id+"_frozRowHeader");
e4.viewport0=document.getElementById(e4.id+"_viewport0");
e4.viewport1=document.getElementById(e4.id+"_viewport1");
e4.viewport2=document.getElementById(e4.id+"_viewport2");
e4.c4=document.getElementById(e4.id+"_corner");
if (e4.c4!=null&&e4.c4.childNodes.length>0){
e4.c4=e4.c4.getElementsByTagName("TABLE")[0];
}
e4.frzRows=e4.frzCols=0;
if (e4.viewport1!=null){
e4.frzRows=e4.viewport1.rows.length;
}
if (e4.viewport0!=null){
var f3=this.GetColGroup(e4.viewport0);
if (f3!=null)e4.frzCols=f3.childNodes.length;
}else if (e4.viewport2!=null){
var f3=this.GetColGroup(e4.viewport2);
if (f3!=null)e4.frzCols=f3.childNodes.length;
}
e4.c5=document.getElementById(e4.id+"_rowHeader");
if (e4.c5!=null)e4.c5=e4.c5.getElementsByTagName("TABLE")[0];
e4.c6=document.getElementById(e4.id+"_colHeader");
if (e4.c6!=null)e4.c6=e4.c6.getElementsByTagName("TABLE")[0];
e4.frozColFooter=document.getElementById(e4.id+"_frozColFooter");
e4.colFooter=document.getElementById(e4.id+"_colFooter");
if (e4.colFooter!=null)e4.colFooter=e4.colFooter.getElementsByTagName("TABLE")[0];
e4.footerCorner=document.getElementById(e4.id+"_footerCorner");
if (e4.footerCorner!=null&&e4.footerCorner.childNodes.length>0){
e4.footerCorner=e4.footerCorner.getElementsByTagName("TABLE")[0];
}
if (e4.frozColFooter!=null)e4.frozColFooter=e4.frozColFooter.getElementsByTagName("TABLE")[0];
var c7=e4.c7=document.getElementById(e4.id+"_commandBar");
if (e4.frozRowHeader!=null)e4.frozRowHeader=e4.frozRowHeader.getElementsByTagName("TABLE")[0];
if (e4.frozColHeader!=null)e4.frozColHeader=e4.frozColHeader.getElementsByTagName("TABLE")[0];
var f4=this.GetViewport(e4);
if (f4!=null){
e4.setAttribute("rowCount",f4.rows.length);
if (f4.rows.length==1)e4.setAttribute("rowCount",0);
e4.setAttribute("colCount",f4.getAttribute("cols"));
}
var d9=e4.d9;
var e1=e4.e1;
var e0=e4.e0;
this.InitSpan(e4,this.GetViewport0(e4),d9);
this.InitSpan(e4,this.GetViewport1(e4),d9);
this.InitSpan(e4,this.GetViewport2(e4),d9);
this.InitSpan(e4,this.GetViewport(e4),d9);
this.InitSpan(e4,this.GetColHeader(e4),e1);
this.InitSpan(e4,this.GetFrozColHeader(e4),e1);
this.InitSpan(e4,this.GetRowHeader(e4),e0);
this.InitSpan(e4,this.GetFrozRowHeader(e4),e0);
if (e4.frzRows!=0||e4.frzCols!=0){
var f5=0;
if (this.GetViewport1(e4)!=null)f5+=this.GetViewport1(e4).rows.length;
if (this.GetViewport(e4)!=null)f5+=this.GetViewport(e4).rows.length;
e4.setAttribute("rowCount",f5);
}
e4.style.overflow="hidden";
if (this.GetParentSpread(e4)==null){
this.LoadScrollbarState(e4);
var f6=this.GetData(e4);
var f7=f6.getElementsByTagName("root")[0];
var f8=f7.getElementsByTagName("activespread")[0];
if (f8!=null&&f8.innerHTML!=""){
this.SetPageActiveSpread(document.getElementById(this.Trim(f8.innerHTML)));
}
}
this.InitLayout(e4);
e4.e2=true;
if (this.GetPageActiveSpread()==e4&&(e4.getAttribute("AllowInsert")=="false"||e4.getAttribute("IsNewRow")=="true")){
var f9=this.GetCmdBtn(e4,"Insert");
this.UpdateCmdBtnState(f9,true);
f9=this.GetCmdBtn(e4,"Add");
this.UpdateCmdBtnState(f9,true);
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
this.CreateSizebar(e4);
this.SyncColSelection(e4);
}
this.Dispose=function (e4){
if (this.handlers==null)
this.handlers=new FpSpread_EventHandlers();
this.handlers.DetachEvents(e4);
}
this.CmdbarMouseOver=function (event){
var g0=this.GetTarget(event);
if (g0!=null&&g0.tagName=="IMG"&&g0.getAttribute("disabled")!="true"){
g0.style.backgroundColor="cyan";
}
}
this.CmdbarMouseOut=function (event){
var g0=this.GetTarget(event);
if (g0!=null&&g0.tagName=="IMG"){
g0.style.backgroundColor="";
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
var e9=this.GetTopSpread(e4);
var g1=document.getElementById(e9.id+"_textBox");
if (g1!=null&&g1.value!=""){
var e5=window.navigator.userAgent;
var e6=(e5.indexOf("Firefox/3.")>=0);
if (e6&&this.a8!=null)
this.a8.value=this.a8.value+g1.value;
g1.value="";
}
}
this.IsXHTML=function (e4){
var e9=this.GetTopSpread(e4);
var g2=e9.getAttribute("strictMode");
return (g2!=null&&g2=="true");
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
var g3=document.createEvent("Events")
g3.initEvent(name,true,true);
return g3;
}
this.Refresh=function (e4){
var g0=e4.style.display;
e4.style.display="none";
e4.style.display=g0;
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
e4.SetSelectedRange=function (r,c,rc,cc,innerRow){e3.SetSelectedRange(this,r,c,rc,cc,innerRow);}
e4.GetSelectedRanges=function (){return e3.GetSelectedRanges(this);}
e4.AddSelection=function (r,c,rc,cc,innerRow){e3.AddSelection(this,r,c,rc,cc,innerRow);}
e4.AddSpan=function (r,c,rc,cc,spans){e3.AddSpan(this,r,c,rc,cc,spans);}
e4.RemoveSpan=function (r,c,spans){e3.RemoveSpan(this,r,c,spans);}
e4.GetActiveRow=function (){var g0=e3.GetRowFromCell(this,this.d1);if (g0<0)return g0;return e3.GetSheetIndex(this,g0);}
e4.GetActiveCol=function (){return e3.GetColFromCell(this,this.d1);}
e4.SetActiveCell=function (r,c){e3.SetActiveCell(this,r,c);}
e4.GetCellByRowCol=function (r,c){return e3.GetCellByRowCol(this,r,c);}
e4.GetValue=function (r,c){return e3.GetValue(this,r,c);}
e4.SetValue=function (r,c,v,noEvent,recalc){e3.SetValue(this,r,c,v,noEvent,recalc);}
e4.GetFormula=function (r,c){return e3.GetFormula(this,r,c);}
e4.SetFormula=function (r,c,f,recalc,clientOnly){e3.SetFormula(this,r,c,f,recalc,clientOnly);}
e4.GetHiddenValue=function (r,colName){return e3.GetHiddenValue(this,r,colName);}
e4.GetSheetRowIndex=function (r){return e3.GetSheetRowIndex(this,r);}
e4.GetSheetColIndex=function (c,innerRow){return e3.GetSheetColIndex(this,c,innerRow);}
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
e4.GetSpread=function (g0){return e3.GetSpread(g0);}
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
e4.SizeAll=function (){e3.SizeAll(this);}
e4.ShowMessage=function (msg,r,c,time){return e3.ShowMessage(this,msg,r,c,time);}
e4.HideMessage=function (r,c){return e3.HideMessage(this,r,c);}
e4.ProcessKeyMap=function (event){
if (this.keyMap!=null){
var f0=this.keyMap.length;
for (var f1=0;f1<f0;f1++){
var g4=this.keyMap[f1];
if (event.keyCode==g4.key&&event.ctrlKey==g4.ctrl&&event.shiftKey==g4.shift&&event.altKey==g4.alt){
var g5=false;
if (typeof(g4.action)=="function")
g5=g4.action();
else 
g5=eval(g4.action);
return g5;
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
var e9=this.GetTopSpread(e4);
var g1=document.getElementById(e9.id+"_textBox");
if (g1==null)
{
g1=document.createElement('INPUT');
g1.type="text";
g1.setAttribute("autocomplete","off");
g1.style.position="absolute";
g1.style.borderWidth=0;
g1.style.top="-10px";
g1.style.left="-100px";
g1.style.width="0px";
g1.style.height="1px";
if (e4.tabIndex!=null)
g1.tabIndex=e4.tabIndex;
g1.id=e4.id+"_textBox";
e4.insertBefore(g1,e4.firstChild);
}
}
this.CreateSizebar=function (e4){
e4.sizeBar=document.getElementById(e4.id+"_sizeBar");
if (e4.sizeBar==null&&(e4.frzRows>0||e4.frzCols>0))
{
e4.sizeBar=document.createElement("img");
e4.sizeBar.style.position="absolute";
e4.sizeBar.style.borderWidth=1;
e4.sizeBar.style.top="0px";
e4.sizeBar.style.left="-400px";
e4.sizeBar.style.width="2px";
e4.sizeBar.style.height="400px";
e4.sizeBar.style.background="black";
e4.sizeBar.id=e4.id+"_sizeBar";
var g6=this.GetViewport(e4).parentNode;
g6.insertBefore(e4.sizeBar,null);
}
}
this.CreateLineBorder=function (e4,id){
var g7=document.getElementById(id);
if (g7==null)
{
g7=document.createElement('div');
g7.style.position="absolute";
g7.style.left="-1000px";
g7.style.top="0px";
g7.style.overflow="hidden";
g7.style.border="1px solid black";
if (e4.getAttribute("FocusBorderColor")!=null)
g7.style.borderColor=e4.getAttribute("FocusBorderColor");
if (e4.getAttribute("FocusBorderStyle")!=null)
g7.style.borderStyle=e4.getAttribute("FocusBorderStyle");
g7.id=id;
var g6=this.GetViewport(e4).parentNode;
g6.insertBefore(g7,null);
}
return g7;
}
this.CreateFocusBorder=function (e4){
if (this.GetTopSpread(e4).getAttribute("hierView")=="true")return ;
if (this.GetTopSpread(e4).getAttribute("showFocusRect")=="false")return ;
if (this.GetViewport(e4)==null)return ;
var g7=this.CreateLineBorder(e4,e4.id+"_focusRectT");
g7.style.height=0;
g7=this.CreateLineBorder(e4,e4.id+"_focusRectB");
g7.style.height=0;
g7=this.CreateLineBorder(e4,e4.id+"_focusRectL");
g7.style.width=0;
g7=this.CreateLineBorder(e4,e4.id+"_focusRectR");
g7.style.width=0;
}
this.GetPosIndicator=function (e4){
var g8=e4.posIndicator;
if (g8==null)
g8=this.CreatePosIndicator(e4);
else if (g8.parentNode!=e4)
e4.insertBefore(g8,null);
return g8;
}
this.CreatePosIndicator=function (e4){
var g8=document.createElement("img");
g8.style.position="absolute";
g8.style.top="0px";
g8.style.left="-400px";
g8.style.width="10px";
g8.style.height="10px";
g8.style.zIndex=1000;
g8.id=e4.id+"_posIndicator";
if (e4.getAttribute("clienturl")!=null)
g8.src=e4.getAttribute("clienturl")+"down.gif";
else 
g8.src=e4.getAttribute("clienturlres");
e4.insertBefore(g8,null);
e4.posIndicator=g8;
return g8;
}
this.InitSpan=function (e4,e7,spans){
if (e7!=null){
var f5=0;
if (e7==this.GetViewport(e4))
f5=e7.rows.length;
var g9=e7.rows;
var h0=this.GetColCount(e4);
for (var h1=0;h1<g9.length;h1++){
if (this.IsChildSpreadRow(e4,e7,h1)){
if (e7==this.GetViewport(e4))f5--;
}else {
var h2=g9[h1].cells;
for (var h3=0;h3<h2.length;h3++){
var h4=h2[h3];
if (h4!=null&&((h4.rowSpan!=null&&h4.rowSpan>1)||(h4.colSpan!=null&&h4.colSpan>1))){
var h5=this.GetRowFromCell(e4,h4);
var h6=parseInt(h4.getAttribute("scol"));
if (h6<h0){
this.AddSpan(e4,h5,h6,h4.rowSpan,h4.colSpan,spans);
}
}
}
}
}
if (e7==this.GetViewport(e4))e4.setAttribute("rowCount",f5);
}
}
this.GetColWithSpan=function (e4,h1,spans,h3){
var h7=0;
var h8=0;
if (h3==0){
while (this.IsCovered(e4,h1,h8,spans))
{
h8++;
}
}
for (var f1=0;f1<spans.length;f1++){
if (spans[f1].rowCount>1&&(spans[f1].col<=h3||h3==0&&spans[f1].col<h8)&&h1>=spans[f1].row&&h1<spans[f1].row+spans[f1].rowCount)
h7+=spans[f1].colCount;
}
return h7;
}
this.AddSpan=function (e4,h1,h3,rc,h0,spans){
if (spans==null)spans=e4.d9;
var h9=new this.Range();
this.SetRange(h9,"Cell",h1,h3,rc,h0);
spans.push(h9);
this.PaintFocusRect(e4);
}
this.RemoveSpan=function (e4,h1,h3,spans){
if (spans==null)spans=e4.d9;
for (var f1=0;f1<spans.length;f1++){
var h9=spans[f1];
if (h9.row==h1&&h9.col==h3){
var i0=spans.length-1;
for (var i1=f1;i1<i0;i1++){
spans[i1]=spans[i1+1];
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
var i2=this.GetOperationMode(e4);
if (e4.d1==null&&i2!="MultiSelect"&&i2!="ExtendedSelect"&&e4.GetRowCount()>0&&e4.GetColCount()>0){
var i3=this.FireActiveCellChangingEvent(e4,0,0,0);
if (!i3){
e4.SetActiveCell(0,0);
var g3=this.CreateEvent("ActiveCellChanged");
g3.cmdID=e4.id;
g3.row=g3.Row=0;
g3.col=g3.Col=0;
if (e4.getAttribute("LayoutMode"))
g3.InnerRow=g3.innerRow=0;
this.FireEvent(e4,g3);
}
}
var e9=this.GetTopSpread(e4);
var g1=document.getElementById(e9.id+"_textBox");
if (e4.d1!=null){
var i4=this.GetEditor(e4.d1);
if (i4==null){
if (g1!=null){
if (this.b8!=g1){
try {g1.focus();g1.value="";}catch (g3){}
}
}
}else {
if (i4.tagName!="SELECT")i4.focus();
this.SetEditorFocus(i4);
}
}else {
if (g1!=null){
try {g1.focus();g1.value="";}catch (g3){}
}
}
this.EnableButtons(e4);
}
this.GetTotalRowCount=function (e4){
var g0=parseInt(e4.getAttribute("totalRowCount"));
if (isNaN(g0))g0=0;
return g0;
}
this.GetPageCount=function (e4){
var g0=parseInt(e4.getAttribute("pageCount"));
if (isNaN(g0))g0=0;
return g0;
}
this.GetColCount=function (e4){
var g0=parseInt(e4.getAttribute("colCount"));
if (isNaN(g0))g0=0;
return e4.frzCols+g0;
}
this.GetRowCount=function (e4){
var g0=parseInt(e4.getAttribute("rowCount"));
if (isNaN(g0))g0=0;
return g0;
}
this.GetRowCountInternal=function (e4){
var g0=parseInt(this.GetViewport(e4).rows.length);
if (isNaN(g0))g0=0;
return e4.frzRows+g0;
}
this.IsChildSpreadRow=function (e4,view,h1){
if (e4==null||view==null)return false;
if (h1>=1&&h1<view.rows.length){
var i5=view.rows[h1].getAttribute("isCSR");
if (i5!=null){
if (i5=="true")
return true;
else 
return false;
}
if (view.rows[h1].cells.length>0&&view.rows[h1].cells[0]!=null&&view.rows[h1].cells[0].firstChild!=null){
var g0=view.rows[h1].cells[0].firstChild;
if (g0.nodeName!="#text"&&g0.getAttribute("FpSpread")=="Spread"){
view.rows[h1].setAttribute("isCSR","true");
return true;
}
}
view.rows[h1].setAttribute("isCSR","false");
}
return false;
}
this.GetChildSpread=function (e4,row,rindex){
var i6=this.GetViewport(e4);
if (i6!=null){
var h1=this.GetDisplayIndex(e4,row)+1;
if (typeof(rindex)=="number")h1+=rindex;
if (h1>=1&&h1<i6.rows.length){
if (i6.rows[h1].cells.length>0&&i6.rows[h1].cells[0]!=null&&i6.rows[h1].cells[0].firstChild!=null){
var g0=i6.rows[h1].cells[0].firstChild;
if (g0.nodeName!="#text"&&g0.getAttribute("FpSpread")=="Spread"){
return g0;
}
}
}
}
return null;
}
this.GetChildSpreads=function (e4){
var f1=0;
var g5=new Array();
var i6=this.GetViewport(e4);
if (i6!=null){
for (var h1=1;h1<i6.rows.length;h1++){
if (i6.rows[h1].cells.length>0&&i6.rows[h1].cells[0]!=null&&i6.rows[h1].cells[0].firstChild!=null){
var g0=i6.rows[h1].cells[0].firstChild;
if (g0.nodeName!="#text"&&g0.getAttribute("FpSpread")=="Spread"){
g5.length=f1+1;
g5[f1]=g0;
f1++;
}
}
}
}
return g5;
}
this.GetDisplayIndex=function (e4,row){
if (row<0)return -1;
var f1=0;
var h1=0;
var i7=this.GetViewport0(e4);
if (i7==null)i7=this.GetViewport1(e4);
if (i7!=null){
if (row<i7.rows.length){
return row;
}
h1=i7.rows.length;
}
var i6=this.GetViewport(e4);
if (i6!=null){
for (f1=0;f1<i6.rows.length;f1++){
if (this.IsChildSpreadRow(e4,i6,f1))continue ;
if (h1==row)break ;
h1++;
}
}
if (i7!=null)f1+=i7.rows.length;
return f1;
}
this.GetSheetIndex=function (e4,row,c2){
var f1=0
var h1=0;
var i6=c2;
if (i6==null)i6=this.GetViewport(e4);
if (i6!=null){
if (row<0||row>=e4.frzRows+i6.rows.length)return -1;
for (f1=0;f1<row;f1++){
if (this.IsChildSpreadRow(e4,i6,f1))continue ;
h1++;
}
}
return h1;
}
this.GetParentRowIndex=function (e4){
var i8=this.GetParentSpread(e4);
if (i8==null)return -1;
var i6=this.GetViewport(i8);
if (i6==null)return -1;
var i9=e4.parentNode.parentNode;
var f1=i9.rowIndex-1;
for (;f1>0;f1--){
if (this.IsChildSpreadRow(i8,i6,f1))continue ;
else 
break ;
}
return this.GetSheetIndex(i8,f1,i6);
}
this.CreateTestBox=function (e4){
var j0=document.getElementById(e4.id+"_testBox");
if (j0==null)
{
j0=document.createElement("span");
j0.style.position="absolute";
j0.style.borderWidth=0;
j0.style.top="-500px";
j0.style.left="-100px";
j0.id=e4.id+"_testBox";
e4.insertBefore(j0,e4.firstChild);
}
return j0;
}
this.SizeToFit=function (e4,h3){
if (h3==null||h3<0)h3=0;
var e7=this.GetViewport(e4);
if (e7!=null){
var j0=this.CreateTestBox(e4);
var g9=e7.rows;
var j1=0;
for (var h1=0;h1<g9.length;h1++){
if (!this.IsChildSpreadRow(e4,e7,h1)){
var j2=this.GetCellFromRowCol(e4,h1,h3);
if (j2.colSpan>1)continue ;
var j3=this.GetPreferredCellWidth(e4,j2,j0);
if (j3>j1)j1=j3;
}
}
this.SetColWidth(e4,h3,j1);
}
}
this.GetPreferredCellWidth=function (e4,j2,j0){
if (j0==null)j0=this.CreateTestBox(e4);
var j4=this.GetRender(e4,j2);
if (j4!=null){
j0.style.fontFamily=j4.style.fontFamily;
j0.style.fontSize=j4.style.fontSize;
j0.style.fontWeight=j4.style.fontWeight;
j0.style.fontStyle=j4.style.fontStyle;
}
j0.innerHTML=j2.innerHTML;
var j3=j0.offsetWidth+8;
if (j2.style.paddingLeft!=null&&j2.style.paddingLeft.length>0)
j3+=parseInt(j2.style.paddingLeft);
if (j2.style.paddingRight!=null&&j2.style.paddingRight.length>0)
j3+=parseInt(j2.style.paddingRight);
return j3;
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
this.SynRowHeight=function (e4,c5,e7,h1,updateParent,header){
if (c5==null||e7==null)return ;
var j5=c5.rows[h1].offsetHeight;
var g6=e7.rows[h1].offsetHeight;
if (j5==g6&&h1>0)return ;
var j6=this.IsXHTML(e4);
if (h1==0&&!j6){
j5+=c5.rows[h1].offsetTop;
g6+=e7.rows[h1].offsetTop;
}
if (j6)e7.rows[h1].style.height="";
var j7=Math.max(j5,g6);
if (c5.rows[h1].style.height=="")c5.rows[h1].style.height=""+j5+"px";
if (e7.rows[h1].style.height=="")e7.rows[h1].style.height=""+g6+"px";
if (this.IsChildSpreadRow(e4,e7,h1)){
c5.rows[h1].style.height=j7;
return ;
}
if (j7>0){
if (j6){
if (j7==j5)
e7.rows[h1].style.height=""+(parseInt(e7.rows[h1].style.height)+(j7-g6))+"px";
else 
c5.rows[h1].style.height=""+(parseInt(c5.rows[h1].style.height)+(j7-j5))+"px";
}else {
if (header&&e7.rows.length>=2&&e7.cellSpacing=="0"){
if (h1==0)
if (j7==j5)
e7.rows[h1].style.height=""+(parseInt(e7.rows[h1].style.height)+(j7-g6))+"px";
else 
c5.rows[h1].style.height=""+(parseInt(c5.rows[h1].style.height)+(j7-j5))+"px";
else 
{
if (e4.frzRows>0&&h1==e4.frzRows-1&&c5==this.GetFrozRowHeader(e4)){
j5+=this.GetRowHeader(e4).rows[0].offsetTop;
g6+=this.GetViewport(e4).rows[0].offsetTop;
c5.rows[h1].style.height=""+(parseInt(c5.rows[h1].style.height)+(Math.max(j5,g6)-j5))+"px";
}else {
c5.rows[h1].style.height=""+j7+"px";
e7.rows[h1].style.height=""+j7+"px";
}
}
}else {
if (j7==j5)
e7.rows[h1].style.height=""+(parseInt(e7.rows[h1].style.height)+(j7-g6))+"px";
else 
c5.rows[h1].style.height=""+(parseInt(c5.rows[h1].style.height)+(j7-j5))+"px";
}
}
}
if (updateParent){
var i8=this.GetParentSpread(e4);
if (i8!=null)this.UpdateRowHeight(i8,e4);
}
}
this.SizeAll=function (e4){
var j8=this.GetChildSpreads(e4);
if (j8!=null&&j8.length>0){
for (var f1=0;f1<j8.length;f1++){
this.SizeAll(j8[f1]);
}
}
this.SizeSpread(e4);
if (this.GetParentSpread(e4)!=null)
this.Refresh(e4);
}
this.EnsureAllRowHeights=function (e4){
if (this.GetFrozColHeader(e4)!=null&&this.GetColHeader(e4)!=null){
for (var f1=0;f1<this.GetFrozColHeader(e4).rows.length;f1++){
this.SynRowHeight(e4,this.GetFrozColHeader(e4),this.GetColHeader(e4),f1,false,false);
}
}
if (this.GetFrozColFooter(e4)!=null&&this.GetColFooter(e4)!=null){
for (var f1=0;f1<this.GetFrozColFooter(e4).rows.length;f1++){
this.SynRowHeight(e4,this.GetFrozColFooter(e4),this.GetColFooter(e4),f1,false,false);
}
}
if (this.GetViewport0(e4)!=null&&this.GetViewport1(e4)!=null){
for (var f1=0;f1<this.GetViewport1(e4).rows.length;f1++){
this.SynRowHeight(e4,this.GetViewport0(e4),this.GetViewport1(e4),f1,false,false);
this.SynRowHeight(e4,this.GetViewport0(e4),this.GetViewport1(e4),f1,false,false);
}
}
if (this.GetViewport(e4)!=null&&this.GetViewport2(e4)!=null){
for (var f1=0;f1<this.GetViewport(e4).rows.length;f1++){
this.SynRowHeight(e4,this.GetViewport2(e4),this.GetViewport(e4),f1,false,false);
this.SynRowHeight(e4,this.GetViewport2(e4),this.GetViewport(e4),f1,false,false);
}
}
if (this.GetFrozRowHeader(e4)!=null){
for (var f1=0;f1<this.GetViewport1(e4).rows.length;f1++){
this.SynRowHeight(e4,this.GetFrozRowHeader(e4),this.GetViewport1(e4),f1,false,true);
this.SynRowHeight(e4,this.GetFrozRowHeader(e4),this.GetViewport0(e4),f1,false,true);
}
}
if (this.GetRowHeader(e4)!=null){
for (var f1=0;f1<this.GetViewport(e4).rows.length;f1++){
this.SynRowHeight(e4,this.GetRowHeader(e4),this.GetViewport(e4),f1,false,true);
this.SynRowHeight(e4,this.GetRowHeader(e4),this.GetViewport2(e4),f1,false,true);
}
}
if (this.GetFrozColHeader(e4)!=null&&this.GetColHeader(e4)!=null){
for (var f1=0;f1<this.GetFrozColHeader(e4).rows.length;f1++){
this.SynRowHeight(e4,this.GetFrozColHeader(e4),this.GetColHeader(e4),f1,false,false);
}
}
if (this.GetViewport0(e4)!=null&&this.GetViewport1(e4)!=null){
for (var f1=0;f1<this.GetViewport1(e4).rows.length;f1++){
this.SynRowHeight(e4,this.GetViewport0(e4),this.GetViewport1(e4),f1,false,false);
this.SynRowHeight(e4,this.GetViewport0(e4),this.GetViewport1(e4),f1,false,false);
}
}
if (this.GetViewport(e4)!=null&&this.GetViewport2(e4)!=null){
for (var f1=0;f1<this.GetViewport(e4).rows.length;f1++){
this.SynRowHeight(e4,this.GetViewport2(e4),this.GetViewport(e4),f1,false,false);
this.SynRowHeight(e4,this.GetViewport2(e4),this.GetViewport(e4),f1,false,false);
}
}
if (this.GetFrozRowHeader(e4)!=null){
for (var f1=0;f1<this.GetViewport1(e4).rows.length;f1++){
this.SynRowHeight(e4,this.GetFrozRowHeader(e4),this.GetViewport1(e4),f1,false,true);
this.SynRowHeight(e4,this.GetFrozRowHeader(e4),this.GetViewport0(e4),f1,false,true);
}
}
if (this.GetRowHeader(e4)!=null){
for (var f1=0;f1<this.GetViewport(e4).rows.length;f1++){
this.SynRowHeight(e4,this.GetRowHeader(e4),this.GetViewport(e4),f1,false,true);
this.SynRowHeight(e4,this.GetRowHeader(e4),this.GetViewport2(e4),f1,false,true);
}
}
if (this.GetCorner(e4)!=null){
if (this.GetCorner(e4).getAttribute("allowTableCorner")!=null){
if (this.GetCorner(e4)!=null&&this.GetColHeader(e4)!=null){
for (var f1=0;f1<this.GetCorner(e4).rows.length;f1++){
this.SynRowHeight(e4,this.GetCorner(e4),this.GetColHeader(e4),f1,false,false);
}
}
if (this.GetFrozColHeader(e4)!=null&&this.GetCorner(e4)!=null){
for (var f1=0;f1<this.GetCorner(e4).rows.length;f1++){
this.SynRowHeight(e4,this.GetCorner(e4),this.GetFrozColHeader(e4),f1,false,false);
}
}
if (this.GetCorner(e4)!=null&&this.GetColHeader(e4)!=null){
for (var f1=0;f1<this.GetCorner(e4).rows.length;f1++){
this.SynRowHeight(e4,this.GetCorner(e4),this.GetColHeader(e4),f1,false,false);
}
}
if (this.GetFrozColHeader(e4)!=null&&this.GetCorner(e4)!=null){
for (var f1=0;f1<this.GetCorner(e4).rows.length;f1++){
this.SynRowHeight(e4,this.GetCorner(e4),this.GetFrozColHeader(e4),f1,false,false);
}
}
}
}
}
this.SizeSpread=function (e4,skipRowHeight){
var j6=this.IsXHTML(e4);
var c2=this.GetViewport(e4);
if (c2==null)return ;
if (skipRowHeight==null)this.EnsureAllRowHeights(e4);
var c6=this.GetColHeader(e4);
var j9=this.GetColGroup(c2);
var k0=this.GetColGroup(c6);
if (j9!=null&&j9.childNodes.length>0&&k0!=null&&k0.childNodes.length>0){
var k1=-1;
if (this.b4!=null)k1=parseInt(this.b4.getAttribute("index"));
if ((this.b4==null||k1==0)&&e4.frzCols>=0)
{
k0.childNodes[0].width=(j9.childNodes[0].offsetLeft+j9.childNodes[0].offsetWidth-c2.cellSpacing);
}
}
var k2=this.GetFrozColHeader(e4);
if (j9!=null&&j9.childNodes.length>0&&k2!=null){
var k3=0;
var k4=this.GetColGroup(this.GetViewport2(e4));
for (var f1=0;f1<k4.childNodes.length;f1++)k3+=k4.childNodes[f1].offsetWidth;
k2.parentNode.parentNode.style.width=""+(k3+j9.childNodes[0].offsetLeft)+"Px";
}
this.SyncMsgs(e4);
if (e4.frzCols>0){
var k5=this.GetFrozColHeader(e4);
if (k5!=null){
var k6=parseInt(k5.parentNode.parentNode.style.width);
if (k5!=null)
k5.parentNode.style.width=""+k6+"px";
var k7=this.GetFrozColFooter(e4);
if (k7!=null)
k7.parentNode.style.width=""+k6+"px";
}
}
if (skipRowHeight==null)this.EnsureAllRowHeights(e4);
var c5=this.GetRowHeader(e4);
var c6=this.GetColHeader(e4);
var k8=this.GetColFooter(e4);
var i8=this.GetParentSpread(e4);
if (i8!=null)this.UpdateRowHeight(i8,e4);
var j7=e4.clientHeight;
var k9=this.GetCommandBar(e4);
if (k9!=null)
{
k9.style.width=""+e4.clientWidth+"px";
if (e4.style.position!="absolute"&&e4.style.position!="relative"){
k9.parentNode.style.borderTop="1px solid white";
k9.parentNode.style.backgroundColor=k9.style.backgroundColor;
}
var l0=this.GetElementById(k9,e4.id+"_cmdTable");
if (l0!=null){
if (e4.style.position!="absolute"&&e4.style.position!="relative"&&(l0.style.height==""||parseInt(l0.style.height)<27)){
l0.style.height=""+(l0.offsetHeight+3)+"px";
}
if (!j6&&parseInt(c2.cellSpacing)>0)
l0.parentNode.style.height=""+(l0.offsetHeight+3)+"px";
j7-=Math.max(l0.parentNode.offsetHeight,l0.offsetHeight);
}
if (l0.offsetHeight>l0.parentNode.offsetHeight)j7+=2;
if (e4.style.position!="absolute"&&e4.style.position!="relative")
k9.style.position="";
}
var c6=this.GetColHeader(e4);
if (c6!=null)
{
if (!e4.initialized)
c6.parentNode.style.height=""+(c6.offsetHeight-parseInt(c6.cellSpacing))+"px";
j7-=c6.parentNode.offsetHeight;
if (j6)
j7+=parseInt(c6.cellSpacing);
}
var k8=this.GetColFooter(e4);
if (k8!=null)
{
j7-=k8.offsetHeight;
k8.parentNode.style.height=""+(k8.offsetHeight)+"px";
}
var c8=this.GetHierBar(e4);
if (c8!=null)
{
j7-=c8.offsetHeight;
}
var l1=this.GetGroupBar(e4);
if (l1!=null){
j7-=l1.offsetHeight;
}
var c9=this.GetPager1(e4);
if (c9!=null)
{
j7-=c9.offsetHeight;
this.InitSlideBar(e4,c9);
}
if (!j6&&e4.frzRows>0&&c5){
var j5=c5.rows[0].offsetTop;
var g6=this.GetViewport(e4).rows[0].offsetTop;
j7-=(g6-j5);
}
var l2=(e4.getAttribute("cmdTop")=="true");
var d0=this.GetPager2(e4);
if (d0!=null)
{
d0.style.width=""+(e4.clientWidth-10)+"px";
j7-=Math.max(d0.offsetHeight,28);
this.InitSlideBar(e4,d0);
}
var l3=null;
if (c5!=null)l3=c5.parentNode;
var l4=null;
if (c6!=null)l4=c6.parentNode;
var l5=null;
if (k8!=null)l5=k8.parentNode;
var l6=this.GetFooterCorner(e4);
if (l5!=null)
{
l5.style.height=""+k8.offsetHeight-parseInt(c2.cellSpacing)+"px";
if (l6!=null){
l6.parentNode.style.height=l5.style.height;
}
}
if (l6!=null&&!j6)
l6.width=""+(l6.parentNode.offsetWidth+parseInt(c2.cellSpacing))+"px";
var c4=this.GetCorner(e4);
if (l4!=null)
{
if (!e4.initialized)
l4.style.height=""+c6.offsetHeight-parseInt(c2.cellSpacing)+"px";
if (c4!=null){
c4.parentNode.style.height=l4.style.height;
}
}
if (c4!=null&&!j6)
c4.width=""+(c4.parentNode.offsetWidth+parseInt(c2.cellSpacing))+"px";
var l7=0;
if (this.GetColFooter(e4)){
l7=this.GetColFooter(e4).offsetHeight;
}
if (k9!=null&&!l2){
if (d0!=null){
if (e4.style.position=="absolute"||e4.style.position=="relative"){
k9.style.position="absolute";
k9.style.top=""+(e4.clientHeight-Math.max(d0.offsetHeight,28)-k9.offsetHeight)+"px";
}else {
k9.style.position="absolute";
k9.style.top=""+(c2.parentNode.offsetTop+l7+c2.parentNode.offsetHeight)+"px";
}
}else {
if (e4.style.position=="absolute"||e4.style.position=="relative"){
k9.style.position="absolute";
k9.style.top=""+(e4.clientHeight-k9.offsetHeight)+"px";
}else {
k9.style.position="absolute";
if (d0!=null)
k9.style.top=""+(this.GetOffsetTop(e4,e4,document.body)+e4.clientHeight-Math.max(d0.offsetHeight,28)-k9.offsetHeight)+"px";
else 
k9.style.top=""+(this.GetOffsetTop(e4,e4,document.body)+e4.clientHeight-k9.offsetHeight+1)+"px";
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
if (k9!=null&&!l2)
d0.style.top=""+(c2.parentNode.offsetTop+c2.parentNode.offsetHeight+k9.offsetHeight+l7)+"px";
else 
d0.style.top=""+(c2.parentNode.offsetTop+c2.parentNode.offsetHeight+l7)+"px";
}
}
var l8=this.GetViewport0(e4);
var l9=this.GetViewport1(e4);
var m0=this.GetViewport2(e4);
if (l9!=null){
l9.parentNode.style.height=""+Math.max(0,l9.offsetHeight-l9.cellSpacing)+"px";
if (l8!=null){
l8.parentNode.style.height=l9.parentNode.style.height;
l8.parentNode.style.width=""+(l8.offsetWidth-l8.cellSpacing)+"px"
}
}
if (m0!=null){
m0.parentNode.style.width=""+(m0.offsetWidth-m0.cellSpacing)+"px"
}
var m1=this.GetFrozRowHeader(e4);
if (m1!=null){
var m2=m1.offsetHeight-m1.cellSpacing;
m1.parentNode.style.height=""+Math.max(0,m2)+"px";
}
var m3=e4.clientWidth;
if (c5!=null)m3-=c5.parentNode.offsetWidth;
if (m0!=null)m3-=m0.offsetWidth;
else if (l8!=null)m3-=l8.offsetWidth;
if (l8!=null)j7-=l8.offsetHeight;
else if (l9!=null)j7-=l9.offsetHeight;
if (e4.frzRows>0)j7+=parseInt(c2.cellSpacing);
if (!j6)j7+=parseInt(c2.cellSpacing);
var m4=document.getElementById(e4.id+"_titleBar");
if (m4)j7-=m4.parentNode.parentNode.offsetHeight;
c2.parentNode.style.height=""+Math.max(j7,1)+"px";
if (e4.frzCols>0)m3+=parseInt(c2.cellSpacing);
c2.parentNode.style.width=""+(m3+parseInt(c2.cellSpacing))+"px";
if (l9!=null){
l9.parentNode.style.width=""+(c2.parentNode.clientWidth)+"px";
}
if (m0!=null)m0.parentNode.style.height=""+(c2.parentNode.clientHeight)+"px";
if (l3!=null){
l3.style.height=""+Math.max(c2.parentNode.offsetHeight,1)+"px";
}
if (this.GetParentSpread(e4)==null&&l4!=null&&l3!=null&&e4.frzCols==0){
var j3=0;
if (l3!=null){
j3=Math.max(e4.clientWidth-l3.offsetWidth,1);
}else {
j3=Math.max(e4.clientWidth,1);
}
l4.style.width=j3;
l4.parentNode.style.width=j3;
}
this.ScrollView(e4);
this.PaintFocusRect(e4);
if (c2&&!c5&&!l8&&!l9&&!m0){
c2.parentNode.parentNode.parentNode.style.height=""+c2.parentNode.offsetHeight+"px";
}
}
this.InitSlideBar=function (e4,pager){
var m5=this.GetElementById(pager,e4.id+"_slideBar");
if (m5!=null){
var j6=this.IsXHTML(e4);
if (j6)
m5.style.height=Math.max(pager.offsetHeight,28)+"px";
else 
m5.style.height=(pager.offsetHeight-2)+"px";
var g0=pager.getElementsByTagName("TABLE");
if (g0!=null&&g0.length>0){
var m6=g0[0].rows[0];
var h6=m6.cells[0];
var m7=m6.cells[2];
e4.slideLeft=Math.max(107,h6.offsetWidth+1);
if (h6.style.paddingRight!="")e4.slideLeft+=parseInt(h6.style.paddingRight);
e4.slideRight=pager.offsetWidth-m7.offsetWidth-m5.offsetWidth-3;
if (m7.style.paddingRight!="")e4.slideRight-=parseInt(m7.style.paddingLeft);
var m8=parseInt(pager.getAttribute("curPage"));
var m9=parseInt(pager.getAttribute("totalPage"))-1;
if (m9==0)m9=1;
var n0=false;
var m3=Math.max(107,e4.slideLeft)+(m8/m9)*(e4.slideRight-e4.slideLeft);
if (pager.id.indexOf("pager1")>=0&&e4.style.position!="absolute"&&e4.style.position!="relative"){
m3+=this.GetOffsetLeft(e4,pager,document);
var n1=(this.GetOffsetTop(e4,h6,pager)+this.GetOffsetTop(e4,pager,document));
m5.style.top=n1+"px";
n0=true;
}
var m4=document.getElementById(e4.id+"_titleBar");
if (pager.id.indexOf("pager1")>=0&&!n0&&m4!=null){
var n1=m4.parentNode.parentNode.offsetHeight;
m5.style.top=n1+"px";
}
m5.style.left=m3+"px";
}
}
}
this.InitLayout=function (e4){
this.SizeSpread(e4);
this.SizeSpread(e4);
}
this.GetRowByKey=function (e4,key){
if (key=="-1")
return -1;
var n2=this.GetViewport1(e4);
if (n2!=null){
var n3=n2.rows.length;
var g9=n2.rows;
for (var i9=0;i9<n3;i9++){
if (g9[i9].getAttribute("FpKey")==key){
return i9;
}
}
}
var n4=this.GetViewport(e4);
if (n4!=null){
var n3=n4.rows.length;
var g9=n4.rows;
for (var i9=0;i9<n3;i9++){
if (g9[i9].getAttribute("FpKey")==key){
if (n2!=null)i9+=n2.rows.length;
return i9;
}
}
}
if (n4!=null)
return 0;
else 
return -1;
}
this.GetColByKey=function (e4,key){
if (key=="-1")
return -1;
var n5=null;
var n2=this.GetViewport0(e4);
if (n2==null||n2.rows.length==0)n2=this.GetViewport2(e4);
if (n2!=null){
n5=this.GetColGroup(n2);
if (n5!=null){
for (var n6=0;n6<n5.childNodes.length;n6++){
var g0=n5.childNodes[n6];
if (g0.getAttribute("FpCol")==key){
return n6;
}
}
}
}
var n4=this.GetViewport(e4);
var f3=this.GetColGroup(n4);
if (f3==null||f3.childNodes.length==0)
f3=this.GetColGroup(this.GetColHeader(e4));
if (f3!=null){
for (var n6=0;n6<f3.childNodes.length;n6++){
var g0=f3.childNodes[n6];
if (g0.getAttribute("FpCol")==key){
if (n5!=null){
n6+=n5.childNodes.length;
}
return n6;
}
}
}
return 0;
}
this.IsRowSelected=function (e4,i9){
var n7=this.GetSelection(e4);
if (n7!=null){
var n8=n7.firstChild;
while (n8!=null){
var h1;
var n3;
var n9=this.GetOperationMode(e4);
if (e4.getAttribute("LayoutMode")&&(n9=="ExtendedSelect"||n9=="MultiSelect")){
var o0=parseInt(n8.getAttribute("row"));
h1=this.GetFirstRowFromKey(e4,o0);
}
else 
h1=parseInt(n8.getAttribute("rowIndex"));
if (e4.getAttribute("LayoutMode")&&(n9=="ExtendedSelect"||n9=="MultiSelect"))
n3=parseInt(e4.getAttribute("layoutrowcount"));
else 
n3=parseInt(n8.getAttribute("rowcount"));
if (h1<=i9&&i9<h1+n3)
return true;
n8=n8.nextSibling;
}
}
}
this.InitSelection=function (e4){
var h1=0;
var h3=0;
var f6=this.GetData(e4);
var f7=f6.getElementsByTagName("root")[0];
var o1=f7.getElementsByTagName("state")[0];
var n7=o1.getElementsByTagName("selection")[0];
var o2=o1.firstChild;
while (o2!=null&&o2.tagName!="activerow"&&o2.tagName!="ACTIVEROW"){
o2=o2.nextSibling;
}
if (o2!=null&&!e4.getAttribute("LayoutMode"))
h1=this.GetRowByKey(e4,o2.innerHTML);
if (h1>=this.GetRowCount(e4))h1=0;
var o3=o1.firstChild;
while (o3!=null&&o3.tagName!="activecolumn"&&o3.tagName!="ACTIVECOLUMN"){
o3=o3.nextSibling;
}
if (o3!=null&&!e4.getAttribute("LayoutMode"))
h3=this.GetColByKey(e4,o3.innerHTML);
if (e4.getAttribute("LayoutMode")&&o2!=null&&o3!=null){
h1=parseInt(o2.innerHTML);
h3=parseInt(o3.innerHTML);
var h4;
if (h1!=-1&&h3!=-1)h4=this.GetCellByRowCol2(e4,o2.innerHTML,o3.innerHTML);
if (h4){
h1=this.GetRowFromCell(e4,h4);
h3=this.GetColFromCell(e4,h4);
}
}
if (h1<0)h1=0;
if (h1>=0||h3>=0){
var o4=f6;
if (this.GetParentSpread(e4)!=null){
var o5=this.GetTopSpread(e4);
if (o5.initialized)o4=this.GetData(o5);
f7=o4.getElementsByTagName("root")[0];
}
var o6=f7.getElementsByTagName("activechild")[0];
e4.d3=h1;e4.d4=h3;
if ((this.GetParentSpread(e4)==null&&(o6==null||o6.innerHTML==""))||(o6!=null&&e4.id==this.Trim(o6.innerHTML))){
this.UpdateAnchorCell(e4,h1,h3);
}else {
e4.d1=this.GetCellFromRowCol(e4,h1,h3);
}
}
var n8=n7.firstChild;
while (n8!=null){
var h1=0;
var h3=0;
if (e4.getAttribute("LayoutMode")&&n8.getAttribute("row")!="-1"&&n8.getAttribute("col")!="-1"){
var h4=this.GetCellByRowCol2(e4,n8.getAttribute("row"),n8.getAttribute("col"));
if (h4){
h1=this.GetRowFromCell(e4,h4);
h3=this.GetColFromCell(e4,h4);
}
}
else if (e4.getAttribute("LayoutMode")&&n8.getAttribute("col")!="-1"&&n8.getAttribute("row")=="-1"&&n8.getAttribute("rowcount")=="-1"){
var i9=this.GetRowTemplateRowFromGroupCell(e4,parseInt(n8.getAttribute("col")));
var h4=this.GetCellByRowCol2(e4,i9,parseInt(n8.getAttribute("col")));
if (h4){
h1=parseInt(h4.parentNode.getAttribute("row"));
h3=this.GetColFromCell(e4,h4);
}
}
else {
h1=this.GetRowByKey(e4,n8.getAttribute("row"));
h3=this.GetColByKey(e4,n8.getAttribute("col"));
}
var n3=parseInt(n8.getAttribute("rowcount"));
var h0=parseInt(n8.getAttribute("colcount"));
n8.setAttribute("rowIndex",h1);
n8.setAttribute("colIndex",h3);
if (e4.getAttribute("LayoutMode")&&n8.getAttribute("col")>=0&&n8.getAttribute("row")>=0&&(n8.getAttribute("rowcount")>=1||n8.getAttribute("colcount")>=1)){
var o7=n8.nextSibling;
if (parseInt(n8.getAttribute("row"))!=parseInt(o2.innerHTML)||parseInt(n8.getAttribute("col"))!=parseInt(o3.innerHTML))n7.removeChild(n8);
n8=o7;
continue ;
}
if (e4.getAttribute("LayoutMode")&&n8.getAttribute("col")=="-1"&&n8.getAttribute("row")!=-1){
n3=parseInt(e4.getAttribute("layoutrowcount"));
}
if (e4.getAttribute("LayoutMode")&&n8.getAttribute("col")!="-1"&&n8.getAttribute("row")=="-1"&&n8.getAttribute("rowcount")=="-1")
this.PaintMultipleRowSelection(e4,h1,h3,1,1,true);
else 
this.PaintSelection(e4,h1,h3,n3,h0,true);
n8=n8.nextSibling;
}
this.PaintFocusRect(e4);
}
this.TranslateKeyPress=function (event){
if (event.ctrlKey&&!event.altKey){
this.TranslateKey(event);
}
var e4=this.GetPageActiveSpread();
if (e4!=null&&event.keyCode==event.DOM_VK_RETURN)this.CancelDefault(event);
}
this.TranslateKey=function (event){
event=this.GetEvent(event);
var o8=this.GetTarget(event);
try {
if (document.readyState!=null&&document.readyState!="complete")return ;
var e4=this.GetPageActiveSpread();
if (typeof(e4.getAttribute("mcctCellType"))!="undefined"&&e4.getAttribute("mcctCellType")=="true")return ;
if (this.GetOperationMode(e4)=="RowMode"&&this.GetEnableRowEditTemplate(e4)=="true"&&this.IsInRowEditTemplate(e4,o8))return ;
if (e4!=null){
if (event.keyCode==229){
this.CancelDefault(event);
return ;
}
if (o8.tagName!="HTML"&&!this.IsChild(o8,this.GetTopSpread(e4)))return ;
this.KeyDown(e4,event);
var o9=false;
if (event.keyCode==event.DOM_VK_TAB){
var p0=this.GetProcessTab(e4);
o9=(p0=="true"||p0=="True");
}
if (o9)
this.CancelDefault(event);
}
}catch (g3){}
}
this.IsInRowEditTemplate=function (e4,o8){
while (o8&&o8.parentNode){
o8=o8.parentNode;
if (o8.tagName=="DIV"&&o8.id==e4.id+"_RowEditTemplateContainer")
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
var f0=e4.keyMap.length;
for (var f1=0;f1<f0;f1++){
var g4=e4.keyMap[f1];
if (g4!=null&&g4.key==keyCode&&g4.ctrl==ctrl&&g4.shift==shift&&g4.alt==alt){
for (var i1=f1+1;i1<f0;i1++){
e4.keyMap[i1-1]=e4.keyMap[i1];
}
e4.keyMap.length=e4.keyMap.length-1;
break ;
}
}
}
this.AddKeyMap=function (e4,keyCode,ctrl,shift,alt,action){
if (e4.keyMap==null)e4.keyMap=new Array();
var g4=this.GetKeyAction(e4,keyCode,ctrl,shift,alt);
if (g4!=null){
g4.action=action;
}else {
var f0=e4.keyMap.length;
e4.keyMap.length=f0+1;
e4.keyMap[f0]=new this.KeyAction(keyCode,ctrl,shift,alt,action);
}
}
this.GetKeyAction=function (e4,keyCode,ctrl,shift,alt){
if (e4.keyMap==null)e4.keyMap=new Array();
var f0=e4.keyMap.length;
for (var f1=0;f1<f0;f1++){
var g4=e4.keyMap[f1];
if (g4!=null&&g4.key==keyCode&&g4.ctrl==ctrl&&g4.shift==shift&&g4.alt==alt){
return g4;
}
}
return null;
}
this.MoveToPrevCell=function (e4){
var p1=this.EndEdit(e4);
if (!p1)return ;
var h1=e4.GetActiveRow();
var h3=e4.GetActiveCol();
this.MoveLeft(e4,h1,h3);
}
this.MoveToNextCell=function (e4){
var p1=this.EndEdit(e4);
if (!p1)return ;
var h1=e4.GetActiveRow();
var h3=e4.GetActiveCol();
this.MoveRight(e4,h1,h3);
}
this.MoveToNextRow=function (e4){
var p1=this.EndEdit(e4);
if (!p1)return ;
var h1=e4.GetActiveRow();
var h3=e4.GetActiveCol();
this.MoveDown(e4,h1,h3);
}
this.MoveToPrevRow=function (e4){
var p1=this.EndEdit(e4);
if (!p1)return ;
var h1=e4.GetActiveRow();
var h3=e4.GetActiveCol();
if (h1>0)
this.MoveUp(e4,h1,h3);
}
this.MoveToFirstColumn=function (e4){
var p1=this.EndEdit(e4);
if (!p1)return ;
var h1=e4.GetActiveRow();
if (e4.d1.parentNode.rowIndex>=0)
this.UpdateLeadingCell(e4,h1,0);
}
this.MoveToLastColumn=function (e4){
var p1=this.EndEdit(e4);
if (!p1)return ;
var h1=e4.GetActiveRow();
if (e4.d1.parentNode.rowIndex>=0){
h3=this.GetColCount(e4)-1;
this.UpdateLeadingCell(e4,h1,h3);
}
}
this.UpdatePostbackData=function (e4){
this.SaveData(e4);
}
this.PrepareData=function (n8){
var g5="";
if (n8!=null){
if (n8.nodeName=="#text")
g5=n8.nodeValue;
else {
g5=this.GetBeginData(n8);
var g0=n8.firstChild;
while (g0!=null){
var p2=this.PrepareData(g0);
if (p2!="")g5+=p2;
g0=g0.nextSibling;
}
g5+=this.GetEndData(n8);
}
}
return g5;
}
this.GetBeginData=function (n8){
var g5="<"+n8.nodeName.toLowerCase();
if (n8.attributes!=null){
for (var f1=0;f1<n8.attributes.length;f1++){
var p3=n8.attributes[f1];
if (p3.nodeName!=null&&p3.nodeName!=""&&p3.nodeName!="style"&&p3.nodeValue!=null&&p3.nodeValue!="")
g5+=(" "+p3.nodeName+"=\""+p3.nodeValue+"\"");
}
}
g5+=">";
return g5;
}
this.GetEndData=function (n8){
return "</"+n8.nodeName.toLowerCase()+">";
}
this.SaveData=function (e4){
if (e4==null)return ;
try {
var f6=this.GetData(e4);
var f7=f6.getElementsByTagName("root")[0];
var g0=this.PrepareData(f7);
var p4=document.getElementById(e4.id+"_data");
p4.value=encodeURIComponent(g0);
}catch (g3){
alert("e "+g3);
}
}
this.SetActiveSpread=function (event){
try {
event=this.GetEvent(event);
var o8=this.GetTarget(event);
var p5=this.GetSpread(o8,false);
var p6=this.GetPageActiveSpread();
if (this.a7&&(p5==null||p5!=p6)&&p5.getAttribute("mcctCellType")!="true"&&p6.getAttribute("mcctCellType")!="true"){
if (o8!=this.a8&&this.a8!=null){
if (this.a8.blur!=null)this.a8.blur();
}
var p1=this.EndEdit();
if (!p1)return ;
}
var p7=false;
if (p5==null){
p5=this.GetSpread(o8,true);
p7=(p5!=null);
}
var h4=this.GetCell(o8,true);
if (h4==null&&p6!=null&&p6.e2){
this.SaveData(p6);
p6.e2=false;
}
if (p6!=null&&p6.e2&&(p5!=p6||p5==null||p7)){
this.SaveData(p6);
p6.e2=false;
}
if (p6!=null&&p6.e2&&p5==p6&&o8.tagName=="INPUT"&&(o8.type=="submit"||o8.type=="button"||o8.type=="image")){
this.SaveData(p6);
p6.e2=false;
}
if (p5!=null&&this.GetOperationMode(p5)=="ReadOnly")return ;
var o5=null;
if (p5==null){
if (p6==null)return ;
o5=this.GetTopSpread(p6);
this.SetActiveSpreadID(o5,"",null,false);
this.SetPageActiveSpread(null);
}else {
if (p5!=p6){
if (p6!=null){
o5=this.GetTopSpread(p6);
this.SetActiveSpreadID(o5,"",null,false);
}
if (p7){
o5=this.GetTopSpread(p5);
var p8=this.GetTopSpread(p6);
if (o5!=p8){
this.SetActiveSpreadID(o5,p5.id,p5.id,true);
this.SetPageActiveSpread(p5);
}else {
this.SetActiveSpreadID(o5,p6.id,p6.id,true);
this.SetPageActiveSpread(p6);
}
}else {
o5=this.GetTopSpread(p5);
this.SetPageActiveSpread(p5);
this.SetActiveSpreadID(o5,p5.id,p5.id,false);
}
}
}
}catch (g3){}
}
this.SetActiveSpreadID=function (e4,id,child,p7){
var f6=this.GetData(e4);
var f7=f6.getElementsByTagName("root")[0];
var f8=f7.getElementsByTagName("activespread")[0];
var p9=f7.getElementsByTagName("activechild")[0];
if (f8==null)return ;
if (p7&&p9!=null&&p9.nodeValue!=""){
f8.innerHTML=p9.innerHTML;
}else {
f8.innerHTML=id;
if (child!=null&&p9!=null)p9.innerHTML=child;
}
this.SaveData(e4);
e4.e2=false;
}
this.GetSpread=function (ele,incCmdBar){
var j3=ele;
while (j3!=null&&j3.tagName!="BODY"){
if (typeof(j3.getAttribute)!="function")break ;
var e4=j3.getAttribute("FpSpread");
if (e4==null)e4=j3.FpSpread;
if (e4=="Spread"){
if (!incCmdBar){
var g0=ele;
while (g0!=null&&g0!=j3){
if (g0.id==j3.id+"_commandBar"||g0.id==j3.id+"_pager1"||g0.id==j3.id+"_pager2")return null;
g0=g0.parentNode;
}
}
return j3;
}
j3=j3.parentNode;
}
return null;
}
this.GetActiveChildSheetView=function (e4){
var p6=this.GetPageActiveSheetView();
if (typeof(p6)=="undefined")return null;
var o5=this.GetTopSpread(e4);
var q0=this.GetTopSpread(p6);
if (q0!=o5)return null;
if (p6==q0)return null;
return p6;
}
this.ScrollViewport=function (event){
var g0=this.GetTarget(event);
var e4=this.GetTopSpread(g0);
if (e4!=null)this.ScrollView(e4);
}
this.ScrollTo=function (e4,i9,n6){
var h4=this.GetCellByRowCol(e4,i9,n6);
if (h4==null)return ;
var i6=this.GetViewport(e4).parentNode;
if (i6==null)return ;
i6.scrollTop=h4.offsetTop;
i6.scrollLeft=h4.offsetLeft;
}
this.ScrollView=function (e4){
var p5=this.GetTopSpread(e4);
var c5=this.GetParent(this.GetRowHeader(p5));
var c6=this.GetParent(this.GetColHeader(p5));
var k8=this.GetParent(this.GetColFooter(p5));
var i6=this.GetParent(this.GetViewport(p5));
var q1=false;
if (c5!=null){
q1=(c5.scrollTop!=i6.scrollTop);
c5.scrollTop=i6.scrollTop;
}
if (c6!=null){
if (!q1)q1=(c6.scrollLeft!=i6.scrollLeft);
c6.scrollLeft=i6.scrollLeft;
}
if (k8!=null){
if (!q1)q1=(k8.scrollLeft!=i6.scrollLeft);
k8.scrollLeft=i6.scrollLeft;
}
var q2=this.GetViewport0(e4);
var q3=this.GetViewport1(e4);
var q4=this.GetViewport2(e4);
if (q4!=null){
q4.parentNode.scrollTop=i6.scrollTop;
}
if (q3!=null){
q3.parentNode.scrollLeft=i6.scrollLeft;
}
if (this.GetParentSpread(e4)==null)this.SaveScrollbarState(e4,i6.scrollTop,i6.scrollLeft);
if (q1){
var g3=this.CreateEvent("Scroll");
this.FireEvent(e4,g3);
if (e4.frzRows!=0||e4.frzCols!=0)this.SyncMsgs(e4);
}
if (i6.scrollTop>0&&i6.scrollTop+i6.offsetHeight>=this.GetViewport(p5).offsetHeight){
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
var f6=this.GetData(e4);
var f7=f6.getElementsByTagName("root")[0];
var q5=f7.getElementsByTagName("scrollTop")[0];
var q6=f7.getElementsByTagName("scrollLeft")[0];
if (e4.getAttribute("scrollContent"))
if (q5!=null&&q6!=null)
if (q5.innerHTML!=scrollTop||q6.innerHTML!=scrollLeft)
this.ShowScrollingContent(e4,q5.innerHTML==scrollTop);
if (q5!=null)q5.innerHTML=scrollTop;
if (q6!=null)q6.innerHTML=scrollLeft;
}
this.LoadScrollbarState=function (e4){
if (this.GetParentSpread(e4)!=null)return ;
var f6=this.GetData(e4);
var f7=f6.getElementsByTagName("root")[0];
var q5=f7.getElementsByTagName("scrollTop")[0];
var q6=f7.getElementsByTagName("scrollLeft")[0];
var q7=0;
if (q5!=null&&q5.innerHTML!=""){
q7=parseInt(q5.innerHTML);
}else {
q7=0;
}
var q8=0;
if (q6!=null&&q6.innerHTML!=""){
q8=parseInt(q6.innerHTML);
}else {
q8=0;
}
var i6=this.GetParent(this.GetViewport(e4));
if (i6!=null){
if (!isNaN(q7))i6.scrollTop=q7;
if (!isNaN(q8))i6.scrollLeft=q8;
var c5=this.GetParent(this.GetRowHeader(e4));
var c6=this.GetParent(this.GetColHeader(e4));
var k8=this.GetParent(this.GetColFooter(e4));
if (k8!=null){
k8.scrollLeft=i6.scrollLeft;
}
if (c5!=null){
c5.scrollTop=i6.scrollTop;
}
if (c6!=null){
c6.scrollLeft=i6.scrollLeft;
}
}
}
this.GetParent=function (g3){
if (g3==null)
return null;
else 
return g3.parentNode;
}
this.GetViewport=function (e4){
return e4.c2;
}
this.GetFrozColHeader=function (e4){
return e4.frozColHeader;
}
this.GetColFooter=function (e4){
return e4.colFooter;
}
this.GetFrozColFooter=function (e4){
return e4.frozColFooter;
}
this.GetTopTable=function (e4){
return e4.getElementsByTagName("TABLE")[0];
}
this.GetFrozRowHeader=function (e4){
return e4.frozRowHeader;
}
this.GetViewport0=function (e4){
return e4.viewport0;
}
this.GetViewport1=function (e4){
return e4.viewport1;
}
this.GetViewport2=function (e4){
return e4.viewport2;
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
this.GetCmdBtn=function (e4,id){
var p5=this.GetTopSpread(e4);
var q9=this.GetCommandBar(p5);
if (q9!=null)
return this.GetElementById(q9,p5.id+"_"+id);
else 
return null;
}
this.Range=function (){
this.type="Cell";
this.row=-1;
this.col=-1;
this.rowCount=0;
this.colCount=0;
this.innerRow=0;
}
this.SetRange=function (h9,type,i9,n6,n3,h0,innerRow){
h9.type=type;
h9.row=i9;
h9.col=n6;
h9.rowCount=n3;
h9.colCount=h0;
h9.innerRow=innerRow;
if (type=="Row"){
h9.col=h9.colCount=-1;
}else if (type=="Column"){
h9.row=h9.rowCount=-1;
}else if (type=="Table"){
h9.col=h9.colCount=-1;h9.row=h9.rowCount=-1;
}
}
this.Margin=function (left,top,right,bottom){
this.left;
this.top;
this.right;
this.bottom;
}
this.GetRender=function (h4){
var g0=h4;
if (g0.firstChild!=null&&g0.firstChild.tagName!=null&&g0.firstChild.tagName!="BR")
return g0.firstChild;
if (g0.firstChild!=null&&g0.firstChild.value!=null){
g0=g0.firstChild;
}
return g0;
}
this.GetPreferredRowHeight=function (e4,h1){
var j0=this.CreateTestBox(e4);
h1=this.GetDisplayIndex(e4,h1);
var i6=this.GetViewport(e4);
var j1=0;
var r0=i6.rows[h1].offsetHeight;
var f0=i6.rows[h1].cells.length;
for (var f1=0;f1<f0;f1++){
var j2=i6.rows[h1].cells[f1];
var j4=this.GetRender(j2);
if (j4!=null){
j0.style.fontFamily=j4.style.fontFamily;
j0.style.fontSize=j4.style.fontSize;
j0.style.fontWeight=j4.style.fontWeight;
j0.style.fontStyle=j4.style.fontStyle;
}
var n6=this.GetColFromCell(e4,j2);
j0.style.posWidth=this.GetColWidthFromCol(e4,n6);
if (j4!=null&&j4.tagName=="SELECT"){
var g0="";
for (var i1=0;i1<j4.childNodes.length;i1++){
var r1=j4.childNodes[i1];
if (r1.text!=null&&r1.text.length>g0.length)g0=r1.text;
}
j0.innerHTML=g0;
}
else if (j4!=null&&j4.tagName=="INPUT")
j0.innerHTML=j4.value;
else 
{
j0.innerHTML=j2.innerHTML;
}
r0=j0.offsetHeight;
if (r0>j1)j1=r0;
}
return Math.max(0,j1)+3;
}
this.SetRowHeight2=function (e4,h1,height){
if (height<1){
height=1;
}
h1=this.GetDisplayIndex(e4,h1);
var b5=null;
var g7=false;
if (h1<e4.frzRows){
g7=true;
if (this.GetFrozRowHeader(e4)!=null)b5=this.GetFrozRowHeader(e4).rows[h1];
}else {
h1-=e4.frzRows;
if (this.GetRowHeader(e4)!=null)b5=this.GetRowHeader(e4).rows[h1];
}
if (b5!=null)b5.style.height=""+height+"px";
if (g7){
var i6=this.GetViewport0(e4);
if (i6!=null){
if (b5!=null){
i6.rows[b5.rowIndex].style.height=""+(b5.offsetHeight-i6.rows[0].offsetTop)+"px";
}else {
i6.rows[h1].style.height=""+height+"px";
b5=i6.rows[h1];
}
}
i6=this.GetViewport1(e4);
if (i6!=null){
if (b5!=null){
i6.rows[b5.rowIndex].style.height=""+(b5.offsetHeight-i6.rows[0].offsetTop)+"px";
}else {
i6.rows[h1].style.height=""+height+"px";
b5=i6.rows[h1];
}
}
}else {
var i6=this.GetViewport(e4);
if (i6!=null){
if (b5!=null){
i6.rows[b5.rowIndex].style.height=b5.style.height;
}else {
i6.rows[h1].style.height=""+height+"px";
b5=i6.rows[h1];
}
}
i6=this.GetViewport2(e4);
if (i6!=null){
if (b5!=null){
i6.rows[b5.rowIndex].style.height=b5.style.height;
}else {
i6.rows[h1].style.height=""+height+"px";
b5=i6.rows[h1];
}
}
}
var r2=this.AddRowInfo(e4,b5.getAttribute("FpKey"));
if (r2!=null){
if (typeof(b5.style.posHeight)=="undefined")
b5.style.posHeight=height;
this.SetRowHeight(e4,r2,b5.style.posHeight);
}
var i8=this.GetParentSpread(e4);
if (i8!=null)i8.UpdateRowHeight(e4);
this.SizeSpread(e4);
}
this.GetRowHeightInternal=function (e4,h1){
var b5=null;
if (this.GetRowHeader(e4)!=null)
b5=this.GetRowHeader(e4).rows[h1];
else if (this.GetViewport(e4)!=null)
b5=this.GetViewport(e4).rows[h1];
if (b5!=null)
return b5.offsetHeight;
else 
return 0;
}
this.GetCell=function (ele,noHeader,event){
var g0=ele;
while (g0!=null){
if (noHeader){
if ((g0.tagName=="TD"||g0.tagName=="TH")&&(g0.parentNode.getAttribute("FpSpread")=="r")){
return g0;
}
}else {
if ((g0.tagName=="TD"||g0.tagName=="TH")&&(g0.parentNode.getAttribute("FpSpread")=="r"||g0.parentNode.getAttribute("FpSpread")=="ch"||g0.parentNode.getAttribute("FpSpread")=="rh")){
return g0;
}
}
g0=g0.parentNode;
}
return null;
}
this.InRowHeader=function (e4,h4){
return (this.IsChild(h4,this.GetFrozRowHeader(e4))||this.IsChild(h4,this.GetRowHeader(e4)));
}
this.InColHeader=function (e4,h4){
return (this.IsChild(h4,this.GetFrozColHeader(e4))||this.IsChild(h4,this.GetColHeader()));
}
this.InColFooter=function (e4,h4){
return (this.IsChild(h4,this.GetFrozColFooter(e4))||this.IsChild(h4,this.GetColFooter()));
}
this.IsHeaderCell=function (e4,h4){
return (h4!=null&&(h4.tagName=="TD"||h4.tagName=="TH")&&(h4.parentNode.getAttribute("FpSpread")=="ch"||h4.parentNode.getAttribute("FpSpread")=="rh"));
}
this.InFrozCols=function (e4,h4){
return (this.IsChild(h4,this.GetFrozColHeader(e4))||this.IsChild(h4,this.GetViewport0(e4))||this.IsChild(h4,this.GetViewport2(e4)));
}
this.InFrozRows=function (e4,h4){
(this.IsChild(h4,this.GetFrozRowHeader(e4))||this.IsChild(h4,this.GetViewport0(e4))||this.IsChild(h4,this.GetViewport1(e4)));
}
this.GetSizeColumn=function (e4,ele,event){
if (ele.tagName!="TD"||(this.GetColHeader(e4)==null))return null;
var n6=-1;
var g0=ele;
var q8=this.GetViewport(this.GetTopSpread(e4)).parentNode.scrollLeft+window.scrollX;
while (g0!=null&&g0.parentNode!=null&&g0.parentNode!=document.documentElement){
if (g0.parentNode.getAttribute("FpSpread")=="ch"){
var r3=this.GetOffsetLeft(e4,g0,document.body);
var r4=r3+g0.offsetWidth;
if (event.clientX+q8<r3+3){
n6=this.GetColFromCell(e4,g0)-1;
}
else if (event.clientX+q8>r4-4){
n6=this.GetColFromCell(e4,g0);
var r5=this.GetSpanCell(g0.parentNode.rowIndex,n6,e4.e1);
if (r5!=null){
n6=r5.col+r5.colCount-1;
}
}else {
n6=this.GetColFromCell(e4,g0);
var r5=this.GetSpanCell(g0.parentNode.rowIndex,n6,e4.e1);
if (r5!=null){
var j3=r3;
n6=-1;
for (var f1=r5.col;f1<r5.col+r5.colCount&&f1<this.GetColCount(e4);f1++){
if (this.IsChild(g0,this.GetColHeader(e4)))
j3+=parseInt(this.GetElementById(this.GetColHeader(e4),e4.id+"col"+f1).width);
else 
j3+=parseInt(this.GetElementById(this.GetFrozColHeader(e4),e4.id+"col"+f1).width);
if (event.clientX>j3-3&&event.clientX<j3+3){
n6=f1;
break ;
}
}
}else {
n6=-1;
}
}
if (isNaN(n6)||n6<0)return null;
var r6=0;
var r7=this.GetColCount(e4);
var r8=true;
var e7=null;
var h3=n6+1;
while (h3<r7){
var f3=this.GetColGroup(this.GetColHeader(e4));
if (h3>=e4.frzCols){
var f3=this.GetColGroup(this.GetColHeader(e4));
if (h3-e4.frzCols<f3.childNodes.length)
r6=parseInt(f3.childNodes[h3-e4.frzCols].width);
}else {
var f3=this.GetColGroup(this.GetFrozColHeader(e4));
if (h3<f3.childNodes.length)
r6=parseInt(f3.childNodes[h3].width);
}
if (r6>1){
r8=false;
break ;
}
h3++;
}
if (r8){
h3=n6+1;
while (h3<r7){
if (this.GetSizable(e4,h3)){
n6=h3;
break ;
}
h3++;
}
}
if (!this.GetSizable(e4,n6))return null;
if (this.IsChild(g0,this.GetColHeader(e4))){
if (event.offsetX<3&&g0.cellIndex==0&&this.GetFrozColHeader(e4)!=null){
return this.GetElementById(this.GetFrozColHeader(e4),e4.id+"col"+(e4.frzCols-1));
}else {
return this.GetElementById(this.GetColHeader(e4),e4.id+"col"+n6);
}
}else {
return this.GetElementById(this.GetFrozColHeader(e4),e4.id+"col"+n6);
}
}
g0=g0.parentNode;
}
return null;
}
this.GetColGroup=function (g0){
if (g0==null)return null;
var f3=g0.getElementsByTagName("COLGROUP");
if (f3!=null&&f3.length>0){
if (g0.colgroup!=null)return g0.colgroup;
var p8=new Object();
p8.childNodes=new Array();
for (var f1=0;f1<f3[0].childNodes.length;f1++){
if (f3[0].childNodes[f1]!=null&&f3[0].childNodes[f1].tagName=="COL"){
var f0=p8.childNodes.length;
p8.childNodes.length++;
p8.childNodes[f0]=f3[0].childNodes[f1];
}
}
g0.colgroup=p8;
return p8;
}else {
return null;
}
}
this.GetSizeRow=function (e4,ele,event){
var n3=this.GetRowCount(e4);
if (n3==0)return null;
if (e4.getAttribute("LayoutMode"))return null;
var h4=this.GetCell(ele);
if (h4==null){
if (ele.getAttribute("FpSpread")=="rowpadding"){
if (event.clientY<3){
var f0=ele.parentNode.rowIndex;
if (f0>1){
var i9=ele.parentNode.parentNode.rows[f0-1];
if (this.GetSizable(e4,i9))
return i9;
}
}
}
var c4=this.GetCorner(e4);
if (c4!=null&&this.IsChild(ele,c4)){
if (event.clientY>ele.offsetHeight-4){
var r9=null;
var f0=0;
r9=this.GetRowHeader(e4);
if (r9!=null){
while (f0<r9.rows.length&&r9.rows[f0].offsetHeight<2&&!this.GetSizable(e4,r9.rows[f0]))
f0++;
if (f0<r9.rows.length&&this.GetSizable(e4,r9.rows[f0])&&r9.rows[f0].offsetHeight<2)
return r9.rows[f0];
}
}else {
}
}
return null;
}
var e0=e4.e0;
var d9=e4.d9;
var s0=this.IsChild(h4,this.GetFrozRowHeader(e4));
var g0=h4;
var q7=this.GetViewport(this.GetTopSpread(e4)).parentNode.scrollTop+window.scrollY;
while (g0!=null&&g0!=document.documentElement){
if (g0.getAttribute("FpSpread")=="rh"){
var f0=-1;
var s1=this.GetOffsetTop(e4,g0,document.body);
var s2=s1+g0.offsetHeight;
if (event.clientY+q7<s1+3){
if (g0.rowIndex>1)
f0=g0.rowIndex-1;
else if (g0.rowIndex==0&&!s0&&this.GetFrozRowHeader(e4)!=null){
s0=true;
f0=g0.frzRows-1;
}
}
else if (event.clientY+q7>s2-4){
var r5=this.GetSpanCell(this.GetRowFromCell(e4,h4),this.GetColFromCell(e4,h4),e0);
if (r5!=null){
var j7=s1;
for (var f1=r5.row;f1<r5.row+r5.rowCount;f1++){
j7+=parseInt(this.GetRowHeader(e4).rows[f1].style.height);
if (event.clientY>j7-3&&event.clientY<j7+3){
f0=f1;
break ;
}
}
}else {
if (g0.rowIndex>=0)f0=g0.rowIndex;
}
}
else {
break ;
}
var j7=0;
var n3=this.GetRowHeader(e4).rows.length;
if (s0)n3=this.GetFrozRowHeader(e4).rows.length;
var s3=true;
var r9=null;
if (s0)
r9=this.GetFrozRowHeader(e4);
else 
r9=this.GetRowHeader(e4);
var h1=f0+1;
while (h1<n3){
if (r9.rows[h1].style.height!=null)j7=parseInt(r9.rows[h1].style.height);
else j7=parseInt(r9.rows[h1].offsetHeight);
if (j7>1){
s3=false;
break ;
}
h1++;
}
if (s3){
h1=f0+1;
while (h1<n3){
if (this.GetSizable(e4,this.GetRowHeader(e4).rows[h1])){
f0=h1;
break ;
}
h1++;
}
}
if (f0>=0&&this.GetSizable(e4,r9.rows[f0])){
return r9.rows[f0];
}
else if (event.clientY<3){
while (f0>0&&r9.rows[f0].offsetHeight==0&&!this.GetSizable(e4,r9.rows[f0]))
f0--;
if (f0>=0&&this.GetSizable(e4,r9.rows[f0]))
return r9.rows[f0];
else 
return null;
}
}
g0=g0.parentNode;
}
return null;
}
this.GetElementById=function (i8,id){
if (i8==null)return null;
var g0=i8.firstChild;
while (g0!=null){
if (g0.id==id||(typeof(g0.getAttribute)=="function"&&g0.getAttribute("name")==id))return g0;
var p8=this.GetElementById(g0,id)
if (p8!=null)return p8;
g0=g0.nextSibling;
}
return null;
}
this.GetSizable=function (e4,ele){
if (typeof(ele)=="number"){
var h4=null;
if (ele<e4.frzCols)
h4=this.GetElementById(this.GetFrozColHeader(e4),e4.id+"col"+ele);
else 
h4=this.GetElementById(this.GetColHeader(e4),e4.id+"col"+ele);
return (h4!=null&&(h4.getAttribute("Sizable")==null||h4.getAttribute("Sizable")=="True"));
}
return (ele!=null&&(ele.getAttribute("Sizable")==null||ele.getAttribute("Sizable")=="True"));
}
this.GetSpanWidth=function (e4,n6,r7){
var j3=0;
var e7=this.GetViewport(e4);
if (e7!=null){
var f3=this.GetColGroup(e7);
if (f3!=null){
for (var f1=n6;f1<n6+r7;f1++){
j3+=parseInt(f3.childNodes[f1].width);
}
}
}
return j3;
}
this.GetCellType=function (h4){
if (h4!=null&&h4.getAttribute("FpCellType")!=null)return h4.getAttribute("FpCellType");
if (h4!=null&&h4.getAttribute("FpRef")!=null){
var g0=document.getElementById(h4.getAttribute("FpRef"));
return g0.getAttribute("FpCellType");
}
if (h4!=null&&h4.getAttribute("FpCellType")!=null)return h4.getAttribute("FpCellType");
return "text";
}
this.GetCellType2=function (h4){
if (h4!=null&&h4.getAttribute("FpRef")!=null){
h4=document.getElementById(h4.getAttribute("FpRef"));
}
var s4=null;
if (h4!=null){
s4=h4.getAttribute("FpCellType");
if (s4=="readonly")s4=h4.getAttribute("CellType");
if (s4==null&&h4.getAttribute("CellType2")=="TagCloudCellType")
s4=h4.getAttribute("CellType2");
}
if (s4!=null)return s4;
return "text";
}
this.GetCellEditorID=function (e4,h4){
if (h4!=null&&h4.getAttribute("FpRef")!=null){
var g0=document.getElementById(h4.getAttribute("FpRef"));
return g0.getAttribute("FpEditorID");
}
if (h4.getAttribute("FpEditorID")!=null)
return h4.getAttribute("FpEditorID");
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
for (var f1=0;f1<this.c0.length;f1++){
var s5=this.c0[f1];
if (s5.id==editorID){
a8=s5.a8;
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
this.GetCellValidatorID=function (e4,h4){
return null;
}
this.GetCellValidator=function (e4,validatorID){
return null;
}
this.GetTableRow=function (e4,h1){
var f7=this.GetData(e4).getElementsByTagName("root")[0];
var f6=f7.getElementsByTagName("data")[0];
var g0=f6.firstChild;
while (g0!=null){
if (g0.getAttribute("key")==""+h1)return g0;
g0=g0.nextSibling;
}
return null;
}
this.GetTableCell=function (i9,h3){
if (i9==null)return null;
var g0=i9.firstChild;
while (g0!=null){
if (g0.getAttribute("key")==""+h3)return g0;
g0=g0.nextSibling;
}
return null;
}
this.AddTableRow=function (e4,h1){
if (h1==null)return null;
var n8=this.GetTableRow(e4,h1);
if (n8!=null)return n8;
var f7=this.GetData(e4).getElementsByTagName("root")[0];
var f6=f7.getElementsByTagName("data")[0];
if (document.all!=null){
n8=this.GetData(e4).createNode("element","row","");
}else {
n8=document.createElement("row");
n8.style.display="none";
}
n8.setAttribute("key",h1);
f6.appendChild(n8);
return n8;
}
this.AddTableCell=function (i9,h3){
if (i9==null)return null;
var n8=this.GetTableCell(i9,h3);
if (n8!=null)return n8;
if (document.all!=null){
n8=this.GetData(e4).createNode("element","cell","");
}else {
n8=document.createElement("cell");
n8.style.display="none";
}
n8.setAttribute("key",h3);
i9.appendChild(n8);
return n8;
}
this.GetCellValue=function (e4,h4){
if (h4==null)return null;
var h1=this.GetRowKeyFromCell(e4,h4);
var h3=e4.getAttribute("LayoutMode")?this.GetColKeyFromCell2(e4,h4):this.GetColKeyFromCell(e4,h4);
var s6=this.AddTableCell(this.AddTableRow(e4,h1),h3);
return s6.innerHTML;
}
this.HTMLEncode=function (s){
var s7=new String(s);
var s8=new RegExp("&","g");
s7=s7.replace(s8,"&amp;");
s8=new RegExp("<","g");
s7=s7.replace(s8,"&lt;");
s8=new RegExp(">","g");
s7=s7.replace(s8,"&gt;");
s8=new RegExp("\"","g");
s7=s7.replace(s8,"&quot;");
return s7;
}
this.HTMLDecode=function (s){
var s7=new String(s);
var s8=new RegExp("&amp;","g");
s7=s7.replace(s8,"&");
s8=new RegExp("&lt;","g");
s7=s7.replace(s8,"<");
s8=new RegExp("&gt;","g");
s7=s7.replace(s8,">");
s8=new RegExp("&nbsp;","g");
s7=s7.replace(s8," ");
s8=new RegExp("&quot;","g");
s7=s7.replace(s8,'"');
return s7;
}
this.SetCellValue=function (e4,h4,val,noEvent,recalc){
if (h4==null)return ;
var s9=this.GetCellType(h4);
if (s9=="readonly")return ;
var h1=this.GetRowKeyFromCell(e4,h4);
var h3=e4.getAttribute("LayoutMode")?this.GetColKeyFromCell2(e4,h4):this.GetColKeyFromCell(e4,h4);
var s6=this.AddTableCell(this.AddTableRow(e4,h1),h3);
val=this.HTMLEncode(val);
val=this.HTMLEncode(val);
s6.innerHTML=val;
if (!noEvent){
var g3=this.CreateEvent("DataChanged");
g3.cell=h4;
g3.cellValue=val;
g3.row=h1;
g3.col=h3;
this.FireEvent(e4,g3);
}
var f9=this.GetCmdBtn(e4,"Update");
if (f9!=null&&f9.getAttribute("disabled")!=null)
this.UpdateCmdBtnState(f9,false);
f9=this.GetCmdBtn(e4,"Cancel");
if (f9!=null&&f9.getAttribute("disabled")!=null)
this.UpdateCmdBtnState(f9,false);
e4.e2=true;
if (recalc){
this.UpdateValues(e4);
}
}
this.GetSelectedRanges=function (e4){
var n7=this.GetSelection(e4);
var g5=new Array();
var n8=n7.firstChild;
while (n8!=null){
var h9=new this.Range();
this.GetRangeFromNode(e4,n8,h9);
var g0=g5.length;
g5.length=g0+1;
g5[g0]=h9;
n8=n8.nextSibling;
}
return g5;
}
this.GetSelectedRange=function (e4){
var h9=new this.Range();
var n7=this.GetSelection(e4);
var n8=n7.lastChild;
if (n8!=null){
this.GetRangeFromNode(e4,n8,h9);
}
return h9;
}
this.GetRangeFromNode=function (e4,n8,h9){
if (n8==null||e4==null||h9==null)return ;
var h1;
var h3;
if (e4.getAttribute("LayoutMode")){
h1=parseInt(n8.getAttribute("rowIndex"));
h3=parseInt(n8.getAttribute("colIndex"));
}
else {
h1=this.GetRowByKey(e4,n8.getAttribute("row"));
h3=this.GetColByKey(e4,n8.getAttribute("col"));
}
var n3=parseInt(n8.getAttribute("rowcount"));
var h0=parseInt(n8.getAttribute("colcount"));
var i6=this.GetViewport(e4);
if (i6!=null){
var t0=this.GetDisplayIndex(e4,h1);
for (var f1=t0;f1<t0+n3;f1++){
if (this.IsChildSpreadRow(e4,i6,f1))n3--;
}
}
var t1;
if (e4.getAttribute("LayoutMode")){
var n6=parseInt(n8.getAttribute("col"));
if (n6!=-1&&parseInt(n8.getAttribute("row"))==-1&&n3==-1){
h1=parseInt(n8.getAttribute("row"));
t1=parseInt(n8.getAttribute("rowIndex"));
}
}
var t2=null;
if (h1<0&&h3<0&&n3!=0&&h0!=0)
t2="Table";
else if (h1<0&&h3>=0&&h0>0)
t2="Column";
else if (h3<0&&h1>=0&&n3>0)
t2="Row";
else 
t2="Cell";
this.SetRange(h9,t2,h1,h3,n3,h0,t1);
}
this.GetSelection=function (e4){
var f6=this.GetData(e4);
var f7=f6.getElementsByTagName("root")[0];
var o1=f7.getElementsByTagName("state")[0];
var t3=o1.getElementsByTagName("selection")[0];
return t3;
}
this.GetRowKeyFromRow=function (e4,h1){
if (h1<0)return null;
var e7=null;
if (h1<e4.frzRows){
e7=this.GetViewport0(e4);
if (e7==null)e7=this.GetViewport1(e4);
}else {
e7=this.GetViewport2(e4);
if (e7==null)e7=this.GetViewport(e4);
}
if (h1>=e4.frzRows){
h1-=e4.frzRows;
}
return e7.rows[h1].getAttribute("FpKey");
}
this.GetColKeyFromCol=function (e4,h3){
if (h3<0)return null;
var e7=null;
if (h3>=e4.frzCols){
e7=this.GetViewport1(e4);
if (e7==null)e7=this.GetViewport(e4);
if (e7==null)e7=this.GetColHeader(e4);
}else {
e7=this.GetViewport0(e4);
if (e7==null)e7=this.GetViewport2(e4);
if (e7==null)e7=this.GetFrozColHeader(e4);
}
if (h3>=e4.frzCols)
h3=h3-e4.frzCols;
var f3=this.GetColGroup(e7);
if (f3!=null&&h3>=0&&h3<f3.childNodes.length){
return f3.childNodes[h3].getAttribute("FpCol");
}
return null;
}
this.GetRowKeyFromCell=function (e4,h4){
var h1=h4.parentNode.getAttribute("FpKey");
return h1;
}
this.GetColKeyFromCell=function (e4,h4){
var n6=this.GetColFromCell(e4,h4);
if (n6>=e4.frzCols){
e7=this.GetViewport(e4);
if (e7==null||!this.IsChild(h4,e7))e7=this.GetViewport1(e4);
var f3=this.GetColGroup(e7);
if (f3!=null&&n6-e4.frzCols>=0&&n6-e4.frzCols<f3.childNodes.length){
return f3.childNodes[n6-e4.frzCols].getAttribute("FpCol");
}
}else {
e7=this.GetViewport0(e4);
if (e7==null||!this.IsChild(h4,e7))e7=this.GetViewport2(e4);
var f3=this.GetColGroup(e7);
if (f3!=null&&n6>=0&&n6<f3.childNodes.length){
return f3.childNodes[n6].getAttribute("FpCol");
}
}
}
this.GetRowTemplateRowFromGroupCell=function (e4,h4,isColHeader){
var t4=this.GetColCount(e4);
var c6=this.GetColHeader(e4);
if ((!e4.allowGroup||isColHeader)&&c6!=null){
for (var f1=0;f1<c6.rows.length;f1++){
for (var i1=0;i1<t4;i1++){
var t5=c6.rows[f1].cells[i1];
var t6=isNaN(h4)?parseInt(h4.getAttribute("col")):h4;
if (t5!=null&&h4!=null&&parseInt(t5.getAttribute("col"))==t6)
return f1;
}
}
}
var n2=this.GetViewport1(e4);
if (n2!=null){
for (var i9=0;i9<n2.rows.length;i9++){
for (var n6=0;n6<n2.rows[i9].cells.length;n6++){
var t7=isNaN(h4)?parseInt(h4.getAttribute("col")):h4;
if (parseInt(n2.rows[i9].cells[n6].getAttribute("col"))==t7&&n2.rows[i9].cells[n6].getAttribute("group")==null)
return parseInt(n2.rows[i9].getAttribute("FpKey"));
}
}
}
var n4=this.GetViewport(e4);
if (n4!=null){
for (var i9=0;i9<n4.rows.length;i9++){
for (var n6=0;n6<n4.rows[i9].cells.length;n6++){
var t7=isNaN(h4)?parseInt(h4.getAttribute("col")):h4;
if (parseInt(n4.rows[i9].cells[n6].getAttribute("col"))==t7&&n4.rows[i9].cells[n6].getAttribute("group")==null)
return parseInt(n4.rows[i9].getAttribute("FpKey"));
}
}
}
return -1;
}
this.GetColTemplateRowFromGroupCell=function (e4,colIndex){
var t4=this.GetColCount(e4);
var c6=this.GetColHeader(e4);
var t8=this.GetRowTemplateRowFromGroupCell(e4,colIndex);
var h4=null
if (c6==null)return -1;
for (var f1=0;f1<c6.rows.length;f1++){
for (var i1=0;i1<t4;i1++){
var t5=c6.rows[f1].cells[i1];
if (t5!=null&&parseInt(t5.getAttribute("col"))==colIndex){
h4=t5;
break ;
}
}
}
return this.GetColFromCell(e4,h4);
}
this.GetColKeyFromCell2=function (e4,h4){
if (!h4)return -1;
if (h4.getAttribute("col"))
return h4.getAttribute("col")=="-1"?0:parseInt(h4.getAttribute("col"));
else 
return this.GetColKeyFromCell(e4,h4);
}
this.GetColKeyFromCol2=function (e4,i9,n6){
var h4=this.GetCellFromRowCol(e4,i9,n6);
if (h4)
return this.GetColKeyFromCell2(e4,h4);
return n6;
}
this.GetCellByRowCol2=function (e4,h1,h3){
if (h1==null||h3==null||h1.length<=0||h1=="-1"||h3.length<=0||h3=="-1")
return null;
var f4=this.GetViewport1(e4);
if (f4!=null){
for (var i9=0;i9<f4.rows.length;i9++){
if (f4.rows[i9].getAttribute("FpKey")==h1){
for (var n6=0;n6<f4.rows[i9].cells.length;n6++){
if (f4.rows[i9].cells[n6].getAttribute("col")==h3)
return f4.rows[i9].cells[n6];
}
}
}
}
var n4=this.GetViewport(e4);
if (n4!=null){
for (var i9=0;i9<n4.rows.length;i9++){
if (n4.rows[i9].getAttribute("FpKey")==h1){
for (var n6=0;n6<n4.rows[i9].cells.length;n6++){
if (n4.rows[i9].cells[n6].getAttribute("col")==h3)
return n4.rows[i9].cells[n6];
}
}
}
}
return null;
}
this.GetRowTemplateRowFromCell=function (e4,h4){
if (!h4)return -1;
try {
var h1
if (h4.getAttribute("group")!=null)
h1=this.GetRowTemplateRowFromGroupCell(e4,h4);
else 
h1=parseInt(h4.parentNode.getAttribute("row"));
return h1;
}
catch (g3){
return -1;
}
}
this.PaintMultipleRowSelection=function (e4,h1,h3,n3,h0,select){
var t9=this.GetRowCountInternal(e4);
var t4=this.GetColCount(e4);
var u0=true;
for (var f1=h1;f1<t9;f1++){
if (this.IsChildSpreadRow(e4,this.GetViewport(e4),f1))continue ;
var h4=null;
for (var i1=h3;i1<h3+h0&&i1<t4;i1++){
if (this.IsCovered(e4,f1,i1,e4.d9))continue ;
h4=this.GetCellFromRowCol(e4,f1,i1,h4);
if (h4!=null&&parseInt(h4.parentNode.getAttribute("row"))==h1){
this.PaintViewportSelection(e4,f1,i1,n3,h0,select);
if (this.GetColHeader(e4)!=null&&this.GetOperationMode(e4)=="Normal"&&u0)this.PaintHeaderSelection(e4,f1,i1,n3,h0,select,true);
if (this.GetRowHeader(e4)!=null)this.PaintHeaderSelection(e4,f1,i1,n3,h0,select,false);
u0=false;
}
}
}
this.PaintAnchorCell(e4);
}
this.GetFirstRowFromKey=function (e4,rowKey){
var f4=this.GetViewport1(e4)
if (f4!=null){
for (var i9=0;i9<f4.rows.length;i9++){
if (f4.rows[i9].getAttribute("FpKey")==rowKey){
return i9;
}
}
}
var n4=this.GetViewport(e4)
if (n4!=null){
for (var i9=0;i9<n4.rows.length;i9++){
if (n4.rows[i9].getAttribute("FpKey")==rowKey){
return (e4.frzRows!=null)?e4.frzRows+i9:i9;
}
}
}
return null;
}
this.GetFirstMultiRowFromViewport=function (e4,i9,isColHeader){
var e7=null;
var u1=null;
if (i9<e4.frzRows)
e7=this.GetViewport1(e4);
else 
e7=this.GetViewport(e4);
if (!isColHeader)
u1=this.GetRowKeyFromRow(e4,i9);
var u2=parseInt(e4.getAttribute("layoutrowcount"));
var u3;
for (var f1=0;f1<e7.rows.length;f1++){
u3=0;
if (u1!=null){
if (e7.rows[f1].getAttribute("FpKey")==u1)
return ((e4.frzRows!=null&&i9<e4.frzRows)?f1:f1+e4.frzRows);
}
else {
for (var i1=f1+1;i1<e7.rows.length;i1++){
if (e7.rows[f1]!=null&&e7.rows[i1]!=null&&e7.rows[f1].getAttribute("FpKey")==e7.rows[i1].getAttribute("FpKey"))
u3++;
if (u3==(u2-1))
return f1;
}
}
}
}
this.GetRowFromViewPort=function (e4,h1,h3){
if (h1<0||h3<0)return null;
var e7=null;
if (h1<e4.frzRows)
e7=this.GetViewport1(e4);
else 
e7=this.GetViewport(e4);
if (h1>=0&&h1<e7.rows.length){
for (var f1=0;f1<e7.rows.length;f1++){
if (e7.rows[f1].getAttribute("row")!=null&&parseInt(e7.rows[f1].getAttribute("row"))==h1)
return f1;
}
}
return 0;
}
this.GetDisplayIndex2=function (e4,t8){
if (!e4.allowGroup)
return (t8!=null)?this.GetDisplayIndex(e4,t8):0;
else {
var f4=this.GetViewport1(e4);
if (f4!=null){
for (var i9=t8;i9<f4.rows.length;i9++){
if (f4.rows(i9).getAttribute("row")==t8){
return i9;
}
}
}
var n4=this.GetViewport(e4);
if (n4!=null){
for (var i9=t8;i9<n4.rows.length;i9++){
if (IsChildSpreadRow(n4,i9))continue ;
if (n4.rows(i9).getAttribute("row")==t8){
return i9;
}
}
}
}
return -1;
}
this.SetSelection=function (e4,i9,n6,rowcount,colcount,addSelection,rowIndex2,colIndex2){
if (!e4.initialized)return ;
var t8=i9;
var u4=(colIndex2==null)?n6:colIndex2;
if (i9!=null&&parseInt(i9)>=0){
i9=this.GetRowKeyFromRow(e4,i9);
if (i9!="newRow")
i9=parseInt(i9);
}
if (n6!=null&&parseInt(n6)>=0){
if (e4.getAttribute("LayoutMode"))
n6=parseInt(this.GetColKeyFromCol2(e4,t8,n6));
else 
n6=parseInt(this.GetColKeyFromCol(e4,n6));
}
if (e4.getAttribute("LayoutMode")&&rowIndex2!=null)
t8=rowIndex2;
var n8=this.GetSelection(e4);
if (n8==null)return ;
if (addSelection==null)
addSelection=(e4.getAttribute("multiRange")=="true"&&!this.a6);
var u5=n8.lastChild;
if (u5==null||addSelection){
if (document.all!=null){
u5=this.GetData(e4).createNode("element","range","");
}else {
u5=document.createElement('range');
u5.style.display="none";
}
n8.appendChild(u5);
}
u5.setAttribute("row",i9);
u5.setAttribute("col",n6);
u5.setAttribute("rowcount",rowcount);
u5.setAttribute("colcount",colcount);
u5.setAttribute("rowIndex",t8);
u5.setAttribute("colIndex",u4);
e4.e2=true;
this.PaintFocusRect(e4);
var f9=this.GetCmdBtn(e4,"Update");
this.UpdateCmdBtnState(f9,false);
var g3=this.CreateEvent("SelectionChanged");
this.FireEvent(e4,g3);
}
this.CreateSelectionNode=function (e4,i9,n6,rowcount,colcount,t8,u4){
var u5=document.createElement('range');
u5.style.display="none";
u5.setAttribute("row",i9);
u5.setAttribute("col",n6);
u5.setAttribute("rowcount",rowcount);
u5.setAttribute("colcount",colcount);
u5.setAttribute("rowIndex",t8);
u5.setAttribute("colIndex",u4);
return u5;
}
this.AddRowToSelection=function (e4,n8,i9){
var n9=this.GetOperationMode(e4);
if (e4.getAttribute("LayoutMode")&&(n9=="ExtendedSelect"||n9=="MultiSelect"))return ;
var t8=i9;
if (typeof(i9)!="undefined"&&parseInt(i9)>=0){
i9=this.GetRowKeyFromRow(e4,i9);
if (i9!="newRow")
i9=parseInt(i9);
}
if (!this.IsRowSelected(e4,i9)&&!isNaN(i9))
{
var u5=this.CreateSelectionNode(e4,i9,-1,1,-1,t8,-1);
n8.appendChild(u5);
}
}
this.RemoveSelection=function (e4,i9,n6,rowcount,colcount){
var n8=this.GetSelection(e4);
if (n8==null)return ;
var u5=n8.firstChild;
while (u5!=null){
var h1;
var n3;
var n9=this.GetOperationMode(e4);
if (e4.getAttribute("LayoutMode")&&(n9=="ExtendedSelect"||n9=="MultiSelect")){
var o0=parseInt(u5.getAttribute("row"));
h1=this.GetFirstRowFromKey(e4,o0);
}
else 
h1=parseInt(u5.getAttribute("rowIndex"));
if (e4.getAttribute("LayoutMode")&&(n9=="ExtendedSelect"||n9=="MultiSelect"))
n3=parseInt(e4.getAttribute("layoutrowcount"));
else 
n3=parseInt(u5.getAttribute("rowcount"));
if (h1<=i9&&i9<h1+n3){
n8.removeChild(u5);
for (var f1=h1;f1<h1+n3;f1++){
if (f1!=i9){
this.AddRowToSelection(e4,n8,f1);
}
}
break ;
}
u5=u5.nextSibling;
}
e4.e2=true;
var f9=this.GetCmdBtn(e4,"Update");
this.UpdateCmdBtnState(f9,false);
var g3=this.CreateEvent("SelectionChanged");
this.FireEvent(e4,g3);
}
this.GetColInfo=function (e4,h3){
var f6=this.GetData(e4);
var f7=f6.getElementsByTagName("root")[0];
var o1=f7.getElementsByTagName("state")[0];
var n6=o1.getElementsByTagName("colinfo")[0];
var g0=n6.firstChild;
while (g0!=null){
if (g0.getAttribute("key")==""+h3)return g0;
g0=g0.nextSibling;
}
return null;
}
this.GetColWidthFromCol=function (e4,h3){
var f3=this.GetColGroup(this.GetViewport(e4));
return parseInt(f3.childNodes[h3].width);
}
this.GetColWidth=function (colInfo){
if (colInfo==null)return null;
var n8=colInfo.getElementsByTagName("width")[0];
if (n8!=null)return n8.innerHTML;
return 0;
}
this.AddColInfo=function (e4,h3){
var n8=this.GetColInfo(e4,h3);
if (n8!=null)return n8;
var f6=this.GetData(e4);
var f7=f6.getElementsByTagName("root")[0];
var o1=f7.getElementsByTagName("state")[0];
var n6=o1.getElementsByTagName("colinfo")[0];
if (document.all!=null){
n8=this.GetData(e4).createNode("element","col","");
}else {
n8=document.createElement('col');
n8.style.display="none";
}
n8.setAttribute("key",h3);
n6.appendChild(n8);
return n8;
}
this.SetColWidth=function (e4,n6,width,oldWidth){
if (n6==null)return ;
n6=parseInt(n6);
var j6=this.IsXHTML(e4);
var u6=null;
if (n6<e4.frzCols){
if (this.GetViewport0(e4)!=null){
var f3=this.GetColGroup(this.GetViewport0(e4));
if (f3==null||f3.childNodes.length==0){
f3=this.GetColGroup(this.GetFrozColHeader(e4));
}
u6=this.AddColInfo(e4,f3.childNodes[n6].getAttribute("FpCol"));
if (width==0)width=1;
if (f3!=null){
if (oldWidth==null)oldWidth=f3.childNodes[n6].width;
f3.childNodes[n6].width=width;
}
this.SetWidthFix(this.GetViewport0(e4),n6,width);
}
if (this.GetFrozColFooter(e4)!=null){
var f3=this.GetColGroup(this.GetFrozColFooter(e4));
if (f3==null||f3.childNodes.length==0){
f3=this.GetColGroup(this.GetFrozColHeader(e4));
}
if (width==0)width=1;
if (f3!=null){
if (oldWidth==null)oldWidth=f3.childNodes[n6].width;
f3.childNodes[n6].width=width;
}
this.SetWidthFix(this.GetFrozColFooter(e4),n6,width);
}
if (this.GetViewport2(e4)!=null){
var f3=this.GetColGroup(this.GetViewport2(e4));
if (f3==null||f3.childNodes.length==0){
f3=this.GetColGroup(this.GetFrozColHeader(e4));
}
u6=this.AddColInfo(e4,f3.childNodes[n6].getAttribute("FpCol"));
if (width==0)width=1;
if (f3!=null){
if (oldWidth==null)oldWidth=f3.childNodes[n6].width;
f3.childNodes[n6].width=width;
}
this.SetWidthFix(this.GetViewport2(e4),n6,width);
}
if (this.GetFrozColHeader(e4)!=null){
var u7=parseInt(this.GetFrozColHeader(e4).parentNode.parentNode.style.width);
this.GetFrozColHeader(e4).parentNode.parentNode.style.width=(u7+width-oldWidth)+"px";
if (this.GetViewport(e4)!=null){
if (this.GetViewport(e4).cellSpacing=="0"&&this.GetColCount(e4)>1&&this.GetViewport(e4).rules!="rows"){
if (j6){
if (n6==this.colCount-1)width-=1;
}
}
}
if (width<=0)width=1;
document.getElementById(e4.id+"col"+n6).width=width;
this.SetWidthFix(this.GetFrozColHeader(e4),n6,width);
if (this.GetViewport(e4)!=null){
if (this.GetViewport(e4).cellSpacing=="0"&&this.GetColCount(e4)>1&&this.GetViewport(e4).rules!="rows"){
if (n6==this.GetColCount(e4)-1)width+=1;
}
}
}
}else {
if (this.GetViewport1(e4)!=null){
var f3=this.GetColGroup(this.GetViewport1(e4));
if (f3==null||f3.childNodes.length==0){
f3=this.GetColGroup(this.GetColHeader(e4));
}
u6=this.AddColInfo(e4,f3.childNodes[n6-e4.frzCols].getAttribute("FpCol"));
if (this.GetViewport1(e4).cellSpacing=="0"&&this.GetColCount(e4)>1&&this.GetViewport1(e4).rules!="rows"){
if (n6==0)width-=1;
}
if (width==0)width=1;
if (f3!=null)
f3.childNodes[n6-e4.frzCols].width=width;
this.SetWidthFix(this.GetViewport1(e4),n6-e4.frzCols,width);
var c6=this.GetColHeader(e4);
var j9=this.GetColGroup(this.GetViewport(e4));
var k0=this.GetColGroup(c6);
if (j9!=null&&j9.childNodes.length>0&&k0!=null&&k0.childNodes.length>0){
f3=this.GetColGroup(this.GetColHeader(e4));
if (f3!=null){
if (n6==e4.frzCols&&e4.frzCols>0)
width=width+j9.childNodes[0].offsetLeft;
f3.childNodes[n6-e4.frzCols].width=width;
}
}
this.SetWidthFix(this.GetColHeader(e4),n6-e4.frzCols,width);
}
if (this.GetViewport(e4)!=null){
var f3=this.GetColGroup(this.GetViewport(e4));
if (f3==null||f3.childNodes.length==0){
f3=this.GetColGroup(this.GetColHeader(e4));
}
u6=this.AddColInfo(e4,f3.childNodes[n6-e4.frzCols].getAttribute("FpCol"));
if (this.GetViewport(e4).cellSpacing=="0"&&this.GetColCount(e4)>1&&this.GetViewport(e4).rules!="rows"){
if (n6==0)width-=1;
}
if (width==0)width=1;
if (f3!=null)
f3.childNodes[n6-e4.frzCols].width=width;
this.SetWidthFix(this.GetViewport(e4),n6-e4.frzCols,width);
}
if (this.GetColHeader(e4)!=null){
if (this.GetViewport(e4)!=null){
if (this.GetViewport(e4).cellSpacing=="0"&&this.GetColCount(e4)>1&&this.GetViewport(e4).rules!="rows"){
if (n6==e4.frzCols&&e4.frzCols>0)width-=1;
if (n6==this.colCount-1)width-=1;
}
}
if (width<=0)width=1;
document.getElementById(e4.id+"col"+n6).width=width;
this.SetWidthFix(this.GetColHeader(e4),n6-e4.frzCols,width);
if (this.GetViewport(e4)!=null){
if (this.GetViewport(e4).cellSpacing=="0"&&this.GetColCount(e4)>1&&this.GetViewport(e4).rules!="rows"){
if (n6==this.GetColCount(e4)-1)width+=1;
}
}
}
if (this.GetColFooter(e4)!=null){
var f3=this.GetColGroup(this.GetColFooter(e4));
if (f3==null||f3.childNodes.length==0){
f3=this.GetColGroup(this.GetColHeader(e4));
}
u6=this.AddColInfo(e4,f3.childNodes[n6-e4.frzCols].getAttribute("FpCol"));
if (this.GetColFooter(e4).cellSpacing=="0"&&this.GetColCount(e4)>1&&this.GetColFooter(e4).rules!="rows"){
if (n6==0)width-=1;
}
if (width==0)width=1;
if (f3!=null)
f3.childNodes[n6-e4.frzCols].width=width;
this.SetWidthFix(this.GetColFooter(e4),n6-e4.frzCols,width);
}
}
var e9=this.GetTopSpread(e4);
this.SizeAll(e9);
this.Refresh(e9);
if (n6<e4.frzCols&&this.GetFrozColHeader(e4)!=null){
var u7=parseInt(this.GetFrozColHeader(e4).parentNode.parentNode.style.width);
this.GetFrozColHeader(e4).parentNode.parentNode.style.width=(u7+width-oldWidth)+"px";
var u8=this.GetColGroup(this.GetTopTable(e4));
if (u8!=null){
var u9=this.GetFrozColHeader(e4).parentNode.parentNode.cellIndex;
var u7=parseInt(u8.childNodes[1].width);
u8.childNodes[u9].width=(u7+width-oldWidth)+"px";
}
}
if (u6!=null){
var n8=u6.getElementsByTagName("width");
if (n8!=null&&n8.length>0)
n8[0].innerHTML=width;
else {
if (document.all!=null){
n8=this.GetData(e4).createNode("element","width","");
}else {
n8=document.createElement('width');
n8.style.display="none";
}
u6.appendChild(n8);
n8.innerHTML=width;
}
}
var f9=this.GetCmdBtn(e4,"Update");
if (f9!=null)this.UpdateCmdBtnState(f9,false);
e4.e2=true;
}
this.SetWidthFix=function (e7,n6,width){
if (e7==null||e7.rows.length==0)return ;
var f1=0;
var v0=0;
var j2=e7.rows[0].cells[0];
var v1=j2.colSpan;
if (v1==null)v1=1;
while (n6>=v0+v1){
f1++;
v0=v0+v1;
j2=e7.rows[0].cells[f1];
v1=j2.colSpan;
if (v1==null)v1=1;
}
j2.width=width;
}
this.GetRowInfo=function (e4,h1){
var f6=this.GetData(e4);
var f7=f6.getElementsByTagName("root")[0];
var o1=f7.getElementsByTagName("state")[0];
var i9=o1.getElementsByTagName("rowinfo")[0];
var g0=i9.firstChild;
while (g0!=null){
if (g0.getAttribute("key")==""+h1)return g0;
g0=g0.nextSibling;
}
return null;
}
this.GetRowHeight=function (r2){
if (r2==null)return null;
var v2=r2.getElementsByTagName("height");
if (v2!=null&&v2.length>0)return v2[0].innerHTML;
return 0;
}
this.AddRowInfo=function (e4,h1){
var n8=this.GetRowInfo(e4,h1);
if (n8!=null)return n8;
var f6=this.GetData(e4);
var f7=f6.getElementsByTagName("root")[0];
var o1=f7.getElementsByTagName("state")[0];
var i9=o1.getElementsByTagName("rowinfo")[0];
if (document.all!=null){
n8=this.GetData(e4).createNode("element","row","");
}else {
n8=document.createElement('row');
n8.style.display="none";
}
n8.setAttribute("key",h1);
i9.appendChild(n8);
return n8;
}
this.GetTopSpread=function (g3)
{
if (g3==null)return null;
var g5=this.GetSpread(g3);
if (g5==null)return null;
var g0=g5.parentNode;
while (g0!=null&&g0.tagName!="BODY")
{
if (g0.getAttribute("FpSpread")=="Spread"){
if (g0.getAttribute("hierView")=="true")
g5=g0;
else 
break ;
}
g0=g0.parentNode;
}
return g5;
}
this.GetParentSpread=function (e4)
{
var v3=e4.getAttribute("parentSpread");
if (v3!=null){
if (v3.length<=0)
return null;
else 
return document.getElementById(v3);
}
else {
try {
var g0=e4.parentNode;
while (g0!=null&&g0.getAttribute("FpSpread")!="Spread")g0=g0.parentNode;
if (g0!=null&&g0.getAttribute("hierView")=="true"){
e4.setAttribute("parentSpread",g0.id);
return g0;
}
else {
e4.setAttribute("parentSpread","");
return null;
}
}catch (g3){
e4.setAttribute("parentSpread","");
return null;
}
}
}
this.SetRowHeight=function (e4,r2,height){
if (r2==null)return ;
var n8=r2.getElementsByTagName("height");
if (n8!=null&&n8.length>0)
n8[0].innerHTML=height;
else {
if (document.all!=null){
n8=this.GetData(e4).createNode("element","height","");
}else {
n8=document.createElement('height');
n8.style.display="none";
}
r2.appendChild(n8);
n8.innerHTML=height;
}
var f9=this.GetCmdBtn(e4,"Update");
if (f9!=null)this.UpdateCmdBtnState(f9,false);
e4.e2=true;
}
this.SetActiveRow=function (e4,i9){
if (this.GetRowCount(e4)<1)return ;
if (i9==null)i9=-1;
var f6=this.GetData(e4);
var f7=f6.getElementsByTagName("root")[0];
var o1=f7.getElementsByTagName("state")[0];
var o2=o1.firstChild;
while (o2!=null&&o2.tagName!="activerow"&&o2.tagName!="ACTIVEROW"){
o2=o2.nextSibling;
}
if (o2!=null)
o2.innerHTML=""+i9;
if (i9!=null&&e4.getAttribute("IsNewRow")!="true"&&e4.getAttribute("AllowInsert")=="true"){
var f9=this.GetCmdBtn(e4,"Insert");
this.UpdateCmdBtnState(f9,false);
f9=this.GetCmdBtn(e4,"Add");
this.UpdateCmdBtnState(f9,false);
}else {
var f9=this.GetCmdBtn(e4,"Insert");
this.UpdateCmdBtnState(f9,true);
f9=this.GetCmdBtn(e4,"Add");
this.UpdateCmdBtnState(f9,true);
}
if (i9!=null&&e4.getAttribute("IsNewRow")!="true"&&(e4.getAttribute("AllowDelete")==null||e4.getAttribute("AllowDelete")=="true")){
var f9=this.GetCmdBtn(e4,"Delete");
this.UpdateCmdBtnState(f9,(i9==-1));
}else {
var f9=this.GetCmdBtn(e4,"Delete");
this.UpdateCmdBtnState(f9,true);
}
e4.e2=true;
}
this.SetActiveCol=function (e4,n6){
var f6=this.GetData(e4);
var f7=f6.getElementsByTagName("root")[0];
var o1=f7.getElementsByTagName("state")[0];
var o3=o1.firstChild;
while (o3!=null&&o3.tagName!="activecolumn"&&o3.tagName!="ACTIVECOLUMN"){
o3=o3.nextSibling;
}
if (o3!=null)
o3.innerHTML=""+parseInt(n6);
e4.e2=true;
}
this.GetEditor=function (h4){
if (h4==null)return null;
var s9=this.GetCellType(h4);
if (s9=="readonly")return null;
var i4=h4.getElementsByTagName("DIV");
if (s9=="MultiColumnComboBoxCellType"){
if (i4!=null&&i4.length>0){
var g0=i4[0];
g0.type="div";
return g0;
}
}
i4=h4.getElementsByTagName("INPUT");
if (i4!=null&&i4.length>0){
var g0=i4[0];
while (g0!=null&&g0.getAttribute&&g0.getAttribute("FpEditor")==null)
g0=g0.parentNode;
if (!g0.getAttribute)g0=null;
return g0;
}
i4=h4.getElementsByTagName("SELECT");
if (i4!=null&&i4.length>0){
var g0=i4[0];
return g0;
}
return null;
}
this.GetPageActiveSpread=function (){
var v4=document.documentElement.getAttribute("FpActiveSpread");
var g0=null;
if (v4!=null)g0=document.getElementById(v4);
return g0;
}
this.GetPageActiveSheetView=function (){
var v4=document.documentElement.getAttribute("FpActiveSheetView");
var g0=null;
if (v4!=null)g0=document.getElementById(v4);
return g0;
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
var f0=the_fpSpread.spreads.length;
for (var f1=0;f1<f0;f1++){
if (the_fpSpread.spreads[f1]!=null)the_fpSpread.SizeSpread(the_fpSpread.spreads[f1]);
}
}
this.DblClick=function (event){
var h4=this.GetCell(this.GetTarget(event),true,event);
var e4=this.GetSpread(h4);
if (h4!=null&&!this.IsHeaderCell(h4)&&this.GetOperationMode(e4)=="RowMode"&&this.GetEnableRowEditTemplate(e4)=="true"&&!e4.getAttribute("LayoutMode")){
var v5=h4.getElementsByTagName("DIV");
if (v5!=null&&v5.length>0&&v5[0].id==e4.id+"_RowEditTemplateContainer")return ;
this.Edit(e4,this.GetRowKeyFromCell(e4,h4));
var f9=this.GetCmdBtn(e4,"Cancel");
if (f9!=null)
this.UpdateCmdBtnState(f9,false);
return ;
}
if (h4!=null&&!this.IsHeaderCell(h4)&&h4==e4.d1)this.StartEdit(e4,h4);
}
this.GetEvent=function (g3){
if (g3!=null)return g3;
return window.event;
}
this.GetTarget=function (g3){
g3=this.GetEvent(g3);
if (g3.target==document){
if (g3.currentTarget!=null)return g3.currentTarget;
}
if (g3.target!=null)return g3.target;
return g3.srcElement;
}
this.StartEdit=function (e4,editCell){
var v6=this.GetOperationMode(e4);
if (v6=="SingleSelect"||v6=="ReadOnly"||this.a7)return ;
if (v6=="RowMode"&&this.GetEnableRowEditTemplate(e4)=="true"&&!e4.getAttribute("LayoutMode"))return ;
var h4=editCell;
if (h4==null)h4=e4.d1;
if (h4==null)return ;
this.b1=-1;
var i4=this.GetEditor(h4);
if (i4!=null){
this.a7=true;
this.a8=i4;
this.b1=1;
}
var j6=this.IsXHTML(e4);
if (h4!=null){
var h1=this.GetRowFromCell(e4,h4);
var h3=this.GetColFromCell(e4,h4);
var g3=this.CreateEvent("EditStart");
g3.cell=h4;
g3.row=this.GetSheetIndex(e4,h1);
g3.col=h3;
g3.cancel=false;
this.FireEvent(e4,g3);
if (g3.cancel)return ;
var s9=this.GetCellType(h4);
if (s9=="readonly")return ;
if (e4.d1!=h4){
e4.d1=h4;
this.SetActiveRow(e4,this.GetRowKeyFromCell(e4,h4));
this.SetActiveCol(e4,e4.getAttribute("LayoutMode")?this.GetColKeyFromCell2(h4):this.GetColKeyFromCell(e4,h4));
}
if (i4==null){
var j4=this.GetRender(h4);
var v7=this.GetValueFromRender(e4,j4);
if (v7==" ")v7="";
this.a9=v7;
this.b0=this.GetFormulaFromCell(h4);
if (this.b0!=null)v7=this.b0;
try {
if (j4!=h4){
j4.style.display="none";
}
else {
j4.innerHTML="";
}
}catch (g3){
return ;
}
var v8=this.GetCellEditorID(e4,h4);
if (v8!=null&&v8.length>0){
this.a8=this.GetCellEditor(e4,v8,true);
if (!this.a8.getAttribute("MccbId")&&!this.a8.getAttribute("Extenders"))
this.a8.style.display="inline";
else 
this.a8.style.display="block";
}else {
this.a8=document.createElement("INPUT");
this.a8.type="text";
}
this.a8.style.fontFamily=j4.style.fontFamily;
this.a8.style.fontSize=j4.style.fontSize;
this.a8.style.fontWeight=j4.style.fontWeight;
this.a8.style.fontStyle=j4.style.fontStyle;
this.a8.style.textDecoration=j4.style.textDecoration;
this.a8.style.position="";
if (j6){
var k3=h4.clientWidth-2;
var v9=parseInt(h4.style.paddingLeft);
if (!isNaN(v9))
k3-=v9;
v9=parseInt(h4.style.paddingRight);
if (!isNaN(v9))
k3-=v9;
this.a8.style.width=""+k3+"px";
}
else 
this.a8.style.width=h4.clientWidth-2;
this.SaveMargin(h4);
if (this.a8.tagName=="TEXTAREA")
this.a8.style.height=""+(h4.offsetHeight-4)+"px";
if ((this.a8.tagName=="INPUT"&&this.a8.type=="text")||this.a8.tagName=="TEXTAREA"){
if (this.a8.style.backgroundColor==""||this.a8.backColorSet!=null){
var w0="";
if (document.defaultView!=null&&document.defaultView.getComputedStyle!=null)w0=document.defaultView.getComputedStyle(h4,'').getPropertyValue("background-color");
if (w0!="")
this.a8.style.backgroundColor=w0;
else 
this.a8.style.backgroundColor=h4.bgColor;
this.a8.backColorSet=true;
}
if (this.a8.style.color==""||this.a8.colorSet!=null){
var w1="";
if (document.defaultView!=null&&document.defaultView.getComputedStyle!=null)w1=document.defaultView.getComputedStyle(h4,'').getPropertyValue("color");
this.a8.style.color=w1;
this.a8.colorSet=true;
}
this.a8.style.borderWidth="0px";
this.RestoreMargin(this.a8,false);
}
this.b1=0;
h4.insertBefore(this.a8,h4.firstChild);
this.SetEditorValue(this.a8,v7);
if (this.a8.offsetHeight<h4.clientHeight&&this.a8.tagName!="TEXTAREA"){
if (h4.vAlign=="middle")
this.a8.style.posTop+=(h4.clientHeight-this.a8.offsetHeight)/2;
else if (h4.vAlign=="bottom")
this.a8.style.posTop+=(h4.clientHeight-this.a8.offsetHeight);
}
this.SizeAll(this.GetTopSpread(e4));
}
this.SetEditorFocus(this.a8);
if (e4.getAttribute("EditMode")=="replace"){
if ((this.a8.tagName=="INPUT"&&this.a8.type=="text")||this.a8.tagName=="TEXTAREA")
this.a8.select();
}
this.a7=true;
var f9=this.GetCmdBtn(e4,"Update");
if (f9!=null&&f9.disabled)
this.UpdateCmdBtnState(f9,false);
f9=this.GetCmdBtn(e4,"Copy");
if (f9!=null&&!f9.disabled)
this.UpdateCmdBtnState(f9,true);
f9=this.GetCmdBtn(e4,"Paste");
if (f9!=null&&!f9.disabled)
this.UpdateCmdBtnState(f9,true);
f9=this.GetCmdBtn(e4,"Clear");
if (f9!=null&&!f9.disabled)
this.UpdateCmdBtnState(f9,true);
}
this.ScrollView(e4);
}
this.GetCurrency=function (validator){
var w2=validator.CurrencySymbol;
if (w2!=null)return w2;
var g0=document.getElementById(validator.id+"cs");
if (g0!=null){
return g0.innerText;
}
return "";
}
this.GetValueFromRender=function (e4,rd){
var s4=this.GetCellType2(this.GetCell(rd));
if (s4!=null){
if (s4=="text")s4="TextCellType";
var i3=null;
if (s4=="ExtenderCellType"){
i3=this.GetFunction(s4+"_getEditor")
if (i3!=null){
if (i3(rd)!=null)
i3=this.GetFunction(s4+"_getEditorValue");
else 
i3=null;
}
}else 
i3=this.GetFunction(s4+"_getValue");
if (i3!=null){
return i3(rd,e4);
}
}
var g0=rd;
while (g0.firstChild!=null&&g0.firstChild.nodeName!="#text")g0=g0.firstChild;
if (g0.innerHTML=="&nbsp;")return "";
var v7=g0.value;
if ((typeof(v7)=="undefined")&&s4=="readonly"&&g0.parentNode!=null&&g0.parentNode.getAttribute("CellType2")=="TagCloudCellType")
v7=g0.textContent;
if (v7==null){
v7=this.ReplaceAll(g0.innerHTML,"&nbsp;"," ");
v7=this.ReplaceAll(v7,"<br>"," ");
v7=this.HTMLDecode(v7);
}
return v7;
}
this.ReplaceAll=function (val,t9,dest){
if (val==null)return val;
var w3=val.length;
while (true){
val=val.replace(t9,dest);
if (val.length==w3)break ;
w3=val.length;
}
return val;
}
this.GetFormula=function (e4,h1,h3){
h1=this.GetDisplayIndex(e4,h1);
var h4=this.GetCellFromRowCol(e4,h1,h3);
var w4=this.GetFormulaFromCell(h4);
return w4;
}
this.SetFormula=function (e4,h1,h3,i3,recalc,clientOnly){
h1=this.GetDisplayIndex(e4,h1);
var h4=this.GetCellFromRowCol(e4,h1,h3);
h4.setAttribute("FpFormula",i3);
if (!clientOnly)
this.SetCellValue(e4,h4,i3,null,recalc);
}
this.GetFormulaFromCell=function (rd){
var v7=null;
if (rd.getAttribute("FpFormula")!=null){
v7=rd.getAttribute("FpFormula");
}
if (v7!=null)
v7=this.Trim(new String(v7));
return v7;
}
this.IsDouble=function (val,decimalchar,negsign,possign,minimumvalue,maximumvalue){
if (val==null||val.length==0)return true;
val=val.replace(" ","");
if (val.length==0)return true;
if (negsign!=null)val=val.replace(negsign,"-");
if (possign!=null)val=val.replace(possign,"+");
if (val.charAt(val.length-1)=="-")val="-"+val.substring(0,val.length-1);
var w5=new RegExp("^\\s*([-\\+])?(\\d+)?(\\"+decimalchar+"(\\d+))?([eE]([-\\+])?(\\d+))?\\s*$");
var w6=val.match(w5);
if (w6==null)
return false;
if ((w6[2]==null||w6[2].length==0)&&(w6[4]==null||w6[4].length==0))return false;
var w7="";
if (w6[1]!=null&&w6[1].length>0)w7=w6[1];
if (w6[2]!=null&&w6[2].length>0)
w7+=w6[2];
else 
w7+="0";
if (w6[4]!=null&&w6[4].length>0)
w7+=("."+w6[4]);
if (w6[6]!=null&&w6[6].length>0){
w7+=("E"+w6[6]);
if (w6[7]!=null)
w7+=(w6[7]);
else 
w7+="0";
}
var w8=parseFloat(w7);
if (isNaN(w8))return false;
var g0=true;
if (minimumvalue!=null){
var w9=parseFloat(minimumvalue);
g0=(!isNaN(w9)&&w8>=w9);
}
if (g0&&maximumvalue!=null){
var j1=parseFloat(maximumvalue);
g0=(!isNaN(j1)&&w8<=j1);
}
return g0;
}
this.GetFunction=function (fn){
if (fn==null||fn=="")return null;
try {
var g0=eval(fn);
return g0;
}catch (g3){
return null;
}
}
this.SetValueToRender=function (rd,val,valueonly){
var i3=null;
var s4=this.GetCellType2(this.GetCell(rd));
if (s4!=null){
if (s4=="text")s4="TextCellType";
if (s4=="ExtenderCellType"){
i3=this.GetFunction(s4+"_getEditor")
if (i3!=null){
if (i3(rd)!=null)
i3=this.GetFunction(s4+"_setEditorValue");
else 
i3=null;
}
}else 
i3=this.GetFunction(s4+"_setValue");
}
if (i3!=null){
i3(rd,val);
}else {
if (typeof(rd.value)!="undefined"){
if (val==null)val="";
rd.value=val;
}else {
var g0=rd;
while (g0.firstChild!=null&&g0.firstChild.nodeName!="#text")g0=g0.firstChild;
g0.innerHTML=this.ReplaceAll(val," ","&nbsp;");
}
}
if ((valueonly==null||!valueonly)&&rd.getAttribute("FpFormula")!=null){
rd.setAttribute("FpFormula",val);
}
}
this.Trim=function (t0){
var w6=t0.match(new RegExp("^\\s*(\\S+(\\s+\\S+)*)\\s*$"));
return (w6==null)?"":w6[1];
}
this.GetOffsetLeft=function (e4,h4,i8){
var e7=i8;
if (e7==null)e7=this.GetViewportFromCell(e4,h4);
var r3=0;
var g0=h4;
while (g0!=null&&g0!=e7&&this.IsChild(g0,e7)){
r3+=g0.offsetLeft;
g0=g0.offsetParent;
}
return r3;
}
this.GetOffsetTop=function (e4,h4,i8){
var e7=i8;
if (e7==null)e7=this.GetViewportFromCell(e4,h4);
var x0=0;
var g0=h4;
while (g0!=null&&g0!=e7&&this.IsChild(g0,e7)){
x0+=g0.offsetTop;
g0=g0.offsetParent;
}
return x0;
}
this.SetEditorFocus=function (g0){
if (g0==null)return ;
var x1=true;
var h4=this.GetCell(g0,true);
var s4=this.GetCellType(h4);
if (s4!=null){
var i3=this.GetFunction(s4+"_setFocus");
if (i3!=null){
i3(g0);
x1=false;
}
}
if (x1){
try {
g0.focus();
}catch (g3){}
}
}
this.SetEditorValue=function (g0,val){
var h4=this.GetCell(g0,true);
var s4=this.GetCellType(h4);
if (s4=="text")s4="TextCellType";
if (s4!=null){
var i3=this.GetFunction(s4+"_setEditorValue");
if (i3!=null){
i3(g0,val);
return ;
}
}
g0.value=val;
}
this.GetEditorValue=function (g0){
var h4=this.GetCell(g0,true);
var s4=this.GetCellType(h4);
if (s4!=null){
var i3=this.GetFunction(s4+"_getEditorValue");
if (i3!=null){
return i3(g0);
}
}
if (g0.type=="checkbox"){
if (g0.checked)
return "True";
else 
return "False";
}
else 
{
return g0.value;
}
}
this.CreateMsg=function (){
if (this.b2!=null)return ;
var g0=this.b2=document.createElement("div");
g0.style.position="absolute";
g0.style.background="yellow";
g0.style.color="red";
g0.style.border="1px solid black";
g0.style.display="none";
}
this.SetMsg=function (msg){
this.CreateMsg();
this.b2.innerHTML=msg;
this.b2.width=this.b2.offsetWidth+6;
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
var h4=this.GetCell(this.a8.parentNode);
var e4=this.GetSpread(h4,false);
if (e4==null)return true;
var x2=this.GetEditorValue(this.a8);
var x3=x2;
if (typeof(x2)=="string")x3=this.Trim(x2);
var x4=(e4.getAttribute("AcceptFormula")=="true"&&x3!=null&&x3.charAt(0)=='=');
var i4=(this.b1!=0);
if (!x4&&!i4){
var x5=null;
var s4=this.GetCellType(h4);
if (s4!=null){
var i3=this.GetFunction(s4+"_isValid");
if (i3!=null){
x5=i3(h4,x2);
}
}
if (x5!=null&&x5!=""){
this.SetMsg(x5);
this.GetViewport(e4).parentNode.insertBefore(this.b2,this.GetViewport(e4).parentNode.firstChild);
this.ShowMsg(true);
this.SetValidatorPos(e4);
this.a8.focus();
return false;
}else {
this.ShowMsg(false);
}
}
if (!i4){
h4.removeChild(this.a8);
this.a8.style.display="none";
this.GetViewport(e4).parentNode.appendChild(this.a8);
this.SetEditorValue(this.a8,"");
var x6=this.GetRender(h4);
if (x6.style.display=="none")x6.style.display="block";
if (this.b0!=null&&this.b0==x2){
this.SetValueToRender(x6,this.a9,true);
}else {
this.SetValueToRender(x6,x2);
}
this.RestoreMargin(h4);
}
if ((this.b0!=null&&this.b0!=x2)||(this.b0==null&&this.a9!=x2)){
this.SetCellValue(e4,h4,x2);
if (x2!=null&&x2.length>0&&x2.indexOf("=")==0)h4.setAttribute("FpFormula",x2);
}
if (!i4)
this.SizeAll(this.GetTopSpread(e4));
this.a8=null;
this.a7=false;
var g3=this.CreateEvent("EditStopped");
g3.cell=h4;
this.FireEvent(e4,g3);
this.Focus(e4);
var x7=e4.getAttribute("autoCalc");
if (x7!="false"){
if ((this.b0!=null&&this.b0!=x2)||(this.b0==null&&this.a9!=x2)){
this.UpdateValues(e4);
}
}
}
this.b1=-1;
return true;
}
this.SetValidatorPos=function (e4){
if (this.a8==null)return ;
var h4=this.GetCell(this.a8.parentNode);
if (h4==null)return ;
var g0=this.b2;
if (g0!=null&&g0.style.display!="none"){
var n2=this.GetViewport0(e4);
var f4=this.GetViewport1(e4);
var x8=this.GetViewport2(e4);
var e7=this.GetViewport(e4);
var p8=this.GetColHeader(e4).offsetHeight;
if ((n2!=null||f4!=null)&&(this.IsChild(h4,x8)||this.IsChild(h4,e7))){
if (n2!=null)
p8+=n2.offsetHeight;
else 
p8+=f4.offsetHeight;
}
var x9=this.GetRowHeader(e4).offsetWidth;
if ((n2!=null||x8!=null)&&(this.IsChild(h4,f4)||this.IsChild(h4,e7)))
{
if (n2!=null)
x9+=n2.offsetWidth;
else 
x9+=x8.offsetWidth;
}
if (e4.frzRows==0&&e4.frzCols==0){
p8=0;
x9=0;
}else {
if (e7!=null&&this.IsChild(h4,e7)){
p8-=e7.parentNode.scrollTop;
x9-=e7.parentNode.scrollLeft;
}
}
g0.style.left=""+(x9+h4.offsetLeft)+"px";
g0.style.top=""+(p8+h4.offsetTop+h4.offsetHeight)+"px";
if (h4.offsetTop+h4.offsetHeight+g0.offsetHeight+16>g0.parentNode.offsetHeight)
g0.style.top=""+(p8+h4.offsetTop-g0.offsetHeight-1)+"px";
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
this.RestoreMargin=function (h4,reset){
if (this.b3.left!=null&&this.b3.left!=-1){
h4.style.paddingLeft=this.b3.left;
if (reset==null||reset)this.b3.left=-1;
}
if (this.b3.right!=null&&this.b3.right!=-1){
h4.style.paddingRight=this.b3.right;
if (reset==null||reset)this.b3.right=-1;
}
if (this.b3.top!=null&&this.b3.top!=-1){
h4.style.paddingTop=this.b3.top;
if (reset==null||reset)this.b3.top=-1;
}
if (this.b3.bottom!=null&&this.b3.bottom!=-1){
h4.style.paddingBottom=this.b3.bottom;
if (reset==null||reset)this.b3.bottom=-1;
}
}
this.PaintSelectedCell=function (e4,h4,select,anchor){
if (h4==null)return ;
var y0=anchor?e4.getAttribute("anchorBackColor"):e4.getAttribute("selectedBackColor");
if (select){
if (h4.getAttribute("bgColorBak")==null)
h4.setAttribute("bgColorBak",document.defaultView.getComputedStyle(h4,"").getPropertyValue("background-color"));
if (h4.bgColor1==null)
h4.bgColor1=h4.style.backgroundColor;
h4.style.backgroundColor=y0;
if (h4.getAttribute("bgSelImg"))
h4.style.backgroundImage=h4.getAttribute("bgSelImg");
}else {
if (h4.bgColor1!=null)
h4.style.backgroundColor="";
if (h4.bgColor1!=null&&h4.bgColor1!="")
h4.style.backgroundColor=h4.bgColor1;
h4.style.backgroundImage="";
if (h4.getAttribute("bgImg")!=null)
h4.style.backgroundImage=h4.getAttribute("bgImg");
}
}
this.PaintAnchorCell=function (e4){
var n9=this.GetOperationMode(e4);
if (e4.d1==null||(n9!="Normal"&&n9!="RowMode"))return ;
if (n9=="MultiSelect"||n9=="ExtendedSelect")return ;
if (!this.IsChild(e4.d1,e4))return ;
var y1=(this.GetTopSpread(e4).getAttribute("hierView")!="true");
if (e4.getAttribute("showFocusRect")=="false")y1=false;
if (y1){
this.PaintSelectedCell(e4,e4.d1,false);
this.PaintFocusRect(e4);
this.PaintAnchorCellHeader(e4,true);
return ;
}
var g0=e4.d1.parentNode.cells[0].firstChild;
if (g0!=null&&g0.nodeName!="#text"&&g0.getAttribute("FpSpread")=="Spread")return ;
this.PaintSelectedCell(e4,e4.d1,true,true);
this.PaintAnchorCellHeader(e4,true);
}
this.ClearSelection=function (e4,thisonly){
var y2=this.GetParentSpread(e4);
if (thisonly==null&&y2!=null&&y2.getAttribute("hierView")=="true"){
this.ClearSelection(y2);
return ;
}
var i6=this.GetViewport(e4);
var f5=this.GetRowCount(e4);
if (i6!=null&&i6.rows.length>f5){
for (var f1=0;f1<i6.rows.length;f1++){
if (i6.rows[f1].cells.length>0&&i6.rows[f1].cells[0]!=null&&i6.rows[f1].cells[0].firstChild!=null&&i6.rows[f1].cells[0].firstChild.nodeName!="#text"){
var g0=i6.rows[f1].cells[0].firstChild;
if (g0.getAttribute("FpSpread")=="Spread"){
this.ClearSelection(g0,true);
}
}
}
}
this.DoclearSelection(e4);
if (e4.d1!=null){
var v6=this.GetOperationMode(e4);
if (v6=="RowMode"||v6=="SingleSelect"||v6=="ExtendedSelect"||v6=="MultiSelect"){
var h5=this.GetRowFromCell(e4,e4.d1);
this.PaintSelection(e4,h5,-1,1,-1,false);
}
this.PaintSelectedCell(e4,e4.d1,false);
this.PaintAnchorCellHeader(e4,false);
}else {
var h4=this.GetCellFromRowCol(e4,1,0);
if (h4!=null)this.PaintSelectedCell(e4,h4,false);
}
this.PaintFocusRect(e4);
e4.selectedCols=[];
e4.e2=true;
}
this.SetSelectedRange=function (e4,h1,h3,n3,h0,t1){
this.ClearSelection(e4);
var h1=this.GetDisplayIndex(e4,h1);
var u3=0;
var y3=n3;
var i6=this.GetViewport(e4);
if (i6!=null){
for (f1=h1;f1<i6.rows.length&&u3<y3;f1++){
if (this.IsChildSpreadRow(e4,i6,f1)){;
n3++;
}else {
u3++;
}
}
}
var y4=null;
var a2=null;
if (e4.getAttribute("LayoutMode")){
if (h3>=0&&n3<0){
if (h0!=1)return ;
var i9=this.GetDisplayIndex2(e4,t1);
var h4=this.GetCellByRowCol(e4,i9,h3);
if (h4!=null&&parseInt(h4.getAttribute("col"))!=-1){
h3=parseInt(h4.getAttribute("col"));
y4=parseInt(h4.parentNode.getAttribute("row"));
a2=this.GetColFromCell(e4,h4);
}
else 
return ;
this.PaintMultipleRowSelection(e4,y4,a2,1,h0,true);
}
else if (h1>=0&&h0<0){
if (n3>parseInt(e4.getAttribute("layoutrowcount")))return ;
var y5=parseInt(this.GetRowKeyFromRow(e4,h1));
var h1=parseInt(this.GetFirstRowFromKey(e4,y5));
this.UpdateAnchorCell(e4,h1,0,true);
n3=parseInt(e4.getAttribute("layoutrowcount"));
this.PaintSelection(h1,h3,n3,h0,true);
}
else if (h1>=0&&h3>=0&&(h0>1||n3>1))
return ;
}
else 
this.PaintSelection(e4,h1,h3,n3,h0,true);
this.SetSelection(e4,h1,h3,n3,h0,null,y4,a2);
}
this.AddSelection=function (e4,h1,h3,n3,h0,t1){
var h1=this.GetDisplayIndex(e4,h1);
var u3=0;
var y3=n3;
var i6=this.GetViewport(e4);
if (i6!=null){
for (f1=h1;f1<i6.rows.length&&u3<y3;f1++){
if (this.IsChildSpreadRow(e4,i6,f1)){;
n3++;
}else {
u3++;
}
}
}
var y4;
var a2;
if (e4.getAttribute("LayoutMode")){
if (h3>=0&&n3<0){
if (h0!=1)return ;
var i9=this.GetDisplayIndex2(e4,t1);
var h4=this.GetCellByRowCol(e4,i9,h3);
if (h4!=null&&parseInt(h4.getAttribute("col"))!=-1){
y4=parseInt(h4.parentNode.getAttribute("row"));
a2=this.GetColFromCell(e4,h4);
h3=parseInt(h4.getAttribute("col"));
}
else 
return ;
this.PaintMultipleRowSelection(e4,y4,a2,1,h0,true);
}
else if (h1>=0&&h0<0){
if (n3>parseInt(e4.getAttribute("layoutrowcount")))return ;
var y5=parseInt(this.GetRowKeyFromRow(e4,h1));
var h1=parseInt(this.GetFirstRowFromKey(e4,y5));
if (e4.allowGroup){
this.ClearSelection(e4);
this.UpdateAnchorCell(e4,h1,0,true);
}
n3=parseInt(e4.getAttribute("layoutrowcount"));
this.PaintSelection(e4,h1,h3,n3,h0,true);
}
else if (h1>=0&&h3>=0&&(h0>1||n3>1))
return ;
}
else 
this.PaintSelection(e4,h1,h3,n3,h0,true);
this.SetSelection(e4,h1,h3,n3,h0,true,y4,a2);
}
this.SelectRow=function (e4,index,u3,select,ignoreAnchor){
e4.d5=index;
e4.d6=-1;
if (!ignoreAnchor)this.UpdateAnchorCell(e4,index,0,false);
e4.d7="r";
var y6=u3;
if (e4.getAttribute("LayoutMode")){
y6=parseInt(e4.getAttribute("layoutrowcount"));
}
this.PaintSelection(e4,index,-1,y6,-1,select);
if (select)
{
this.SetSelection(e4,index,-1,u3,-1);
}else {
this.RemoveSelection(e4,index,-1,u3,-1);
this.PaintFocusRect(e4);
}
}
this.SelectColumn=function (e4,index,u3,select,ignoreAnchor){
e4.d5=-1;
e4.d6=index;
if (!ignoreAnchor){
var h1=0;
var y7=index;
if (e4.getAttribute("LayoutMode")){
var h4=e4.d1;
if (parseInt(e4.d1.getAttribute("col"))==-1)return ;
if (h4){
h1=this.GetRowFromCell(e4,h4);
y7=this.GetColFromCell(e4,h4);
}
e4.copymulticol=true;
}
this.UpdateAnchorCell(e4,h1,y7,false,true);
}
e4.d7="c";
if (!e4.getAttribute("LayoutMode"))
this.PaintSelection(e4,-1,y7,-1,u3,select);
else 
this.PaintMultipleRowSelection(e4,h1,y7,1,u3,select);
if (select)
{
this.SetSelection(e4,-1,index,-1,u3,null,h1,y7);
this.AddColSelection(e4,index);
}
}
this.AddColSelection=function (e4,index){
var y8=0;
for (var f1=0;f1<e4.selectedCols.length;f1++){
if (e4.selectedCols[f1]==index)return ;
if (index>e4.selectedCols[f1])y8=f1+1;
}
e4.selectedCols.length++;
for (var f1=e4.selectedCols.length-1;f1>y8;f1--)
e4.selectedCols[f1]=e4.selectedCols[f1-1];
e4.selectedCols[y8]=index;
}
this.IsColSelected=function (e4,u4){
for (var f1=0;f1<e4.selectedCols.length;f1++)
if (e4.selectedCols[f1]==u4)return true;
return false;
}
this.SyncColSelection=function (e4){
e4.selectedCols=[];
var y9=this.GetSelectedRanges(e4);
for (var f1=0;f1<y9.length;f1++){
var h9=y9[f1];
if (h9.type=="Column"){
for (var i1=h9.col;i1<h9.col+h9.colCount;i1++){
this.AddColSelection(e4,i1);
}
}
}
}
this.InitMovingCol=function (e4,u4,isGroupBar,o8){
if (e4.getAttribute("LayoutMode")&&u4==-1)return ;
if (this.GetOperationMode(e4)!="Normal"){
e4.selectedCols=[];
e4.selectedCols.push(u4);
}
if (isGroupBar){
this.dragCol=u4;
this.dragViewCol=this.GetColByKey(e4,u4);
}else {
if (e4.getAttribute("LayoutMode"))
this.dragCol=this.GetColTemplateRowFromGroupCell(e4,u4);
else 
this.dragCol=parseInt(this.GetSheetColIndex(e4,u4));
this.dragViewCol=u4;
}
var z0=this.GetMovingCol(e4);
if (isGroupBar){
this.ClearSelection(e4);
z0.innerHTML="";
var z1=document.createElement("DIV");
z1.innerHTML=o8.innerHTML;
z1.style.borderTop="0px solid";
z1.style.borderLeft="0px solid";
z1.style.borderRight="#808080 1px solid";
z1.style.borderBottom="#808080 1px solid";
z1.style.width=""+Math.max(this.GetPreferredCellWidth(e4,o8),80)+"px";
z0.appendChild(z1);
if (e4.getAttribute("DragColumnCssClass")==null){
z0.style.backgroundColor=o8.style.backgroundColor;
z0.style.paddingTop="1px";
z0.style.paddingBottom="1px";
}
z0.style.top="-50px";
z0.style.left="-100px";
}else {
var z2=0;
z0.style.top="0px";
z0.style.left="-1000px";
z0.style.display="";
z0.innerHTML="";
var z3=document.createElement("TABLE");
z0.appendChild(z3);
var i9=document.createElement("TR");
z3.appendChild(i9);
for (var f1=0;f1<e4.selectedCols.length;f1++){
var h4=document.createElement("TD");
i9.appendChild(h4);
var z4;
var z5;
if (e4.getAttribute("LayoutMode")){
z4=this.GetRowTemplateRowFromGroupCell(e4,u4,true);
z5=this.GetColTemplateRowFromGroupCell(e4,u4);
}
else {
if (e4.getAttribute("columnHeaderAutoTextIndex")!=null)
z4=parseInt(e4.getAttribute("columnHeaderAutoTextIndex"));
else 
z4=e4.getAttribute("ColHeaders")-1;
z5=e4.selectedCols[f1];
}
var z6=this.GetHeaderCellFromRowCol(e4,z4,z5,true);
if (z6.getAttribute("FpCellType")=="ExtenderCellType"&&z6.getElementsByTagName("DIV").length>0){
var z7=this.GetEditor(z6);
var z8=this.GetFunction("ExtenderCellType_getEditorValue");
if (z7!==null&&z8!==null){
h4.innerHTML=z8(z7);
}
}
else 
h4.innerHTML=z6.innerHTML;
h4.style.cssText=z6.style.cssText;
h4.style.borderTop="0px solid";
h4.style.borderLeft="0px solid";
h4.style.borderRight="#808080 1px solid";
h4.style.borderBottom="#808080 1px solid";
h4.setAttribute("align","center");
var j3=Math.max(this.GetPreferredCellWidth(e4,z6),80);
h4.style.width=""+j3+"px";
z2+=j3;
}
if (e4.getAttribute("DragColumnCssClass")==null){
z0.style.backgroundColor=e4.getAttribute("SelectedBackColor");
z0.style.tableLayout="fixed";
z0.style.width=""+z2+"px";
}
}
e4.selectedCols.context=[];
var z9=e4.selectedCols.context;
var r3=0;
var f3=this.GetColGroup(this.GetFrozColHeader(e4));
if (f3!=null){
for (var f1=0;f1<f3.childNodes.length;f1++){
var aa0=f3.childNodes[f1].offsetWidth;
z9.push({left:r3,width:aa0});
r3+=aa0;
}
}
f3=this.GetColGroup(this.GetColHeader(e4));
if (f3!=null){
for (var f1=0;f1<f3.childNodes.length;f1++){
var aa0=f3.childNodes[f1].offsetWidth;
z9.push({left:r3,width:aa0});
r3+=aa0;
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
this.GetSpanCell=function (h1,h3,span){
if (span==null){
return null;
}
var u3=span.length;
for (var f1=0;f1<u3;f1++){
var r5=span[f1];
var aa1=(r5.row<=h1&&h1<r5.row+r5.rowCount&&r5.col<=h3&&h3<r5.col+r5.colCount);
if (aa1)return r5;
}
return null;
}
this.IsCovered=function (e4,h1,h3,span){
var r5=this.GetSpanCell(h1,h3,span);
if (r5==null){
return false;
}else {
if (r5.row==h1&&r5.col==h3)return false;
return true;
}
}
this.IsSpanCell=function (e4,h1,h3){
var d9=e4.d9;
var u3=d9.length;
for (var f1=0;f1<u3;f1++){
var r5=d9[f1];
var aa1=(r5.row==h1&&r5.col==h3);
if (aa1)return r5;
}
return null;
}
this.SelectRange=function (e4,h1,h3,n3,h0,select){
e4.d7="";
this.UpdateRangeSelection(e4,h1,h3,n3,h0,select);
if (select){
this.SetSelection(e4,h1,h3,n3,h0);
this.PaintAnchorCell(e4);
}
}
this.UpdateRangeSelection=function (e4,h1,h3,n3,h0,select){
var i6=this.GetViewport(e4);
this.UpdateRangeSelection(e4,h1,h3,n3,h0,select,i6);
}
this.GetSpanCells=function (e4,i6){
if (i6==this.GetViewport(e4)||i6==this.GetViewport1(e4)||i6==this.GetViewport2(e4)||i6==this.GetViewport0(e4))
return e4.d9;
else if (i6==this.GetColHeader(e4)||i6==this.GetFrozColHeader(e4))
return e4.e1;
else if (i6==this.GetRowHeader(e4)||i6==this.GetFrozRowHeader(e4))
return e4.e0;
return null;
}
this.UpdateRangeSelection=function (e4,h1,h3,n3,h0,select,i6){
if (i6==null)return ;
for (var f1=h1;f1<h1+n3&&f1<i6.rows.length;f1++){
if (this.IsChildSpreadRow(e4,i6,f1))continue ;
var aa2=this.GetCellIndex(e4,f1,h3,this.GetSpanCells(e4,i6));
for (var i1=0;i1<h0;i1++){
if (this.IsCovered(e4,f1,h3+i1,this.GetSpanCells(e4,i6)))continue ;
if (aa2<i6.rows[f1].cells.length){
this.PaintSelectedCell(e4,i6.rows[f1].cells[aa2],select);
}
aa2++;
}
}
}
this.GetColFromCell=function (e4,h4){
if (h4==null)return -1;
var h1=this.GetRowFromCell(e4,h4);
return this.GetColIndex(e4,h1,h4.cellIndex,this.GetSpanCells(e4,h4.parentNode.parentNode.parentNode),this.InFrozCols(e4,h4),this.IsChild(h4,this.GetFrozRowHeader(e4))||this.IsChild(h4,this.GetRowHeader(e4)));
}
this.GetRowFromCell=function (e4,h4){
if (h4==null||h4.parentNode==null)return -1;
var h1=h4.parentNode.rowIndex;
if (e4.frzRows>0&&(this.IsChild(h4,this.GetViewport2(e4))||this.IsChild(h4,this.GetViewport(e4))||this.IsChild(h4,this.GetRowHeader(e4)))){
h1+=e4.frzRows;
}
return h1;
}
this.GetColIndex=function (e4,f1,t7,span,frozArea,c5){
var aa3=0;
var u3=this.GetColCount(e4);
var aa4=e4.frzCols;
if (frozArea){
u3=e4.frzCols;
aa4=0;
}else if (c5){
aa4=0;
var f3=null;
if (this.GetFrozRowHeader(e4)!=null)
f3=this.GetColGroup(this.GetFrozRowHeader(e4));
else if (this.GetRowHeader(e4)!=null)
f3=this.GetColGroup(this.GetRowHeader(e4));
if (f3!=null)
u3=f3.childNodes.length;
}
for (var i1=aa4;i1<u3;i1++){
if (this.IsCovered(e4,f1,i1,span))continue ;
if (aa3==t7){
return i1;
}
aa3++;
}
return u3;
}
this.GetCellIndex=function (e4,f1,u4,span){
var aa5=false;
var e7=this.GetViewport(e4);
if (e7!=null)aa5=e7.parentNode.getAttribute("hiddenCells");
if (aa5&&span==e4.d9){
if (span!=e4.e0&&u4>=e4.frzCols){
return u4-e4.frzCols;
}
return u4;
}else {
var aa4=0;
var u3=u4;
if (span!=e4.e0&&u4>=e4.frzCols){
aa4=e4.frzCols;
u3=u4-e4.frzCols;
}
var aa3=0;
for (var i1=0;i1<u3;i1++){
if (this.IsCovered(e4,f1,aa4+i1,span))continue ;
aa3++;
}
return aa3;
}
}
this.NextCell=function (e4,event,key){
if (event.altKey)return ;
var aa6=this.GetParent(this.GetViewport(e4));
if (e4.d1==null){
var i3=this.FireActiveCellChangingEvent(e4,0,0);
if (!i3){
e4.SetActiveCell(0,0);
var g3=this.CreateEvent("ActiveCellChanged");
g3.cmdID=e4.id;
g3.row=g3.Row=0;
g3.col=g3.Col=0;
this.FireEvent(e4,g3);
}
return ;
}
if (event.shiftKey&&key!=event.DOM_VK_TAB){
var r1=this.GetOperationMode(e4);
if (r1=="RowMode"||r1=="SingleSelect"||r1=="MultiSelect"||(r1=="Normal"&&this.GetSelectionPolicy(e4)=="Single"))return ;
var r5=this.GetSpanCell(e4.d3,e4.d4,this.GetSpanCells(e4,this.GetViewportFromCell(e4,e4.d1)));
switch (key){
case event.DOM_VK_RIGHT:
var h1=e4.d3;
var h3=e4.d4+1;
if (r5!=null){
h3=r5.col+r5.colCount;
}
if (h3>this.GetColCount(e4)-1)return ;
e4.d4=h3;
e4.d2=this.GetCellFromRowCol(e4,h1,h3);
this.Select(e4,e4.d1,e4.d2);
break ;
case event.DOM_VK_LEFT:
var h1=e4.d3;
var h3=e4.d4-1;
if (r5!=null){
h3=r5.col-1;
}
r5=this.GetSpanCell(h1,h3,this.GetSpanCells(e4,this.GetViewportFromCell(e4,e4.d1)));
if (r5!=null){
if (this.IsSpanCell(e4,h1,r5.col))h3=r5.col;
}
if (h3<0)return ;
e4.d4=h3;
e4.d2=this.GetCellFromRowCol(e4,h1,h3);
this.Select(e4,e4.d1,e4.d2);
break ;
case event.DOM_VK_DOWN:
var h1=e4.d3+1;
var h3=e4.d4;
if (r5!=null){
h1=r5.row+r5.rowCount;
}
h1=this.GetNextRow(e4,h1);
if (h1>this.GetRowCountInternal(e4)-1)return ;
e4.d3=h1;
e4.d2=this.GetCellFromRowCol(e4,h1,h3);
this.Select(e4,e4.d1,e4.d2);
break ;
case event.DOM_VK_UP:
var h1=e4.d3-1;
var h3=e4.d4;
if (r5!=null){
h1=r5.row-1;
}
h1=this.GetPrevRow(e4,h1);
r5=this.GetSpanCell(h1,h3,this.GetSpanCells(e4,this.GetViewportFromCell(e4,e4.d1)));
if (r5!=null){
if (this.IsSpanCell(e4,r5.row,h3))h1=r5.row;
}
if (h1<0)return ;
e4.d3=h1;
e4.d2=this.GetCellFromRowCol(e4,h1,h3);
this.Select(e4,e4.d1,e4.d2);
break ;
case event.DOM_VK_HOME:
if (e4.d1.parentNode.rowIndex>=0){
e4.d4=0;
e4.d2=this.GetCellFromRowCol(e4,e4.d3,e4.d4);
this.Select(e4,e4.d1,e4.d2);
}
break ;
case event.DOM_VK_END:
if (e4.d1.parentNode.rowIndex>=0){
e4.d4=this.GetColCount(e4)-1;
e4.d2=this.GetCellFromRowCol(e4,e4.d3,e4.d4);
this.Select(e4,e4.d1,e4.d2);
}
break ;
case event.DOM_VK_PAGE_DOWN:
if (this.GetViewport(e4)!=null&&e4.d1.parentNode.rowIndex>=0){
h1=0;
for (h1=0;h1<this.GetViewport(e4).rows.length;h1++){
if (this.GetViewport(e4).rows[h1].offsetTop+this.GetViewport(e4).rows[h1].offsetHeight>this.GetViewport(e4).parentNode.offsetHeight+this.GetViewport(e4).parentNode.scrollTop){
break ;
}
}
h1=this.GetNextRow(e4,h1);
if (h1<this.GetViewport(e4).rows.length){
this.GetViewport(e4).parentNode.scrollTop=this.GetViewport(e4).rows[h1].offsetTop;
e4.d3=h1;
}else {
h1=this.GetRowCountInternal(e4)-1;
e4.d3=h1;
}
e4.d2=this.GetCellFromRowCol(e4,e4.d3,e4.d4);
this.Select(e4,e4.d1,e4.d2);
}
break ;
case event.DOM_VK_PAGE_UP:
if (this.GetViewport(e4)!=null&&e4.d1.parentNode.rowIndex>0){
h1=0;
for (h1=0;h1<this.GetViewport(e4).rows.length;h1++){
if (this.GetViewport(e4).rows[h1].offsetTop+this.GetViewport(e4).rows[h1].offsetHeight>this.GetViewport(e4).parentNode.scrollTop){
break ;
}
}
if (h1<this.GetViewport(e4).rows.length){
var j7=0;
while (h1>0){
j7+=this.GetViewport(e4).rows[h1].offsetHeight;
if (j7>this.GetViewport(e4).parentNode.offsetHeight){
break ;
}
h1--;
}
h1=this.GetPrevRow(e4,h1);
if (h1>=0){
this.GetViewport(e4).parentNode.scrollTop=this.GetViewport(e4).rows[h1].offsetTop;
e4.d3=h1;
e4.d2=this.GetCellFromRowCol(e4,e4.d3,e4.d4);
this.Select(e4,e4.d1,e4.d2);
}
}
}
break ;
}
this.SyncColSelection(e4);
}else {
if (key==event.DOM_VK_TAB){
if (event.shiftKey)key=event.DOM_VK_LEFT;
else key=event.DOM_VK_RIGHT;
}
var aa7=e4.d1;
var h1=e4.d3;
var h3=e4.d4;
switch (key){
case event.DOM_VK_RIGHT:
if (event.keyCode==event.DOM_VK_TAB){
var aa8=h1;
var aa9=h3;
do {
this.MoveRight(e4,h1,h3);
h1=e4.d3;
h3=e4.d4;
}while (!(aa8==h1&&aa9==h3)&&this.GetCellFromRowCol(e4,h1,h3).getAttribute("TabStop")!=null&&this.GetCellFromRowCol(e4,h1,h3).getAttribute("TabStop")=="false")
}
else {
this.MoveRight(e4,h1,h3);
}
break ;
case event.DOM_VK_LEFT:
if (event.keyCode==event.DOM_VK_TAB){
var aa8=h1;
var aa9=h3;
do {
this.MoveLeft(e4,h1,h3);
h1=e4.d3;
h3=e4.d4;
}while (!(aa8==h1&&aa9==h3)&&this.GetCellFromRowCol(e4,h1,h3).getAttribute("TabStop")!=null&&this.GetCellFromRowCol(e4,h1,h3).getAttribute("TabStop")=="false")
}
else {
this.MoveLeft(e4,h1,h3);
}
break ;
case event.DOM_VK_DOWN:
this.MoveDown(e4,h1,h3);
break ;
case event.DOM_VK_UP:
this.MoveUp(e4,h1,h3);
break ;
case event.DOM_VK_HOME:
if (e4.d1.parentNode.rowIndex>=0){
this.UpdateLeadingCell(e4,h1,0);
}
break ;
case event.DOM_VK_END:
if (e4.d1.parentNode.rowIndex>=0){
h3=this.GetColCount(e4)-1;
this.UpdateLeadingCell(e4,h1,h3);
}
break ;
case event.DOM_VK_PAGE_DOWN:
if (this.GetViewport(e4)!=null&&e4.d1.parentNode.rowIndex>=0){
h1=0;
for (h1=0;h1<this.GetViewport(e4).rows.length;h1++){
if (this.GetViewport(e4).rows[h1].offsetTop+this.GetViewport(e4).rows[h1].offsetHeight>this.GetViewport(e4).parentNode.offsetHeight+this.GetViewport(e4).parentNode.scrollTop){
break ;
}
}
h1=this.GetNextRow(e4,h1);
if (h1<this.GetViewport(e4).rows.length){
var g0=this.GetViewport(e4).rows[h1].offsetTop;
this.UpdateLeadingCell(e4,h1,e4.d4);
this.GetViewport(e4).parentNode.scrollTop=g0;
}else {
h1=this.GetPrevRow(e4,this.GetRowCount(e4)-1);
this.UpdateLeadingCell(e4,h1,e4.d4);
}
}
break ;
case event.DOM_VK_PAGE_UP:
if (this.GetViewport(e4)!=null&&e4.d1.parentNode.rowIndex>=0){
h1=0;
for (h1=0;h1<this.GetViewport(e4).rows.length;h1++){
if (this.GetViewport(e4).rows[h1].offsetTop+this.GetViewport(e4).rows[h1].offsetHeight>this.GetViewport(e4).parentNode.scrollTop){
break ;
}
}
if (h1<this.GetViewport(e4).rows.length){
var j7=0;
while (h1>=0){
j7+=this.GetViewport(e4).rows[h1].offsetHeight;
if (j7>this.GetViewport(e4).parentNode.offsetHeight){
break ;
}
h1--;
}
h1=this.GetPrevRow(e4,h1);
if (h1>=0){
var g0=this.GetViewport(e4).rows[h1].offsetTop;
this.UpdateLeadingCell(e4,h1,e4.d4);
this.GetViewport(e4).parentNode.scrollTop=g0;
}
}
}
break ;
}
if (aa7!=e4.d1){
var g3=this.CreateEvent("ActiveCellChanged");
g3.cmdID=e4.id;
g3.Row=g3.row=this.GetSheetIndex(e4,this.GetRowFromCell(e4,e4.d1));
g3.Col=g3.col=this.GetColFromCell(e4,e4.d1);
if (e4.getAttribute("LayoutMode"))
g3.InnerRow=g3.innerRow=e4.d1.parentNode.getAttribute("row");
this.FireEvent(e4,g3);
}
}
var h4=this.GetCellFromRowCol(e4,e4.d3,e4.d4);
if (key==event.DOM_VK_LEFT&&h4.offsetLeft<aa6.scrollLeft){
if (h4.cellIndex>0)
aa6.scrollLeft=e4.d1.offsetLeft;
else 
aa6.scrollLeft=0;
}else if (h4.cellIndex==0){
aa6.scrollLeft=0;
}
if (key==event.DOM_VK_RIGHT&&h4.offsetLeft+h4.offsetWidth>aa6.scrollLeft+aa6.offsetWidth-10){
aa6.scrollLeft+=h4.offsetWidth;
}
if (key==event.DOM_VK_UP&&h4.parentNode.offsetTop<aa6.scrollTop){
if (h4.parentNode.rowIndex>1)
aa6.scrollTop=h4.parentNode.offsetTop;
else 
aa6.scrollTop=0;
}else if (h4.parentNode.rowIndex==1){
aa6.scrollTop=0;
}
var ab0=this.GetParent(this.GetViewport(e4));
aa6=this.GetParent(this.GetViewport(e4));
if (key==event.DOM_VK_DOWN&&(this.IsChild(h4,aa6)||this.IsChild(h4,this.GetViewport2(e4)))&&h4.offsetTop+h4.offsetHeight>aa6.scrollTop+aa6.clientHeight){
ab0.scrollTop+=h4.offsetHeight;
}
if (h4!=null&&h4.offsetWidth<aa6.clientWidth){
if ((this.IsChild(h4,aa6)||this.IsChild(h4,this.GetViewport1(e4)))&&h4.offsetLeft+h4.offsetWidth>aa6.scrollLeft+aa6.clientWidth){
ab0.scrollLeft=h4.offsetLeft+h4.offsetWidth-aa6.clientWidth;
}
}
if ((this.IsChild(h4,aa6)||this.IsChild(h4,this.GetViewport1(e4)))&&h4.offsetTop+h4.offsetHeight>aa6.scrollTop+aa6.clientHeight&&h4.offsetHeight<aa6.clientHeight){
ab0.scrollTop=h4.offsetTop+h4.offsetHeight-aa6.clientHeight;
}
if (h4.offsetTop<aa6.scrollTop){
ab0.scrollTop=h4.offsetTop;
}
this.ScrollView(e4);
this.EnableButtons(e4);
this.SaveData(e4);
var i3=true;
if (e4.d1!=null){
var i4=this.GetEditor(e4.d1);
if (i4!=null){
this.SetEditorFocus(i4);
i3=false;
if (!i4.disabled&&(i4.type==null||i4.type=="checkbox"||i4.type=="radio"||i4.type=="text"||i4.type=="password"||i4.tagName=="SELECT")){
this.a7=true;
this.a8=i4;
this.a9=this.GetEditorValue(i4);
}
}else {
this.a7=false;
this.a8=null;
}
}
if (i3)this.Focus(e4);
}
this.MoveUp=function (e4,h1,h3){
var n3=this.GetRowCountInternal(e4);
var h0=this.GetColCount(e4);
h1--;
h1=this.GetPrevRow(e4,h1);
if (h1>=0){
e4.d3=h1;
this.UpdateLeadingCell(e4,e4.d3,e4.d4);
}
}
this.MoveDown=function (e4,h1,h3){
var n3=this.GetRowCountInternal(e4);
var h0=this.GetColCount(e4);
var r5=this.GetSpanCell(h1,h3,this.GetSpanCells(e4,this.GetViewportFromCell(e4,e4.d1)));
if (r5!=null){
h1=r5.row+r5.rowCount;
}else {
h1++;
}
h1=this.GetNextRow(e4,h1);
if (h1==n3)h1=n3-1;
if (h1<n3){
e4.d3=h1;
this.UpdateLeadingCell(e4,e4.d3,e4.d4);
}
}
this.MoveLeft=function (e4,h1,h3){
var ab1=h1;
var n3=this.GetRowCountInternal(e4);
var h0=this.GetColCount(e4);
var r5=this.GetSpanCell(h1,h3,this.GetSpanCells(e4,this.GetViewportFromCell(e4,e4.d1)));
if (r5!=null){
h3=r5.col-1;
}else {
h3--;
}
if (h3<0){
h3=h0-1;
h1--;
if (h1<0){
h1=n3-1;
}
h1=this.GetPrevRow(e4,h1);
if (h1<0){
h1=n3-1;
}
h1=this.GetPrevRow(e4,h1);
e4.d3=h1;
}
var ab2=this.UpdateLeadingCell(e4,e4.d3,h3);
if (ab2)e4.d3=ab1;
}
this.MoveRight=function (e4,h1,h3){
var ab1=h1;
var n3=this.GetRowCountInternal(e4);
var h0=this.GetColCount(e4);
var r5=this.GetSpanCell(h1,h3,this.GetSpanCells(e4,this.GetViewportFromCell(e4,e4.d1)));
if (r5!=null){
h3=r5.col+r5.colCount;
}else {
h3++;
}
if (h3>=h0){
h3=0;
h1++;
if (h1>=n3)h1=0;
h1=this.GetNextRow(e4,h1);
if (h1>=n3)h1=0;
h1=this.GetNextRow(e4,h1);
e4.d3=h1;
}
var ab2=this.UpdateLeadingCell(e4,e4.d3,h3);
if (ab2)e4.d3=ab1;
}
this.UpdateLeadingCell=function (e4,h1,h3){
var ab3=0;
if (e4.getAttribute("LayoutMode")){
ab3=this.GetRowFromViewPort(e4,h1,h3);
var ab4=this.GetCellFromRowCol(e4,ab3,h3);
if (ab4)ab3=ab4.parentNode.getAttribute("row");
}
var i3=this.FireActiveCellChangingEvent(e4,h1,h3,ab3);
if (!i3){
var n9=this.GetOperationMode(e4);
if (n9!="MultiSelect")
this.ClearSelection(e4);
e4.d4=h3;
e4.d3=h1;
e4.d6=h3;
e4.d5=h1;
this.UpdateAnchorCell(e4,h1,h3);
}
return i3;
}
this.GetPrevRow=function (e4,h1){
if (h1<0)return 0;
var i6=this.GetViewport(e4);
if (h1<e4.frzRows){
i6=this.GetViewport0(e4);
if (i6==null)i6=this.GetViewport1(e4);
}
while (i6!=null&&h1<i6.rows.length){
if (this.IsChildSpreadRow(e4,i6,h1))
h1--;
else 
break ;
}
var h7=0;
if (h1>=e4.frzRows){
h1=h1-e4.frzRows;
h7=e4.frzRows;
}
if (e4.frzCols<this.GetColCount(e4)){
var x8=this.GetViewport2(e4);
while ((x8==null||h1<x8.rows.length&&x8.rows[h1].cells.length==0)&&i6!=null&&h1>0&&i6.rows[h1].cells.length==0)h1--;
}
if (i6!=null&&h1>=0&&h1<i6.rows.length){
if (i6.rows[h1].getAttribute("previewrow")){
h1--;
}
}
return h1+h7;
}
this.GetNextRow=function (e4,h1){
var i6=this.GetViewport(e4);
if (h1<e4.frzRows){
i6=this.GetViewport0(e4);
if (i6==null)i6=this.GetViewport1(e4);
}
while (i6!=null&&h1<i6.rows.length){
if (this.IsChildSpreadRow(e4,i6,h1))h1++;
else 
break ;
}
var h7=0;
if (h1>=e4.frzRows){
h1=h1-e4.frzRows;
h7=e4.frzRows;
}
if (e4.frzCols<this.GetColCount(e4)){
var x8=this.GetViewport2(e4);
while ((x8==null||h1<x8.rows.length&&x8.rows[h1].cells.length==0)&&i6!=null&&h1<i6.rows.length&&i6.rows[h1].cells.length==0)h1++;
}
if (i6!=null&&h1>=0&&h1<i6.rows.length){
if (i6.rows[h1].getAttribute("previewrow")){
h1++;
}
}
return h1+h7;
}
this.FireActiveCellChangingEvent=function (e4,i9,n6,t1){
var g3=this.CreateEvent("ActiveCellChanging");
g3.cancel=false;
g3.cmdID=e4.id;
g3.row=this.GetSheetIndex(e4,i9);
g3.col=n6;
if (e4.getAttribute("LayoutMode"))
g3.innerRow=t1;
this.FireEvent(e4,g3);
return g3.cancel;
}
this.GetSheetRowIndex=function (e4,h1){
h1=this.GetDisplayIndex(e4,h1);
if (h1<0)return -1;
var m6=null;
if (e4.frzRows>0){
if (h1>=e4.frzRows&&this.GetViewport(e4)!=null){
m6=this.GetViewport(e4).rows[h1-e4.frzRows];
}else if (h1<e4.frzRows&&this.GetViewport1(e4)!=null){
m6=this.GetViewport1(e4).rows[h1];
}
}else {
m6=this.GetViewport(e4).rows[h1];
}
if (m6!=null){
return m6.getAttribute("FpKey");
}else {
return -1;
}
}
this.GetSheetColIndex=function (e4,h3,t1){
var n6=-1;
if (e4.getAttribute("LayoutMode")){
var h1=this.GetDisplayIndex2(e4,t1);
var h4=this.GetCellByRowCol(e4,h1,h3);
if (h4!=null)
n6=parseInt(h4.getAttribute("col"));
}
else {
var f3=null;
if (e4.frzCols>0&&h3<e4.frzCols){
var k5=this.GetFrozColHeader(e4);
if (k5!=null&&k5.rows.length>0){
f3=this.GetColGroup(k5);
}else {
var m0=this.GetViewport2(e4);
if (m0!=null&&m0.rows.length>0){
f3=this.GetColGroup(m0);
}else {
var l8=this.GetViewport0(e4);
if (l8!=null&&l8.rows.length>0){
f3=this.GetColGroup(l8);
}
}
}
if (f3!=null&&h3>=0&&h3<f3.childNodes.length)n6=f3.childNodes[h3].getAttribute("FpCol");
}else {
var ab5=this.GetColHeader(e4);
if (ab5!=null&&ab5.rows.length>0){
f3=this.GetColGroup(ab5);
}else {
var e7=this.GetViewport(e4);
if (e7!=null&&e7.rows.length>0){
f3=this.GetColGroup(e7);
}else {
var l9=this.GetViewport1(e4);
if (l9!=null&&l9.rows.length>0){
f3=this.GetColGroup(l9);
}
}
}
if (f3!=null&&h3>=0&&h3<f3.childNodes.length){
n6=f3.childNodes[h3-e4.frzCols].getAttribute("FpCol");
}
}
}
return n6;
}
this.GetCellByRowCol=function (e4,h1,h3){
h1=this.GetDisplayIndex(e4,h1);
return this.GetCellFromRowCol(e4,h1,h3);
}
this.GetHeaderCellFromRowCol=function (e4,h1,h3,c6){
if (h1<0||h3<0)return null;
var e7=null;
if (c6){
if (h3<e4.frzCols){
e7=this.GetFrozColHeader(e4);
}else {
e7=this.GetColHeader(e4);
}
}else {
if (h1<e4.frzRows){
e7=this.GetFrozRowHeader(e4);
}else {
e7=this.GetRowHeader(e4);
}
}
var r5=this.GetSpanCell(h1,h3,this.GetSpanCells(e4,e7));
if (r5!=null){
h1=r5.row;
h3=r5.col;
}
var t6=this.GetCellIndex(e4,h1,h3,this.GetSpanCells(e4,e7));
if (!c6){
if (h1>=e4.frzRows){
h1-=e4.frzRows;
}
}
return e7.rows[h1].cells[t6];
}
this.GetCellFromRowCol=function (e4,h1,h3,prevCell){
if (h1<0||h3<0)return null;
var e7=null;
if (h1<e4.frzRows){
if (h3<e4.frzCols){
e7=this.GetViewport0(e4);
}else {
e7=this.GetViewport1(e4);
}
}else {
if (h3<e4.frzCols){
e7=this.GetViewport2(e4);
}else {
e7=this.GetViewport(e4);
}
}
var d9=e4.d9;
var r5=this.GetSpanCell(h1,h3,d9);
if (r5!=null){
h1=r5.row;
h3=r5.col;
}
var t6=0;
var aa5=false;
if (e7!=null)aa5=e7.parentNode.getAttribute("hiddenCells");
if (prevCell!=null&&!aa5){
if (prevCell.cellIndex<prevCell.parentNode.cells.length-1)
t6=prevCell.cellIndex+1;
}
else 
{
t6=this.GetCellIndex(e4,h1,h3,d9);
}
if (h1>=e4.frzRows){
h1-=e4.frzRows;
}
if (h1>=0&&h1<e7.rows.length)
return e7.rows[h1].cells[t6];
else 
return null;
}
this.GetHiddenValue=function (e4,h1,colName){
if (colName==null)return ;
h1=this.GetDisplayIndex(e4,h1);
var v7=null;
var e7=null;
e7=this.GetViewport(e4);
if (e7!=null&&h1>=0&&h1<e7.rows.length){
var m6=e7.rows[h1];
v7=m6.getAttribute("hv"+colName);
}
return v7;
}
this.GetValue=function (e4,h1,h3){
h1=this.GetDisplayIndex(e4,h1);
var h4=this.GetCellFromRowCol(e4,h1,h3);
var j4=this.GetRender(h4);
var v7=this.GetValueFromRender(e4,j4);
if (v7!=null)v7=this.Trim(v7.toString());
return v7;
}
this.SetValue=function (e4,h1,h3,x2,noEvent,recalc){
h1=this.GetDisplayIndex(e4,h1);
if (x2!=null&&typeof(x2)!="string")x2=new String(x2);
var h4=this.GetCellFromRowCol(e4,h1,h3);
if (this.ValidateCell(e4,h4,x2)){
this.SetCellValueFromView(h4,x2);
if (x2!=null){
this.SetCellValue(e4,h4,""+x2,noEvent,recalc);
}else {
this.SetCellValue(e4,h4,"",noEvent,recalc);
}
this.SizeSpread(e4);
}else {
if (e4.getAttribute("lcidMsg")!=null)
alert(e4.getAttribute("lcidMsg"));
else 
alert("Can't set the data into the cell. The data type is not correct for the cell.");
}
}
this.SetActiveCell=function (e4,h1,h3){
this.ClearSelection(e4,true);
h1=this.GetDisplayIndex(e4,h1);
this.UpdateAnchorCell(e4,h1,h3);
this.ResetLeadingCell(e4);
}
this.GetOperationMode=function (e4){
var n9=e4.getAttribute("OperationMode");
return n9;
}
this.SetOperationMode=function (e4,n9){
e4.setAttribute("OperationMode",n9);
}
this.GetEnableRowEditTemplate=function (e4){
var ab6=e4.getAttribute("EnableRowEditTemplate");
return ab6;
}
this.GetSelectionPolicy=function (e4){
var ab7=e4.getAttribute("SelectionPolicy");
return ab7;
}
this.UpdateAnchorCell=function (e4,h1,h3,select,isColHeader){
if (h1<0||h3<0)return ;
if (e4.getAttribute("LayoutMode")&&e4.allowGroup&&isColHeader)
h1=this.GetRowFromViewPort(e4,h1,h3);
e4.d1=this.GetCellFromRowCol(e4,h1,h3);
if (e4.d1==null)return ;
this.SetActiveRow(e4,this.GetRowKeyFromCell(e4,e4.d1));
this.SetActiveCol(e4,e4.getAttribute("LayoutMode")?this.GetColKeyFromCell2(e4,e4.d1):this.GetColKeyFromCell(e4,e4.d1));
if (select==null||select){
var n9=this.GetOperationMode(e4);
if (n9=="RowMode"||n9=="SingleSelect"||n9=="ExtendedSelect")
this.SelectRow(e4,h1,1,true,true);
else if (n9!="MultiSelect")
this.SelectRange(e4,h1,h3,1,1,true);
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
this.Edit=function (e4,i9){
var n9=this.GetOperationMode(e4);
if (n9!="RowMode")return ;
var v4=e4.getAttribute("name");
var ab8=(e4.getAttribute("ajax")!="false");
if (ab8){
if (FarPoint&&FarPoint.System.WebControl.MultiColumnComboBoxCellTypeUtilitis)
FarPoint.System.WebControl.MultiColumnComboBoxCellTypeUtilitis.CloseAll();
this.SyncData(v4,"Edit,"+i9,e4);
}
else 
__doPostBack(v4,"Edit,"+i9);
}
this.Update=function (e4){
if (this.a7&&this.GetOperationMode(e4)!="RowMode"&&this.GetEnableRowEditTemplate(e4)!="true")return ;
this.SaveData(e4);
var v4=e4.getAttribute("name");
var ab8=(e4.getAttribute("ajax")!="false");
if (ab8)
this.SyncData(v4,"Update",e4);
else 
__doPostBack(v4,"Update");
}
this.Cancel=function (e4){
var g0=document.getElementById(e4.id+"_data");
g0.value="";
this.SaveData(e4);
var v4=e4.getAttribute("name");
var ab8=(e4.getAttribute("ajax")!="false");
if (ab8)
this.SyncData(v4,"Cancel",e4);
else 
__doPostBack(v4,"Cancel");
}
this.Add=function (e4){
if (this.a7)return ;
var v4=null;
var p6=this.GetPageActiveSpread();
if (p6!=null){
v4=p6.getAttribute("name");
}else {
v4=e4.getAttribute("name");
}
var ab8=(e4.getAttribute("ajax")!="false");
if (ab8)
this.SyncData(v4,"Add",e4);
else 
__doPostBack(v4,"Add");
}
this.Insert=function (e4){
if (this.a7)return ;
var v4=null;
var p6=this.GetPageActiveSpread();
if (p6!=null){
v4=p6.getAttribute("name");
}else {
v4=e4.getAttribute("name");
}
var ab8=(e4.getAttribute("ajax")!="false");
if (ab8)
this.SyncData(v4,"Insert",e4);
else 
__doPostBack(v4,"Insert");
}
this.Delete=function (e4){
if (this.a7)return ;
var v4=null;
var p6=this.GetPageActiveSpread();
if (p6!=null){
v4=p6.getAttribute("name");
}else {
v4=e4.getAttribute("name");
}
var ab8=(e4.getAttribute("ajax")!="false");
if (ab8)
this.SyncData(v4,"Delete",e4);
else 
__doPostBack(v4,"Delete");
}
this.Print=function (e4){
if (this.a7)return ;
this.SaveData(e4);
if (document.printSpread==null){
var g0=document.createElement("IFRAME");
g0.name="printSpread";
g0.style.position="absolute";
g0.style.left="-10px";
g0.style.width="0px";
g0.style.height="0px";
document.printSpread=g0;
document.body.insertBefore(g0,null);
g0.addEventListener("load",function (){the_fpSpread.PrintSpread();},false);
}
var ab9=this.GetForm(e4);
if (ab9==null)return ;
{
var i3=ab9;
i3.__EVENTTARGET.value=e4.getAttribute("name");
i3.__EVENTARGUMENT.value="Print";
var ac0=i3.target;
i3.target="printSpread";
i3.submit();
i3.target=ac0;
}
}
this.PrintSpread=function (){
document.printSpread.contentWindow.focus();
document.printSpread.contentWindow.print();
window.focus();
var p6=this.GetPageActiveSpread();
if (p6!=null)this.Focus(p6);
}
this.GotoPage=function (e4,f0){
if (this.a7)return ;
var v4=e4.getAttribute("name");
var ab8=(e4.getAttribute("ajax")!="false");
if (ab8)
this.SyncData(v4,"Page,"+f0,e4);
else 
__doPostBack(v4,"Page,"+f0);
}
this.Next=function (e4){
if (this.a7)return ;
var v4=e4.getAttribute("name");
var ab8=(e4.getAttribute("ajax")!="false");
if (ab8)
this.SyncData(v4,"Next",e4);
else 
__doPostBack(v4,"Next");
}
this.Prev=function (e4){
if (this.a7)return ;
var v4=e4.getAttribute("name");
var ab8=(e4.getAttribute("ajax")!="false");
if (ab8)
this.SyncData(v4,"Prev",e4);
else 
__doPostBack(v4,"Prev");
}
this.GetViewportFromCell=function (e4,j2){
if (j2!=null){
var g0=j2;
while (g0!=null){
if (g0.tagName=="TABLE")break ;
g0=g0.parentNode;
}
if (g0==this.GetViewport(e4)||g0==this.GetViewport0(e4)||g0==this.GetViewport1(e4)||g0==this.GetViewport2(e4))
return g0;
}
return null;
}
this.IsChild=function (h4,i8){
if (h4==null||i8==null)return false;
var g0=h4.parentNode;
while (g0!=null){
if (g0==i8)return true;
g0=g0.parentNode;
}
return false;
}
this.GetCorner=function (e4){
return e4.c4;
}
this.GetFooterCorner=function (e4){
return e4.footerCorner;
}
this.Select=function (e4,cl1,cl2){
if (this.GetSpread(cl1)!=e4||this.GetSpread(cl2)!=e4)return ;
var h5=e4.d5;
var h6=e4.d6;
var ac1=this.GetRowFromCell(e4,cl2);
var m7=0;
if (e4.d7=="r"){
m7=-1;
if (this.IsChild(cl2,this.GetColHeader(e4)))
ac1=0;
}else if (e4.d7=="c"){
if (this.IsChild(cl2,this.GetRowHeader(e4)))
m7=0;
else 
m7=this.GetColFromCell(e4,cl2);
ac1=-1;
}
else {
if (this.IsChild(cl2,this.GetColHeader(e4))){
ac1=0;m7=this.GetColFromCell(e4,cl2);
}else if (this.IsChild(cl2,this.GetRowHeader(e4))){
m7=0;
}else {
m7=this.GetColFromCell(e4,cl2);
}
}
if (e4.d7=="t"){
h6=m7=h5=ac1=-1;
}
var g0=Math.max(h5,ac1);
h5=Math.min(h5,ac1);
ac1=g0;
g0=Math.max(h6,m7);
h6=Math.min(h6,m7);
m7=g0;
var h9=null;
var n7=this.GetSelection(e4);
var n8=n7.lastChild;
if (n8!=null){
var h1=this.GetRowByKey(e4,n8.getAttribute("row"));
var h3=this.GetColByKey(e4,n8.getAttribute("col"));
var n3=parseInt(n8.getAttribute("rowcount"));
var h0=parseInt(n8.getAttribute("colcount"));
h9=new this.Range();
this.SetRange(h9,"cell",h1,h3,n3,h0);
}
if (h9!=null&&h9.col==-1&&h9.row==-1)return ;
if (h9!=null&&h9.col==-1&&h9.row>=0){
if (h9.row>ac1||h9.row+h9.rowCount-1<h5){
this.PaintSelection(e4,h9.row,h9.col,h9.rowCount,h9.colCount,false);
this.PaintSelection(e4,h5,h6,ac1-h5+1,m7-h6+1,true);
}else {
if (h5>h9.row){
var g0=h5-h9.row;
this.PaintSelection(e4,h9.row,h9.col,g0,h9.colCount,false);
if (ac1<h9.row+h9.rowCount-1){
this.PaintSelection(e4,ac1,h9.col,h9.row+h9.rowCount-ac1,h9.colCount,false);
}else {
this.PaintSelection(e4,h9.row+h9.rowCount,h9.col,ac1-h9.row-h9.rowCount+1,h9.colCount,true);
}
}else {
this.PaintSelection(e4,h5,h9.col,h9.row-h5,h9.colCount,true);
if (ac1<h9.row+h9.rowCount-1){
this.PaintSelection(e4,ac1+1,h9.col,h9.row+h9.rowCount-ac1-1,h9.colCount,false);
}else {
this.PaintSelection(e4,h9.row+h9.rowCount,h9.col,ac1-h9.row-h9.rowCount+1,h9.colCount,true);
}
}
}
}else if (h9!=null&&h9.row==-1&&h9.col>=0){
if (h9.col>m7||h9.col+h9.colCount-1<h6){
this.PaintSelection(e4,h9.row,h9.col,h9.rowCount,h9.colCount,false);
this.PaintSelection(e4,h5,h6,ac1-h5+1,m7-h6+1,true);
}else {
if (h6>h9.col){
this.PaintSelection(e4,h9.row,h9.col,h9.rowCount,h6-h9.col,false);
if (m7<h9.col+h9.colCount-1){
this.PaintSelection(e4,h9.row,m7,h9.rowCount,h9.col+h9.colCount-m7,false);
}else {
this.PaintSelection(e4,h9.row,h9.col+h9.colCount,h9.rowCount,m7-h9.col-h9.colCount,true);
}
}else {
this.PaintSelection(e4,h9.row,h6,h9.rowCount,h9.col-h6,true);
if (m7<h9.col+h9.colCount-1){
this.PaintSelection(e4,h9.row,m7+1,h9.rowCount,h9.col+h9.colCount-m7-1,false);
}else {
this.PaintSelection(e4,h9.row,h9.col+h9.colCount,h9.rowCount,m7-h9.col-h9.colCount+1,true);
}
}
}
}else if (h9!=null&&h9.row>=0&&h9.col>=0){
this.ExtendSelection(e4,h9,h5,h6,ac1-h5+1,m7-h6+1);
}else {
this.PaintSelection(e4,h5,h6,ac1-h5+1,m7-h6+1,true);
}
this.SetSelection(e4,h5,h6,ac1-h5+1,m7-h6+1,h9==null);
}
this.ExtendSelection=function (e4,h9,newRow,newCol,newRowCount,newColCount)
{
var r3=Math.max(h9.col,newCol);
var r4=Math.min(h9.col+h9.colCount-1,newCol+newColCount-1);
var x0=Math.max(h9.row,newRow);
var ac2=Math.min(h9.row+h9.rowCount-1,newRow+newRowCount-1);
if (h9.row<x0){
this.PaintSelection(e4,h9.row,h9.col,x0-h9.row,h9.colCount,false);
}
if (h9.col<r3){
this.PaintSelection(e4,h9.row,h9.col,h9.rowCount,r3-h9.col,false);
}
if (h9.row+h9.rowCount-1>ac2){
this.PaintSelection(e4,ac2+1,h9.col,h9.row+h9.rowCount-ac2-1,h9.colCount,false);
}
if (h9.col+h9.colCount-1>r4){
this.PaintSelection(e4,h9.row,r4+1,h9.rowCount,h9.col+h9.colCount-r4-1,false);
}
if (newRow<x0){
this.PaintSelection(e4,newRow,newCol,x0-newRow,newColCount,true);
}
if (newCol<r3){
this.PaintSelection(e4,newRow,newCol,newRowCount,r3-newCol,true);
}
if (newRow+newRowCount-1>ac2){
this.PaintSelection(e4,ac2+1,newCol,newRow+newRowCount-ac2-1,newColCount,true);
}
if (newCol+newColCount-1>r4){
this.PaintSelection(e4,newRow,r4+1,newRowCount,newCol+newColCount-r4-1,true);
}
}
this.PaintAnchorCellHeader=function (e4,select){
var h1,h3;
h1=this.GetRowFromCell(e4,e4.d1);
h3=this.GetColFromCell(e4,e4.d1);
if (select&&e4.d1.getAttribute("group")!=null){
var r5=this.GetSpanCell(h1,h3,e4.d9);
if (r5!=null&&r5.colCount>1){
var ac3=this.GetSelectedRange(e4);
if (h1<ac3.row||h1>=ac3.row+ac3.rowCount||h3<ac3.col||h3>=ac3.col+ac3.colCount)
return ;
}
}
if (this.GetColHeader(e4)!=null)this.PaintHeaderSelection(e4,h1,h3,1,1,select,true);
if (this.GetRowHeader(e4)!=null)this.PaintHeaderSelection(e4,h1,h3,1,1,select,false);
}
this.LineIntersection=function (s1,h6,s2,m7){
var t0,g3;
t0=Math.max(s1,s2);
g3=Math.min(s1+h6,s2+m7);
if (t0<g3)
return {s:t0,c:g3-t0};
return null;
}
this.RangeIntersection=function (h5,h6,y3,cc1,ac1,m7,rc2,cc2){
var ac4=this.LineIntersection(h5,y3,ac1,rc2);
var ac5=this.LineIntersection(h6,cc1,m7,cc2);
if (ac4&&ac5)
return {row:ac4.s,col:ac5.s,rowCount:ac4.c,colCount:ac5.c};
return null;
}
this.PaintSelection=function (e4,h1,h3,n3,h0,select){
if (h1<0&&h3<0){
this.PaintCornerSelection(e4,select);
}
var ac6=false;
var ac7=false;
var t8;
var u4;
var ac8;
if (h1<0){
t8=h1;
h1=0;
n3=this.GetRowCountInternal(e4);
}
if (h3<0){
u4=h3;
h3=0;
h0=this.GetColCount(e4);
}
this.PaintViewportSelection(e4,h1,h3,n3,h0,select);
var n7=this.GetSelection(e4);
var n8;
var ac1;
var m7;
var ac9;
var ad0;
var ad1=0;
var ad2=0;
var h9;
var ad3;
for (var f1=n7.childNodes.length-1;f1>=0;f1--){
n8=n7.childNodes[f1];
if (n8){
ac1=parseInt(n8.getAttribute("rowIndex"));
m7=parseInt(n8.getAttribute("colIndex"));
ac9=parseInt(n8.getAttribute("rowcount"));
ad0=parseInt(n8.getAttribute("colcount"));
if (ac1<0||ac9<0){ac1=0;ac9=this.GetRowCountInternal(e4);}
if (m7<0||ad0<0){m7=0;ad0=this.GetColCount(e4);}
if (ad1<ac9)
ad1=ac9;
if (ad2<ad0)
ad2=ad0;
if (f1>=n7.childNodes.length-1){
if (h1<=ac1&&n3>=ac9||e4.getAttribute("LayoutMode")&&h0==1&&h1<parseInt(e4.getAttribute("layoutrowcount"))){
if (this.GetColHeader(e4)!=null&&this.GetOperationMode(e4)=="Normal"){
this.PaintHeaderSelection(e4,h1,h3,n3,h0,select,true);
ac6=true;
}
}
if (h3<=m7&&h0>=ad0){
if (this.GetRowHeader(e4)!=null){
this.PaintHeaderSelection(e4,h1,h3,n3,h0,select,false);
ac7=true;
}
}
if (!ac6&&!ac7){
if (this.GetColHeader(e4)!=null&&this.GetOperationMode(e4)=="Normal"){
this.PaintHeaderSelection(e4,h1,h3,n3,h0,select,true);
ac6=true;
}
if (this.GetRowHeader(e4)!=null){
this.PaintHeaderSelection(e4,h1,h3,n3,h0,select,false);
ac7=true;
}
}
}
else {
if (!select&&this.GetOperationMode(e4)=="Normal"&&!e4.getAttribute("LayoutMode")){
h9=this.RangeIntersection(h1,h3,n3,h0,ac1,m7,ac9,ad0);
if (h9){
this.PaintViewportSelection(e4,h9.row,h9.col,h9.rowCount,h9.colCount,true);
}
if (ac6){
ad3=this.LineIntersection(h3,h0,m7,ad0);
if (ad3)this.PaintHeaderSelection(e4,h1,ad3.s,n3,ad3.c,true,true);
}
if (ac7){
ad3=this.LineIntersection(h1,n3,ac1,ac9);
if (ad3)this.PaintHeaderSelection(e4,ad3.s,h3,ad3.c,h0,true,false);
}
}
}
}
}
if (t8!=null||u4!=null){
if ((ad1<n3&&t8<0)||(ad2<h0&&u4<0))
ac8=true;
}
if (n7.childNodes.length<=0||(e4.getAttribute("SelectionPolicy")=="MultiRange"&&ac8)){
if (this.GetColHeader(e4)!=null&&this.GetOperationMode(e4)=="Normal")this.PaintHeaderSelection(e4,h1,h3,n3,h0,select,true);
if (this.GetRowHeader(e4)!=null)this.PaintHeaderSelection(e4,h1,h3,n3,h0,select,false);
}
this.PaintAnchorCell(e4);
}
this.PaintFocusRect=function (e4){
var g7=document.getElementById(e4.id+"_focusRectT");
if (g7==null)return ;
var ad4=this.GetSelectedRange(e4);
if (e4.d1==null&&(ad4==null||(ad4.rowCount==0&&ad4.colCount==0))){
g7.style.left="-1000px";
var v4=e4.id;
g7=document.getElementById(v4+"_focusRectB");
g7.style.left="-1000px";
g7=document.getElementById(v4+"_focusRectL");
g7.style.left="-1000px";
g7=document.getElementById(v4+"_focusRectR");
g7.style.left="-1000px";
return ;
}
var i2=this.GetOperationMode(e4);
if (i2=="RowMode"||i2=="SingleSelect"||i2=="MultiSelect"||i2=="ExtendedSelect"){
var h1=e4.GetActiveRow();
ad4=new this.Range();
this.SetRange(ad4,"Row",h1,-1,1,-1);
}else if (ad4==null||(ad4.rowCount==0&&ad4.colCount==0)){
var h1=e4.GetActiveRow();
var h3=e4.GetActiveCol();
ad4=new this.Range();
this.SetRange(ad4,"Cell",h1,h3,e4.d1.rowSpan,e4.d1.colSpan);
}
if (ad4.row<0){
ad4.row=0;
ad4.rowCount=this.GetRowCountInternal(e4);
}
if (ad4.col<0){
ad4.col=0;
ad4.colCount=this.GetColCount(e4);
if (e4.getAttribute("LayoutMode")&&ad4.rowCount<parseInt(e4.getAttribute("layoutrowcount"))&&ad4.type=="Row")ad4.rowCount=parseInt(e4.getAttribute("layoutrowcount"));
}
var t8;
if (e4.getAttribute("LayoutMode"))
t8=(ad4.innerRow!=null)?ad4.innerRow:ad4.row;
else 
t8=ad4.row;
var h4=this.GetCellFromRowCol(e4,t8,ad4.col);
if (h4==null)return ;
if (ad4.rowCount==1&&ad4.colCount==1){
ad4.rowCount=h4.rowSpan;
ad4.colCount=h4.colSpan;
if (h4.colSpan>1){
var ad5=parseInt(h4.getAttribute("col"));
if (ad5!=ad4.col&&!isNaN(ad5)&&!e4.getAttribute("LayoutMode"))ad4.col=ad5;
}
}
var g0=this.GetOffsetTop(e4,h4);
var ad6=this.GetOffsetLeft(e4,h4);
if (h4.rowSpan>1){
t8=h4.parentNode.rowIndex;
var h6=this.GetCellFromRowCol(e4,t8,ad4.col+ad4.colCount-1);
if (h6!=null&&h6.parentNode.rowIndex>h4.parentNode.rowIndex){
g0=this.GetOffsetTop(e4,h6);
}
if (e4.getAttribute("LayoutMode")&&ad4.rowCount<h4.rowSpan&&(ad4.type=="Column"||ad4.type=="Row"))ad4.rowCount=h4.rowSpan;
}
if (h4.colSpan>1){
var h6=this.GetCellFromRowCol(e4,t8+ad4.rowCount-1,ad4.col);
var p8=this.GetOffsetLeft(e4,h6);
if (p8>ad6){
ad6=p8;
h4=h6;
}
if (e4.getAttribute("LayoutMode")&&ad4.colCount<h4.colSpan&&(ad4.type=="Column"||ad4.type=="Row"))ad4.colCount=h4.colSpan;
}
var j7=0;
var g9=this.GetViewport(e4).rows;
for (var h1=t8;h1<t8+ad4.rowCount&&h1<g9.length;h1++){
j7+=g9[h1].offsetHeight;
if (h1>t8)j7+=parseInt(this.GetViewport(e4).cellSpacing);
}
var j3=0;
var f3=this.GetColGroup(this.GetViewport(e4));
if (f3.childNodes==null||f3.childNodes.length==0)return ;
for (var h3=ad4.col;h3<ad4.col+ad4.colCount&&h3<f3.childNodes.length;h3++){
j3+=f3.childNodes[h3].offsetWidth;
if (h3>ad4.col)j3+=parseInt(this.GetViewport(e4).cellSpacing);
}
if (ad4.col>h4.cellIndex&&ad4.type=="Column"){
var m7=(e4.getAttribute("LayoutMode")!=null)?parseInt(h4.getAttribute("scol")):parseInt(h4.getAttribute("col"));
for (var h3=m7;h3<ad4.col;h3++){
ad6+=f3.childNodes[h3].offsetWidth;
if (h3>m7)ad6+=parseInt(this.GetViewport(e4).cellSpacing);
}
}
if (ad4.row>0)g0-=2;
else j7-=2;
if (ad4.col>0)ad6-=2;
else j3-=2;
if (parseInt(this.GetViewport(e4).cellSpacing)>0){
g0+=1;ad6+=1;
}else {
j3+=1;
j7+=1;
}
if (j3<0)j3=0;
if (j7<0)j7=0;
g7.style.left=""+ad6+"px";
g7.style.top=""+g0+"px";
g7.style.width=""+j3+"px";
g7=document.getElementById(e4.id+"_focusRectB");
g7.style.left=""+ad6+"px";
g7.style.top=""+(g0+j7)+"px";
g7.style.width=""+j3+"px";
g7=document.getElementById(e4.id+"_focusRectL");
g7.style.left=""+ad6+"px";
g7.style.top=""+g0+"px";
g7.style.height=""+j7+"px";
g7=document.getElementById(e4.id+"_focusRectR");
g7.style.left=""+(ad6+j3)+"px";
g7.style.top=""+g0+"px";
g7.style.height=""+j7+"px";
}
this.PaintCornerSelection=function (e4,select){
var ad7=true;
if (e4.getAttribute("ShowHeaderSelection")=="false")ad7=false;
if (!ad7)return ;
var n4=this.GetCorner(e4);
if (n4!=null&&n4.rows.length>0){
for (var f1=0;f1<n4.rows.length;f1++){
for (var i1=0;i1<n4.rows[f1].cells.length;i1++){
if (n4.rows[f1].cells[i1]!=null)
this.PaintSelectedCell(e4,n4.rows[f1].cells[i1],select);
}
}
}
}
this.PaintHeaderSelection=function (e4,h1,h3,n3,h0,select,c6){
var ad7=true;
if (e4.getAttribute("ShowHeaderSelection")=="false")ad7=false;
if (!ad7)return ;
var ad8=c6?e4.e1:e4.e0;
if (e4.getAttribute("LayoutMode")&&c6){
if (n3>parseInt(e4.getAttribute("layoutrowcount")))
n3=parseInt(e4.getAttribute("layoutrowcount"));
var ad9=this.GetCellFromRowCol(e4,h1,h3);
if (e4.allowGroup&&ad9.getAttribute("group")!=null)
h1=this.GetFirstMultiRowFromViewport(e4,h1,true);
for (var f1=h1;f1<h1+n3;f1++){
for (var i1=h3;i1<h3+h0;i1++){
var ae0=this.GetCellFromRowCol(e4,f1,i1);
if (ae0){
var o0=this.GetRowTemplateRowFromCell(e4,ae0);
if (!isNaN(o0)){
if (o0>=parseInt(e4.getAttribute("layoutrowcount")))o0=parseInt(e4.getAttribute("layoutrowcount"))-1;
var h4=this.GetHeaderCellFromRowCol(e4,o0,i1,c6);
if (h4!=null&&this.GetRowTemplateRowFromCell(e4,h4)==o0)this.PaintSelectedCell(e4,h4,select);
}
}
}
}
}
else {
var t9=this.GetRowCountInternal(e4);
var t4=this.GetColCount(e4);
if (c6){
if (this.GetColHeader(e4)==null)return ;
h1=0;
n3=t9=this.GetColHeader(e4).rows.length;
}else {
if (this.GetRowHeader(e4)==null)return ;
h3=0;
h0=t4=this.GetColGroup(this.GetRowHeader(e4)).childNodes.length;
}
}
if (e4.getAttribute("LayoutMode")&&e4.getAttribute("OperationMode")!="Normal"&&!c6)
h1=this.GetFirstMultiRowFromViewport(e4,h1,false);
if (e4.getAttribute("LayoutMode")&&e4.d1!=null&&e4.d1.getAttribute("group")!=null&&!c6&&n3!=t9)
n3=1;
for (var f1=h1;f1<h1+n3&&f1<t9;f1++){
if (!c6&&this.IsChildSpreadRow(e4,this.GetViewport(e4),f1))continue ;
for (var i1=h3;i1<h3+h0&&i1<t4;i1++){
if (!e4.getAttribute("LayoutMode")&&this.IsCovered(e4,f1,i1,ad8))continue ;
var h4=this.GetHeaderCellFromRowCol(e4,f1,i1,c6);
if (h4!=null)this.PaintSelectedCell(e4,h4,select);
}
}
}
this.PaintViewportSelection=function (e4,h1,h3,n3,h0,select){
var t9=this.GetRowCountInternal(e4);
var t4=this.GetColCount(e4);
if (e4.getAttribute("LayoutMode")&&e4.getAttribute("OperationMode")!="Normal"&&n3==parseInt(e4.getAttribute("layoutrowcount")))
h1=this.GetFirstMultiRowFromViewport(e4,h1,false);
if (e4.getAttribute("LayoutMode")&&e4.d1!=null&&e4.d1.getAttribute("group")!=null&&n3!=t9)
n3=1;
for (var f1=h1;f1<h1+n3&&f1<t9;f1++){
if (this.IsChildSpreadRow(e4,this.GetViewport(e4),f1))continue ;
var h4=null;
for (var i1=h3;i1<h3+h0&&i1<t4;i1++){
if (this.IsCovered(e4,f1,i1,e4.d9))continue ;
h4=this.GetCellFromRowCol(e4,f1,i1,h4);
this.PaintSelectedCell(e4,h4,select);
}
}
}
this.Copy=function (e4){
var p6=this.GetPageActiveSpread();
if (p6!=null&&p6!=e4&&this.GetTopSpread(p6)==e4){
this.Copy(p6);
return ;
}
var n7=this.GetSelection(e4);
var n8=n7.lastChild;
if (n8!=null){
var h1;
var h3;
var n3;
var h0;
e4.copymulticol=false;
if (e4.getAttribute("LayoutMode")&&n8.getAttribute("col")!="-1"&&n8.getAttribute("row")=="-1"&&n8.getAttribute("rowcount")=="-1"){
var h4=e4.d1;
if (h4){
h1=h4.parentNode.getAttribute("row");
h3=this.GetColFromCell(e4,h4);
n3=this.GetRowCountInternal(e4);
h0=parseInt(n8.getAttribute("colcount"));
e4.copymulticol=true;
}
}
else if (e4.getAttribute("LayoutMode")&&n8.getAttribute("col")=="-1"&&n8.getAttribute("row")!=-1){
var u1=parseInt(n8.getAttribute("row"));
h1=this.GetFirstRowFromKey(e4,u1);
h3=parseInt(n8.getAttribute("colIndex"));
n3=parseInt(e4.getAttribute("layoutrowcount"));
}
else {
h1=e4.getAttribute("LayoutMode")?parseInt(n8.getAttribute("rowIndex")):this.GetRowByKey(e4,n8.getAttribute("row"));
h3=e4.getAttribute("LayoutMode")?parseInt(n8.getAttribute("colIndex")):this.GetColByKey(e4,n8.getAttribute("col"));
n3=parseInt(n8.getAttribute("rowcount"));
h0=parseInt(n8.getAttribute("colcount"));
}
if (h1<0){
h1=0;
n3=this.GetRowCountInternal(e4);
}
if (h3<0){
h3=0;
h0=this.GetColCount(e4);
}
var f6="";
for (var f1=h1;f1<h1+n3;f1++){
if (this.IsChildSpreadRow(e4,this.GetViewport(e4),f1))continue ;
var h4=null;
for (var i1=h3;i1<h3+h0;i1++){
if (this.IsCovered(e4,f1,i1,e4.d9))
f6+="";
else 
{
h4=this.GetCellFromRowCol(e4,f1,i1,h4);
if (e4.getAttribute("LayoutMode")&&e4.copymulticol&&(h4==null||(h4.parentNode.getAttribute("row"))!=h1))continue ;
if (h4!=null&&h4.parentNode.getAttribute("previewrow")!=null)continue ;
var s4=this.GetCellType(h4);
if (s4=="TextCellType"&&h4.getAttribute("password")!=null)
f6+="";
else 
f6+=this.GetCellValueFromView(e4,h4);
}
if (i1+1<h3+h0)f6+="\t";
}
f6+="\r\n";
}
this.b9=f6;
}else {
if (e4.d1!=null){
var f6=this.GetCellValueFromView(e4,e4.d1);
this.b9=f6;
}
}
}
this.GetCellValueFromView=function (e4,h4){
var x2=null;
if (h4!=null){
var ae1=this.GetRender(h4);
x2=this.GetValueFromRender(e4,ae1);
if (x2==null||x2==" ")x2="";
}
return x2;
}
this.SetCellValueFromView=function (h4,x2,ignoreLock){
if (h4!=null){
var ae1=this.GetRender(h4);
var s9=this.GetCellType(h4);
if ((s9!="readonly"||ignoreLock)&&ae1!=null&&ae1.getAttribute("FpEditor")!="Button")
this.SetValueToRender(ae1,x2);
}
}
this.Paste=function (e4){
var p6=this.GetPageActiveSpread();
if (p6!=null&&p6!=e4&&this.GetTopSpread(p6)==e4){
this.Paste(p6);
return ;
}
if (e4.d1==null)return ;
var f6=this.b9;
if (f6==null)return ;
var e7=this.GetViewportFromCell(e4,e4.d1);
var h1=this.GetRowFromCell(e4,e4.d1);
var h3=this.GetColFromCell(e4,e4.d1);
var h0=this.GetColCount(e4);
var n3=this.GetRowCountInternal(e4);
var ae2=h1;
var aa2=h3;
var ae3=new String(f6);
if (ae3.length==0)return ;
var f0=ae3.lastIndexOf("\r\n");
if (f0>=0&&f0==ae3.length-2)ae3=ae3.substring(0,f0);
var ae4=0;
var ae5=ae3.split("\r\n");
for (var f1=0;f1<ae5.length&&ae2<n3;f1++){
if (typeof(ae5[f1])=="string"){
ae5[f1]=ae5[f1].split("\t");
if (ae5[f1].length>ae4)ae4=ae5[f1].length;
}
ae2++;
}
ae2=this.GetSheetIndex(e4,h1);
for (var f1=0;f1<ae5.length&&ae2<n3;f1++){
var ae6=ae5[f1];
if (ae6!=null){
aa2=h3;
var h4=null;
var ac1=this.GetDisplayIndex(e4,ae2);
for (var i1=0;i1<ae6.length&&aa2<h0;i1++){
if (!this.IsCovered(e4,ac1,aa2,e4.d9)){
h4=this.GetCellFromRowCol(e4,ac1,aa2,h4);
if (h4==null)return ;
if (e4.getAttribute("LayoutMode")&&e4.copymulticol&&parseInt(h4.parentNode.getAttribute("row"))!=parseInt(e4.d1.parentNode.getAttribute("row")))continue ;
if (h4!=null&&h4.parentNode.getAttribute("previewrow")!=null)continue ;
var ae7=ae6[i1];
if (!this.ValidateCell(e4,h4,ae7)){
if (e4.getAttribute("lcidMsg")!=null)
alert(e4.getAttribute("lcidMsg"));
else 
alert("Can't set the data into the cell. The data type is not correct for the cell.");
return ;
}
}
aa2++;
}
}
ae2++;
}
if (ae5.length==0)return ;
ae2=this.GetSheetIndex(e4,h1);
for (var f1=0;f1<ae5.length&&ae2<n3;f1++){
aa2=h3;
var ae6=ae5[f1];
var h4=null;
var ac1=this.GetDisplayIndex(e4,ae2);
for (var i1=0;i1<ae4&&aa2<h0;i1++){
if (!this.IsCovered(e4,ac1,aa2,e4.d9)){
h4=this.GetCellFromRowCol(e4,ac1,aa2,h4);
if (e4.getAttribute("LayoutMode")&&e4.copymulticol&&parseInt(h4.parentNode.getAttribute("row"))!=parseInt(e4.d1.parentNode.getAttribute("row")))continue ;
if (h4!=null&&h4.parentNode.getAttribute("previewrow")!=null)continue ;
var s9=this.GetCellType(h4);
var ae1=this.GetRender(h4);
if (s9!="readonly"&&ae1.getAttribute("FpEditor")!="Button"){
var ae7=null;
if (ae6!=null&&i1<ae6.length)ae7=ae6[i1];
this.SetCellValueFromView(h4,ae7);
if (ae7!=null){
this.SetCellValue(e4,h4,""+ae7);
}else {
this.SetCellValue(e4,h4,"");
}
}
}
aa2++;
}
ae2++;
}
var x7=e4.getAttribute("autoCalc");
if (x7!="false"){
this.UpdateValues(e4);
}
var e9=this.GetTopSpread(e4);
var g1=document.getElementById(e9.id+"_textBox");
if (g1!=null){
g1.blur();
}
this.Focus(e4);
this.SizeSpread(e4);
}
this.UpdateValues=function (e4){
if (e4.d8==null&&this.GetParentSpread(e4)==null&&e4.getAttribute("rowFilter")!="true"&&e4.getAttribute("hierView")!="true"&&e4.getAttribute("IsNewRow")!="true"){
this.SaveData(e4);
this.StorePostData(e4);
this.SyncData(e4.getAttribute("name"),"UpdateValues",e4);
}
}
this.ValidateCell=function (e4,h4,x2){
if (h4==null||x2==null||x2=="")return true;
var x5=null;
var s4=this.GetCellType(h4);
if (s4!=null){
var i3=this.GetFunction(s4+"_isValid");
if (i3!=null){
x5=i3(h4,x2);
}
}
return (x5==null||x5=="");
}
this.DoclearSelection=function (e4){
var n7=this.GetSelection(e4);
var n8=n7.lastChild;
while (n8!=null){
var h1=e4.getAttribute("LayoutMode")?parseInt(n8.getAttribute("rowIndex")):this.GetRowByKey(e4,n8.getAttribute("row"));
var h3=e4.getAttribute("LayoutMode")?parseInt(n8.getAttribute("colIndex")):this.GetColByKey(e4,n8.getAttribute("col"));
var n3=parseInt(n8.getAttribute("rowcount"));
var h0=parseInt(n8.getAttribute("colcount"));
if (e4.getAttribute("LayoutMode")&&h1!=-1&&(h0==-1||e4.getAttribute("OperationMode")!="Normal")){
n3=parseInt(e4.getAttribute("layoutrowcount"));
this.PaintSelection(e4,h1,-1,n3,-1,false);
}
if (e4.getAttribute("LayoutMode")&&h3!=-1&&(n3==-1||e4.getAttribute("OperationMode")!="Normal")){
var i9=this.GetRowTemplateRowFromGroupCell(e4,parseInt(n8.getAttribute("col")));
var h4=this.GetCellByRowCol2(e4,i9,parseInt(n8.getAttribute("col")));
if (h4){
h1=parseInt(h4.parentNode.getAttribute("row"));
h3=this.GetColFromCell(e4,h4);
}
this.PaintMultipleRowSelection(e4,h1,h3,1,h0,false);
}
else 
this.PaintSelection(e4,h1,h3,n3,h0,false);
n7.removeChild(n8);
n8=n7.lastChild;
}
}
this.Clear=function (e4){
var p6=this.GetPageActiveSpread();
if (p6!=null&&p6!=e4&&this.GetTopSpread(p6)==e4){
this.Clear(p6);
return ;
}
var s9=this.GetCellType(e4.d1);
if (s9=="readonly")return ;
var n7=this.GetSelection(e4);
var n8=n7.lastChild;
if (this.AnyReadOnlyCell(e4,n8)){
return ;
}
this.Copy(e4);
if (n8!=null){
var h1;
var h3;
var n3;
var h0;
var ae8=false;
if (e4.getAttribute("LayoutMode")&&n8.getAttribute("col")!="-1"&&n8.getAttribute("row")=="-1"&&n8.getAttribute("rowcount")=="-1"){
var h4=e4.d1;
if (h4){
h1=h4.parentNode.getAttribute("row");
h3=this.GetColFromCell(e4,h4);
n3=this.GetRowCountInternal(e4);
h0=parseInt(n8.getAttribute("colcount"));
ae8=true;
}
}
else if (e4.getAttribute("LayoutMode")&&n8.getAttribute("col")=="-1"&&n8.getAttribute("row")!=-1){
var u1=parseInt(n8.getAttribute("row"));
h1=this.GetFirstRowFromKey(e4,u1);
h3=parseInt(n8.getAttribute("colIndex"));
n3=parseInt(e4.getAttribute("layoutrowcount"));
}
else {
h1=e4.getAttribute("LayoutMode")?parseInt(n8.getAttribute("rowIndex")):this.GetRowByKey(e4,n8.getAttribute("row"));
h3=e4.getAttribute("LayoutMode")?parseInt(n8.getAttribute("colIndex")):this.GetColByKey(e4,n8.getAttribute("col"));
n3=parseInt(n8.getAttribute("rowcount"));;
h0=parseInt(n8.getAttribute("colcount"));
}
if (h1<0){
h1=0;
n3=this.GetRowCountInternal(e4);
}
if (h3<0){
h3=0;
h0=this.GetColCount(e4);
}
for (var f1=h1;f1<h1+n3;f1++){
if (this.IsChildSpreadRow(e4,this.GetViewport(e4),f1))continue ;
var h4=null;
for (var i1=h3;i1<h3+h0;i1++){
if (!this.IsCovered(e4,f1,i1,e4.d9)){
h4=this.GetCellFromRowCol(e4,f1,i1,h4);
if (e4.getAttribute("LayoutMode")&&ae8&&(h4==null||(h4.parentNode.getAttribute("row"))!=h1))continue ;
if (h4!=null&&h4.parentNode.getAttribute("previewrow")!=null)continue ;
var s9=this.GetCellType(h4);
if (s9!="readonly"){
var ae9=this.GetEditor(h4);
if (ae9!=null&&ae9.getAttribute("FpEditor")=="Button")continue ;
this.SetCellValueFromView(h4,null);
this.SetCellValue(e4,h4,"");
}
}
}
}
var x7=e4.getAttribute("autoCalc");
if (x7!="false"){
this.UpdateValues(e4);
}
}
}
this.AnyReadOnlyCell=function (e4,n8){
if (n8!=null){
var h1=this.GetRowByKey(e4,n8.getAttribute("row"));
var h3=this.GetColByKey(e4,n8.getAttribute("col"));
var n3=parseInt(n8.getAttribute("rowcount"));
var h0=parseInt(n8.getAttribute("colcount"));
if (h1<0){
h1=0;
n3=this.GetRowCountInternal(e4);
}
if (h3<0){
h3=0;
h0=this.GetColCount(e4);
}
for (var f1=h1;f1<h1+n3;f1++){
if (this.IsChildSpreadRow(e4,this.GetViewport(e4),f1))continue ;
var h4=null;
for (var i1=h3;i1<h3+h0;i1++){
if (!this.IsCovered(e4,f1,i1,e4.d9)){
h4=this.GetCellFromRowCol(e4,f1,i1,h4);
var s9=this.GetCellType(h4);
if (s9=="readonly"){
return true;
}
}
}
}
}
return false;
}
this.GetViewportFromPoint=function (e4,m3,n1){
var x9=l2=0;
var p8=t2=0;
var k3=u7=0;
var r0=h2=0;
var l8=this.GetViewport0(e4);
var l9=this.GetViewport1(e4);
var m0=this.GetViewport2(e4);
var e7=this.GetViewport(e4);
if (l8!=null){
x9=this.GetOffsetLeft(e4,l8,document.body);
k3=l8.offsetWidth;
p8=this.GetOffsetTop(e4,l8,document.body);
r0=l8.offsetHeight;
}
if (m0!=null){
x9=this.GetOffsetLeft(e4,m0,document.body);
k3=m0.offsetWidth;
t2=this.GetOffsetTop(e4,m0,document.body);
h2=m0.offsetHeight;
}
if (l9!=null){
l2=this.GetOffsetLeft(e4,l9,document.body);
u7=l9.offsetWidth;
p8=this.GetOffsetTop(e4,l9,document.body);
r0=l9.offsetHeight;
}
if (e7!=null){
l2=this.GetOffsetLeft(e4,e7,document.body);
u7=e7.offsetWidth;
t2=this.GetOffsetTop(e4,e7,document.body);
h2=e7.offsetHeight;
}
if (x9<m3&&m3<l2){
if (p8<n1&&n1<t2)return l8;
else if (t2<n1&&n1<t2+u7)return m0;
}else if (l2<m3&&m3<l2+u7){
if (p8<n1&&n1<t2)return l9;
else if (t2<n1&&n1<t2+u7)return e7;
}
return null;
}
this.GetCellFromPoint=function (e4,m3,n1,e7){
var r3=this.GetOffsetLeft(e4,e7,document.body);
var x0=this.GetOffsetTop(e4,e7,document.body);
if (m3<r3||n1<x0){
return null;
}else {
var g9=e7.rows;
var af0=null;
for (var h1=0;h1<g9.length;h1++){
var m6=g9[h1];
x0+=m6.offsetHeight;
if (n1<x0){
af0=m6;
break ;
}
}
if (af0!=null){
for (var h3=0;h3<af0.cells.length;h3++){
var af1=af0.cells[h3];
r3+=af1.offsetWidth;
if (m3<r3){
return af1;
}
}
}
}
return null;
}
this.MoveSliderBar=function (e4,g3){
var m5=this.GetElementById(this.activePager,e4.id+"_slideBar");
var g0=(g3.clientX-this.GetOffsetLeft(e4,e4,document.body)+window.scrollX-8);
if (g0<e4.slideLeft)g0=e4.slideLeft;
if (g0>e4.slideRight)g0=e4.slideRight;
var m9=parseInt(this.activePager.getAttribute("totalPage"))-1;
var af2=parseInt(((g0-e4.slideLeft)/(e4.slideRight-e4.slideLeft))*m9)+1;
if (e4.style.position!="absolute"&&e4.style.position!="relative")
g0+=this.GetOffsetLeft(e4,e4,document.body)
m5.style.left=g0+"px";
return af2;
}
this.MouseMove=function (event){
if (window.fpPostOn!=null)return ;
event=this.GetEvent(event);
var o8=this.GetTarget(event);
if (o8!=null&&o8.tagName=="scrollbar")
return ;
if (o8.parentNode!=null&&o8.parentNode.getAttribute("previewrow"))
return ;
var e4=this.GetSpread(o8,true);
if (e4!=null&&this.dragSlideBar)
{
if (this.activePager!=null){
var af2=this.MoveSliderBar(e4,event);
var af3=this.GetElementById(this.activePager,e4.id+"_posIndicator");
af3.innerHTML=this.activePager.getAttribute("pageText")+af2;
}
return ;
}
if (this.a6)e4=this.GetSpread(this.b8);
if (e4==null||(!this.a6&&this.HitCommandBar(o8)))return ;
if (e4.getAttribute("OperationMode")=="ReadOnly")return ;
var j6=this.IsXHTML(e4);
if (this.a6){
if (this.dragCol!=null&&this.dragCol>=0){
var z0=this.GetMovingCol(e4);
if (z0!=null){
if (z0.style.display=="none")z0.style.display="";
if (e4.style.position!="absolute"&&e4.style.position!="relative"){
z0.style.top=""+(event.clientY+window.scrollY)+"px";
z0.style.left=""+(event.clientX+window.scrollX+5)+"px";
}else {
z0.style.top=""+(event.clientY-this.GetOffsetTop(e4,e4,document.body)+window.scrollY)+"px";
z0.style.left=""+(event.clientX-this.GetOffsetLeft(e4,e4,document.body)+window.scrollX+5)+"px";
}
}
var e7=this.GetViewport(e4);
var af4=document.body;
var af5=this.GetGroupBar(e4);
var g0=-1;
var m3=event.clientX;
var x0=0;
var r3=0;
if (e4.style.position!="absolute"&&e4.style.position!="relative"){
x0=this.GetOffsetTop(e4,e4,document.body)-e7.parentNode.scrollTop;
r3=this.GetOffsetLeft(e4,e4,document.body)-e7.parentNode.scrollLeft;
m3+=Math.max(document.body.scrollLeft,document.documentElement.scrollLeft);
}else {
m3-=(this.GetOffsetLeft(e4,e4,document.body)-Math.max(document.body.scrollLeft,document.documentElement.scrollLeft));
}
var af6=false;
var j6=this.IsXHTML(e4);
var af7=j6?document.body.parentNode.scrollTop:document.body.scrollTop;
var m4=document.getElementById(e4.id+"_titleBar");
if (m4)af7-=m4.parentNode.parentNode.offsetHeight;
if (this.GetPager1(e4)!=null)af7-=this.GetPager1(e4).offsetHeight;
if (af5!=null&&event.clientY<this.GetOffsetTop(e4,e4,document.body)-e7.parentNode.scrollTop+af5.offsetHeight-af7){
if (e4.style.position!="absolute"&&e4.style.position!="relative")
r3=this.GetOffsetLeft(e4,e4,document.body);
x0+=10;
af6=true;
var z3=af5.getElementsByTagName("TABLE")[0];
if (z3!=null){
for (var f1=0;f1<z3.rows[0].cells[0].childNodes.length;f1++){
var j3=z3.rows[0].cells[0].childNodes[f1].offsetWidth;
if (j3==null)continue ;
if (r3<=m3&&m3<r3+j3){
g0=f1;
break ;
}
r3+=j3;
}
}
if (g0==-1&&m3>=r3)g0=-2;
e4.targetCol=g0;
}else {
if (e4.style.position=="absolute"||e4.style.position=="relative")
r3=-e7.parentNode.scrollLeft;
if (this.GetRowHeader(e4)!=null)r3+=this.GetRowHeader(e4).offsetWidth;
if (af5!=null)x0+=af5.offsetHeight;
if (m3<r3){
g0=0;
}else {
var z9=e4.selectedCols.context;
if (z9){
for (var f1=0;f1<z9.length;f1++){
if (z9[f1].left+r3<=m3&&m3<z9[f1].left+r3+z9[f1].width){
g0=f1;
}
}
if (this.IsColSelected(e4,g0)){
while (this.IsColSelected(e4,g0)&&this.IsColSelected(e4,g0-1))g0--;
}else {
if (this.IsColSelected(e4,g0-1))g0++;
}
if (g0<0)g0=0;
if (g0>=z9.length)g0=z9.length-1;
r3+=z9[g0].left;
}
}
r3-=5;
var af8=parseInt(this.GetSheetColIndex(e4,g0));
if (af8<0)af8=g0;
e4.targetCol=af8;
}
if (m4)x0+=m4.parentNode.parentNode.offsetHeight;
if (this.GetPager1(e4)!=null)x0+=this.GetPager1(e4).offsetHeight;
var af3=this.GetPosIndicator(e4);
af3.style.left=""+r3+"px";
af3.style.top=""+x0+"px";
if (af5!=null&&af6&&af5.getElementsByTagName("TABLE").length==0){
af3.style.display="none";
}else {
if (af6||e4.allowColMove)
af3.style.display="";
else 
af3.style.display="none";
}
var i6=this.GetParent(this.GetViewport(e4));
if (i6!=null){
var af9=this.GetOffsetLeft(e4,e4,document.body)+i6.offsetLeft+i6.offsetWidth-20;
var ag0=0;
var m0=this.GetViewport2(e4);
if (m0!=null){
ag0=m0.offsetWidth;
af9+=ag0;
}
if (event.clientX>af9){
i6.scrollLeft=i6.scrollLeft+10;
this.ScrollView(e4);
this.UpdatePostbackData(e4);
}else if (event.clientX<this.GetOffsetLeft(e4,e4,document.body)+i6.offsetLeft+ag0+5){
i6.scrollLeft=i6.scrollLeft-10;
this.ScrollView(e4);
this.UpdatePostbackData(e4);
}
}
return ;
}
if (this.b4==null&&this.b5==null){
if (e4.d1!=null){
var i6=this.GetParent(this.GetViewport(e4));
if (i6!=null){
var t2=this.GetOffsetTop(e4,e4,document.body)+i6.offsetTop+i6.offsetHeight-10;
var ag1=0;
var l9=this.GetViewport1(e4);
if (l9!=null){
ag1=this.GetViewport1(e4).offsetHeight;
t2+=ag1;
}
if (event.clientY>t2){
i6.scrollTop=i6.scrollTop+10;
this.ScrollView(e4);
}else if (event.clientY<this.GetOffsetTop(e4,e4,document.body)+i6.offsetTop+ag1+5){
i6.scrollTop=i6.scrollTop-10;
this.ScrollView(e4);
}
var af9=this.GetOffsetLeft(e4,e4,document.body)+i6.offsetLeft+i6.offsetWidth-20;
var ag0=0;
var m0=this.GetViewport2(e4);
if (m0!=null){
ag0=m0.offsetWidth;
af9+=ag0;
}
if (event.clientX>af9){
i6.scrollLeft=i6.scrollLeft+10;
this.ScrollView(e4);
}else if (event.clientX<this.GetOffsetLeft(e4,e4,document.body)+i6.offsetLeft+ag0+5){
i6.scrollLeft=i6.scrollLeft-10;
this.ScrollView(e4);
}
}
var h4=this.GetCell(o8,null,event);
if (h4==null&&o8!=null){
var e7=this.GetViewportFromPoint(e4,event.clientX,event.clientY);
if (e7!=null)
h4=this.GetCellFromPoint(e4,event.clientX,event.clientY,e7);
}
if (h4!=null&&h4!=e4.d2){
var i2=this.GetOperationMode(e4);
if (i2!="MultiSelect"){
if (i2=="SingleSelect"||i2=="RowMode"){
this.ClearSelection(e4);
var h5=this.GetRowFromCell(e4,h4);
this.UpdateAnchorCell(e4,h5,0);
this.SelectRow(e4,h5,1,true,true);
}else {
if (!(i2=="Normal"&&this.GetSelectionPolicy(e4)=="Single")&&!e4.getAttribute("LayoutMode")){
this.Select(e4,e4.d1,h4);
this.SyncColSelection(e4);
}
}
e4.d2=h4;
}
}
}
}else if (this.b4!=null){
var ag2=event.clientX-this.b6;
var aa0=parseInt(this.b4.width)+ag2;
var w9=0;
var ag3=(aa0>w9);
if (ag3){
if (e4.frzRows>0||e4.frzCols>0){
var h7=0;
if (!j6)h7+=parseInt(e4.style.borderWidth);
e4.sizeBar.style.left=(event.clientX-this.GetOffsetLeft(e4,e4,document.body)-h7+window.scrollX)+"px";
}else {
this.b4.width=aa0;
var k1=parseInt(this.b4.getAttribute("index"));
if (this.IsChild(this.b4,this.GetFrozColHeader(e4)))
this.SetWidthFix(this.GetFrozColHeader(e4),k1,aa0);
else 
this.SetWidthFix(this.GetColHeader(e4),k1-e4.frzCols,aa0);
this.b6=event.clientX;
}
}
}else if (this.b5!=null){
var ag2=event.clientY-this.b7;
var ag4=parseInt(this.b5.style.height)+ag2;
var w9=0;
var ag3=(w9<ag4);
if (ag3){
if (e4.frzRows>0||e4.frzCols>0){
var h7=0;
if (!j6)h7+=parseInt(e4.style.borderWidth);
if (e4.style.position=="relative"||e4.style.position=="absolute")
e4.sizeBar.style.top=(event.clientY-this.GetOffsetTop(e4,e4,document.body)-h7+window.scrollY)+"px";
else 
e4.sizeBar.style.top=(event.clientY-h7+window.scrollY)+"px";
}else {
this.b5.style.height=""+(parseInt(this.b5.style.height)+ag2)+"px";
this.b7=event.clientY;
}
}
}
}else {
this.b8=o8;
if (this.b8==null||this.GetSpread(this.b8)!=e4)return ;
var o8=this.GetSizeColumn(e4,this.b8,event);
if (o8!=null){
this.b4=o8;
this.b8.style.cursor=this.GetResizeCursor(false);
}else {
var o8=this.GetSizeRow(e4,this.b8,event);
if (o8!=null){
this.b5=o8;
if (this.b8!=null&&this.b8.style!=null)this.b8.style.cursor=this.GetResizeCursor(true);
}else {
if (this.b8!=null&&this.b8.style!=null){
var h4=this.GetCell(this.b8);
if (h4!=null&&this.IsHeaderCell(e4,h4)){
if (this.b8.getAttribute("FpSpread")=="rowpadding"||this.b8.getAttribute("ControlType")=="chgrayarea")
this.b8.style.cursor=this.GetgrayAreaCursor(e4);
else 
this.b8.style.cursor="default";
}else {
if (this.b8!=null&&this.b8.style!=null&&(this.b8.getAttribute("FpSpread")=="rowpadding"||this.b8.getAttribute("ControlType")=="chgrayarea"))
this.b8.style.cursor=this.GetgrayAreaCursor(e4);
}
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
this.GetResizeCursor=function (i9){
if (i9){
return "n-resize";
}else {
return "w-resize";
}
}
this.HitCommandBar=function (o8){
var g0=o8;
var e4=this.GetTopSpread(this.GetSpread(g0,true));
if (e4==null)return false;
var q9=this.GetCommandBar(e4);
while (g0!=null&&g0!=e4){
if (g0==q9)return true;
g0=g0.parentNode;
}
return false;
}
this.OpenWaitMsg=function (e4){
var i3=document.getElementById(e4.id+"_waitmsg");
if (i3==null)return ;
var j3=e4.offsetWidth;
var j7=e4.offsetHeight;
var j0=this.CreateTestBox(e4);
j0.style.fontFamily=i3.style.fontFamily;
j0.style.fontSize=i3.style.fontSize;
j0.style.fontWeight=i3.style.fontWeight;
j0.style.fontStyle=i3.style.fontStyle;
j0.innerHTML=i3.innerHTML;
i3.style.width=""+(j0.offsetWidth+2)+"px";
var ad6=Math.max(10,(j3-parseInt(i3.style.width))/2);
var g0=Math.max(10,(j7-parseInt(i3.style.height))/2);
if (e4.style.position!="absolute"&&e4.style.position!="relative"){
ad6+=this.GetOffsetLeft(e4,e4,document.body);
g0+=this.GetOffsetTop(e4,e4,document.body);
}
i3.style.top=""+g0+"px";
i3.style.left=""+ad6+"px";
i3.style.display="block";
}
this.CloseWaitMsg=function (e4){
var i3=document.getElementById(e4.id+"_waitmsg");
if (i3==null)return ;
i3.style.display="none";
this.Focus(e4);
}
this.MouseDown=function (event){
if (window.fpPostOn!=null)return ;
event=this.GetEvent(event);
var o8=this.GetTarget(event);
var e4=this.GetSpread(o8,true);
e4.mouseY=event.clientY;
var ag5=this.GetPageActiveSpread();
if (this.GetViewport(e4)==null)return ;
if (e4!=null&&o8.parentNode!=null&&o8.parentNode.getAttribute("name")==e4.id+"_slideBar"){
if (this.IsChild(o8,this.GetPager1(e4)))
this.activePager=this.GetPager1(e4);
else if (this.IsChild(o8,this.GetPager2(e4)))
this.activePager=this.GetPager2(e4);
if (this.activePager!=null){
var p1=true;
if (this.a7)p1=this.EndEdit(e4);
if (p1){
this.UpdatePostbackData(e4);
this.dragSlideBar=true;
}
}
return this.CancelDefault(event);
}
if (this.GetOperationMode(e4)=="ReadOnly")return ;
var j6=false;
if (e4!=null)j6=this.IsXHTML(e4);
if (this.a7&&e4.getAttribute("mcctCellType")!="true"){
var g0=this.GetCell(o8);
if (g0!=e4.d1){
var p1=this.EndEdit();
if (!p1)return ;
}else 
return ;
}
if (o8==this.GetParent(this.GetViewport(e4))){
if (this.GetTopSpread(ag5)!=e4){
this.SetActiveSpread(event);
}
return ;
}
var ag6=(ag5==e4);
this.SetActiveSpread(event);
ag5=this.GetPageActiveSpread();
if (this.HitCommandBar(o8))return ;
if (event.button==2)return ;
if (this.IsChild(o8,this.GetGroupBar(e4))){
var h6=parseInt(o8.id.replace(e4.id+"_group",""));
if (!isNaN(h6)){
this.InitMovingCol(e4,h6,true,o8);
this.a6=true;
e4.dragFromGroupbar=true;
this.CancelDefault(event);
return ;
}
}
if (this.IsInRowEditTemplate(e4,o8)){
return ;
}
this.b4=this.GetSizeColumn(e4,o8,event);
if (this.b4!=null){
this.a6=true;
this.b6=this.b7=event.clientX;
if (this.b4.style!=null)this.b4.style.cursor=this.GetResizeCursor(false);
this.b8=o8;
if (e4.frzRows>0||e4.frzCols>0){
var ag7=this.GetViewport0(e4);
if (ag7==null)ag7=this.GetViewport1(e4);
if (ag7==null)ag7=this.GetViewport(e4);
ag7=ag7.parentNode;
if (this.GetColHeader(e4)!=null)
e4.sizeBar.style.top=""+(this.GetOffsetTop(e4,ag7,e4)-this.GetColHeader(e4).offsetHeight)+"px";
else 
e4.sizeBar.style.top=""+this.GetOffsetTop(e4,ag7,e4)+"px";
var h7=0;
if (!j6)h7+=parseInt(e4.style.borderWidth);
e4.sizeBar.style.left=(this.b6-this.GetOffsetLeft(e4,e4,document.body)-h7+window.scrollX)+"px";
var ag8=0;
if (this.GetViewport0(e4)!=null)ag8=this.GetViewport0(e4).parentNode.offsetHeight;
if (ag8==0&&this.GetViewport1(e4)!=null)ag8=this.GetViewport1(e4).parentNode.offsetHeight;
if (this.GetViewport(e4)!=null)ag8+=this.GetViewport(e4).parentNode.offsetHeight;
if (this.GetColHeader(e4)!=null)ag8+=this.GetColHeader(e4).offsetHeight;
e4.sizeBar.style.height=""+ag8+"px";
e4.sizeBar.style.width="2px";
}
}else {
this.b5=this.GetSizeRow(e4,o8,event);
if (this.b5!=null){
this.a6=true;
this.b6=this.b7=event.clientY;
this.b5.style.cursor=this.GetResizeCursor(true);
this.b8=o8;
if (e4.frzRows>0||e4.frzCols>0){
var ag7=this.GetViewport0(e4);
if (ag7==null)ag7=this.GetViewport1(e4);
if (ag7==null)ag7=this.GetViewport(e4);
ag7=ag7.parentNode;
var h7=0;
if (!j6)h7+=parseInt(e4.style.borderWidth);
if (e4.style.position=="relative"||e4.style.position=="absolute"){
e4.sizeBar.style.left="0px";
e4.sizeBar.style.top=(this.b7-this.GetOffsetTop(e4,e4,document.body)-h7+window.scrollY)+"px";
}else {
e4.sizeBar.style.left=""+this.GetOffsetLeft(e4,e4,document.body)+"px";
e4.sizeBar.style.top=(this.b7-h7+window.scrollY)+"px";
}
e4.sizeBar.style.height="2px";
e4.sizeBar.style.width=""+e4.offsetWidth+"px";
}
}else {
var ag9=this.GetCell(o8,null,event);
if (ag9==null){
var c4=this.GetCorner(e4);
if (c4!=null&&this.IsChild(o8,c4)){
if (this.GetOperationMode(e4)=="Normal")
this.SelectTable(e4,true);
}
return ;
}
var ah0=this.GetColFromCell(e4,ag9);
if (ag9.parentNode.getAttribute("FpSpread")=="ch"&&ah0>=this.GetColCount(e4))return ;
if (ag9.parentNode.getAttribute("FpSpread")=="rh"&&this.IsChildSpreadRow(e4,this.GetViewport(e4),ag9.parentNode.rowIndex))return ;
if (ag9.parentNode.getAttribute("FpSpread")=="ch"&&this.GetOperationMode(e4)!="Normal"){
if (e4.allowColMove||e4.allowGroup){
if (e4.getAttribute("LayoutMode"))ah0=parseInt(ag9.getAttribute("col"));
this.InitMovingCol(e4,ah0);
}
this.a6=true;
this.b8=o8;
return this.CancelDefault(event);
}
if (ag9.parentNode.getAttribute("FpSpread")=="ch"&&(this.GetOperationMode(e4)=="RowMode"||this.GetOperationMode(e4)=="SingleSelect"||this.GetOperationMode(e4)=="ExtendedSelect")){
if (!e4.allowColMove&&!e4.allowGroup)
return ;
}else {
var o9=this.FireActiveCellChangingEvent(e4,this.GetRowFromCell(e4,ag9),ah0,ag9.parentNode.getAttribute("row"));
if (o9)return ;
var n9=this.GetOperationMode(e4);
var e9=this.GetTopSpread(e4);
if (!event.ctrlKey||e4.getAttribute("multiRange")!="true"){
if (n9!="MultiSelect"){
if (!(
(e4.allowColMove||e4.allowGroup)&&ag9.parentNode.getAttribute("FpSpread")=="ch"&&
n9=="Normal"&&(e4.getAttribute("SelectionPolicy")=="Range"||e4.getAttribute("SelectionPolicy")=="MultiRange")&&
e4.selectedCols.length!=0&&this.IsColSelected(e4,ah0)
))
this.ClearSelection(e4);
}
}else {
if (n9!="ExtendedSelect"&&n9!="MultiSelect"){
if (e4.d1!=null)this.PaintSelectedCell(e4,e4.d1,true);
}
}
}
e4.d1=ag9;
var h4=e4.d1;
var aa6=this.GetParent(this.GetViewport(e4));
if (aa6!=null&&!this.IsControl(o8)&&(o8!=null&&o8.tagName!="scrollbar")){
if (this.IsChild(h4,aa6)&&h4.offsetLeft+h4.offsetWidth>aa6.scrollLeft+aa6.clientWidth){
aa6.scrollLeft=h4.offsetLeft+h4.offsetWidth-aa6.clientWidth;
}
if ((this.IsChild(h4,aa6)||this.IsChild(h4,this.GetViewport2(e4)))&&h4.offsetTop+h4.offsetHeight>aa6.scrollTop+aa6.clientHeight&&h4.offsetHeight<aa6.clientHeight){
aa6.scrollTop=h4.offsetTop+h4.offsetHeight-aa6.clientHeight;
}
if (h4.offsetTop<aa6.scrollTop){
aa6.scrollTop=h4.offsetTop;
}
if (h4.offsetLeft<aa6.scrollLeft){
aa6.scrollLeft=h4.offsetLeft;
}
this.ScrollView(e4);
}
if (ag9.parentNode.getAttribute("FpSpread")!="ch")this.SetActiveRow(e4,this.GetRowKeyFromCell(e4,e4.d1));
if (ag9.parentNode.getAttribute("FpSpread")=="rh")
this.SetActiveCol(e4,0);
else {
this.SetActiveCol(e4,e4.getAttribute("LayoutMode")?this.GetColKeyFromCell2(e4,e4.d1):this.GetColKeyFromCell(e4,e4.d1));
}
var n9=this.GetOperationMode(e4);
if (e4.d1.parentNode.getAttribute("FpSpread")=="r"){
if (n9=="ExtendedSelect"||n9=="MultiSelect"){
var ah1=this.IsRowSelected(e4,this.GetRowFromCell(e4,e4.d1));
if (ah1)
this.SelectRow(e4,this.GetRowFromCell(e4,e4.d1),1,false,true);
else 
this.SelectRow(e4,this.GetRowFromCell(e4,e4.d1),1,true,true);
}
else if (n9=="RowMode"||n9=="SingleSelect")
this.SelectRow(e4,this.GetRowFromCell(e4,e4.d1),1,true,true);
else {
this.SelectRange(e4,this.GetRowFromCell(e4,e4.d1),ah0,1,1,true);
}
e4.d5=this.GetRowFromCell(e4,e4.d1);
e4.d6=ah0;
}else if (e4.d1.parentNode.getAttribute("FpSpread")=="ch"){
if (o8.tagName=="INPUT"||o8.tagName=="TEXTAREA"||o8.tagName=="SELECT")
return ;
var u4=ah0;
if (e4.allowColMove||e4.allowGroup)
{
if (n9=="Normal"&&(e4.getAttribute("SelectionPolicy")=="Range"||e4.getAttribute("SelectionPolicy")=="MultiRange")){
if (this.IsColSelected(e4,u4))this.InitMovingCol(e4,u4);
else this.SelectColumn(e4,u4,1,true);
}else {
if (e4.getAttribute("LayoutMode"))
u4=parseInt(e4.d1.getAttribute("col"));
if (n9=="Normal"||n9=="ReadOnly")
this.SelectColumn(e4,u4,1,true);
else e4.selectedCols.push(u4);
this.InitMovingCol(e4,u4);
}
}else {
if (n9=="Normal"||n9=="ReadOnly"){
if (e4.getAttribute("LayoutMode"))
u4=parseInt(e4.d1.getAttribute("col"));
this.SelectColumn(e4,u4,1,true);
}
else 
return ;
}
}else if (e4.d1.parentNode.getAttribute("FpSpread")=="rh"){
if (o8.tagName=="INPUT"||o8.tagName=="TEXTAREA"||o8.tagName=="SELECT")
return ;
if (n9=="ExtendedSelect"||n9=="MultiSelect"){
if (this.IsRowSelected(e4,this.GetRowFromCell(e4,e4.d1)))
this.SelectRow(e4,this.GetRowFromCell(e4,e4.d1),1,false,true);
else 
this.SelectRow(e4,this.GetRowFromCell(e4,e4.d1),1,true,true);
}else {
this.SelectRow(e4,this.GetRowFromCell(e4,e4.d1),1,true);
}
}
if (e4.d1!=null){
var g3=this.CreateEvent("ActiveCellChanged");
g3.cmdID=e4.id;
g3.Row=g3.row=this.GetSheetIndex(e4,this.GetRowFromCell(e4,e4.d1));
g3.Col=g3.col=ah0;
if (e4.getAttribute("LayoutMode"))
g3.InnerRow=g3.innerRow=e4.d1.parentNode.getAttribute("row");
this.FireEvent(e4,g3);
}
e4.d2=e4.d1;
if (e4.d1!=null){
e4.d3=this.GetRowFromCell(e4,e4.d1);
e4.d4=ah0;
}
this.b8=o8;
this.a6=true;
}
}
this.EnableButtons(e4);
if (!this.a7&&this.b5==null&&this.b4==null){
if (e4.d1!=null&&this.IsChild(e4.d1,e4)&&!this.IsHeaderCell(this.GetCell(o8))){
var i4=this.GetEditor(e4.d1);
if (i4!=null){
if (i4.type=="submit")this.SaveData(e4);
this.a7=(i4.type!="button"&&i4.type!="submit");
this.a8=i4;
this.a9=this.GetEditorValue(i4);
if (i4.focus)i4.focus();
}
}
}
if (!this.IsControl(o8)){
if (e4!=null)this.UpdatePostbackData(e4);
return this.CancelDefault(event);
}
}
this.GetMovingCol=function (e4){
var z0=document.getElementById(e4.id+"movingCol");
if (z0==null){
z0=document.createElement("DIV");
z0.style.display="none";
z0.style.position="absolute";
z0.style.top="0px";
z0.style.left="0px";
z0.id=e4.id+"movingCol";
z0.align="center";
e4.insertBefore(z0,null);
if (e4.getAttribute("DragColumnCssClass")!=null)
z0.className=e4.getAttribute("DragColumnCssClass");
else 
z0.style.border="1px solid black";
z0.style.MozOpacity=0.50;
}
return z0;
}
this.IsControl=function (g0){
return (g0!=null&&(g0.tagName=="INPUT"||g0.tagName=="TEXTAREA"||g0.tagName=="SELECT"||g0.tagName=="OPTION"));
}
this.EnableButtons=function (e4){
var s9=this.GetCellType(e4.d1);
var n7=this.GetSelection(e4);
var n8=n7.lastChild;
var v6=e4.getAttribute("OperationMode");
var ah2=v6=="ReadOnly"||v6=="SingleSelect"||s9=="readonly";
if (!ah2){
ah2=this.AnyReadOnlyCell(e4,n8);
}
if (ah2){
var f9=this.GetCmdBtn(e4,"Copy");
this.UpdateCmdBtnState(f9,n8==null);
var f6=this.b9;
f9=this.GetCmdBtn(e4,"Paste");
this.UpdateCmdBtnState(f9,(n8==null||f6==null));
f9=this.GetCmdBtn(e4,"Clear");
this.UpdateCmdBtnState(f9,true);
}else {
var f9=this.GetCmdBtn(e4,"Copy");
this.UpdateCmdBtnState(f9,n8==null);
var f6=this.b9;
f9=this.GetCmdBtn(e4,"Paste");
this.UpdateCmdBtnState(f9,(n8==null||f6==null));
f9=this.GetCmdBtn(e4,"Clear");
this.UpdateCmdBtnState(f9,n8==null);
}
}
this.CellClicked=function (h4){
var e4=this.GetSpread(h4);
if (e4!=null){
this.SaveData(e4);
}
}
this.UpdateCmdBtnState=function (f9,disabled){
if (f9==null)return ;
if (f9.tagName=="INPUT"){
var g0=f9.disabled;
if (g0==disabled)return ;
f9.disabled=disabled;
}else {
var g0=f9.getAttribute("disabled");
if (g0==disabled)return ;
f9.setAttribute("disabled",disabled);
}
if (f9.tagName=="IMG"){
var ah3=f9.getAttribute("disabledImg");
if (disabled&&ah3!=null&&ah3!=""){
if (f9.src.indexOf(ah3)<0)f9.src=ah3;
}else {
var ah4=f9.getAttribute("enabledImg");
if (f9.src.indexOf(ah4)<0)f9.src=ah4;
}
}
}
this.MouseUp=function (event){
if (window.fpPostOn!=null)return ;
event=this.GetEvent(event);
var o8=this.GetTarget(event);
var e4=this.GetSpread(o8,true);
if (e4==null&&!this.a6){
return ;
}
if (this.dragSlideBar&&e4!=null)
{
this.dragSlideBar=false;
if (this.activePager!=null){
var af2=this.MoveSliderBar(e4,event)-1;
this.activePager=null;
this.GotoPage(e4,af2);
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
var v6=this.GetOperationMode(e4);
if (v6=="ReadOnly")return ;
var i3=true;
if (this.a6){
this.a6=false;
if (this.dragCol!=null&&this.dragCol>=0){
var ah5=(this.IsChild(o8,this.GetGroupBar(e4))||o8==this.GetGroupBar(e4));
if (!ah5&&this.GetGroupBar(e4)!=null){
var ah6=event.clientX;
var ah7=event.clientY;
var r3=this.GetOffsetLeft(e4,e4,document.body);
var x0=this.GetOffsetTop(e4,e4,document.body);
var ah8=this.GetGroupBar(e4).offsetWidth;
var ah9=this.GetGroupBar(e4).offsetHeight;
var q8=window.scrollX;
var q7=window.scrollY;
var m4=document.getElementById(e4.id+"_titleBar");
if (m4)q7-=m4.parentNode.parentNode.offsetHeight;
if (this.GetPager1(e4)!=null)q7-=this.GetPager1(e4).offsetHeight;
ah5=(r3<=q8+ah6&&q8+ah6<=r3+ah8&&x0<=q7+ah7&&q7+ah7<=x0+ah9);
}
if (e4.dragFromGroupbar){
if (ah5){
if (e4.targetCol>0)
this.Regroup(e4,this.dragCol,parseInt((e4.targetCol+1)/2));
else 
this.Regroup(e4,this.dragCol,e4.targetCol);
}else {
this.Ungroup(e4,this.dragCol,e4.targetCol);
}
}else {
if (ah5){
if (e4.allowGroup){
if (e4.targetCol>0)
this.Group(e4,this.dragCol,parseInt((e4.targetCol+1)/2));
else 
this.Group(e4,this.dragCol,e4.targetCol);
}
}else if (e4.allowColMove){
if (e4.targetCol!=null){
var g3=this.CreateEvent("ColumnDragMove");
g3.cancel=false;
g3.col=e4.selectedCols;
this.FireEvent(e4,g3);
if (!g3.cancel){
this.MoveCol(e4,this.dragCol,e4.targetCol);
var g3=this.CreateEvent("ColumnDragMoveCompleted");
g3.col=e4.selectedCols;
this.FireEvent(e4,g3);
}
}
}
}
var z0=this.GetMovingCol(e4);
if (z0!=null)
z0.style.display="none";
this.dragCol=-1;
this.dragViewCol=-1;
var af3=this.GetPosIndicator(e4);
if (af3!=null)
af3.style.display="none";
e4.dragFromGroupbar=false;
e4.targetCol=null;
this.b4=this.b5=null;
}
if (this.b4!=null){
if (e4.sizeBar!=null)e4.sizeBar.style.left="-400px";
i3=false;
var ag2=event.clientX-this.b6;
var aa0=parseInt(this.b4.width);
var ai0=aa0;
if (isNaN(aa0))aa0=0;
aa0+=ag2;
if (aa0<1)aa0=1;
var k1=parseInt(this.b4.getAttribute("index"));
var u8=this.GetColGroup(this.GetViewport(e4));
if (this.IsChild(this.b4,this.GetFrozColHeader(e4))){
u8=this.GetColGroup(this.GetViewport0(e4));
if (u8==null)u8=this.GetColGroup(this.GetViewport2(e4));
}
if (u8!=null&&u8.childNodes.length>0){
if (this.IsChild(this.b4,this.GetColHeader(e4)))
ai0=parseInt(u8.childNodes[k1-e4.frzCols].width);
else 
ai0=parseInt(u8.childNodes[k1].width);
}else {
ai0=1;
}
if (this.GetViewport(e4).rules!="rows"){
if (k1==parseInt(this.colCount)-1)ai0-=1;
}
if (aa0!=ai0&&event.clientX!=this.b7)
{
this.SetColWidth(e4,k1,aa0,ai0);
var g3=this.CreateEvent("ColWidthChanged");
g3.col=k1;
g3.width=aa0;
this.FireEvent(e4,g3);
}
this.ScrollView(e4);
this.PaintFocusRect(e4);
}else if (this.b5!=null){
if (e4.sizeBar!=null){e4.sizeBar.style.left="-400px";e4.sizeBar.style.width="2px";}
i3=false;
var ag2=event.clientY-this.b7;
var ag4=this.b5.offsetHeight+ag2;
if (ag4<1){
ag4=1;
ag2=1-this.b5.offsetHeight;
}
this.b5.style.height=""+ag4+"px";
this.b5.style.cursor="auto";
var i6=null;
if (this.IsChild(this.b5,this.GetFrozRowHeader(e4))){
i6=this.GetViewport1(e4);
}else {
i6=this.GetViewport(e4);
}
if (i6.rows.length>=2&&i6.cellSpacing=="0"&&e4.frzRow==0){
if (this.b5.rowIndex==0)
i6.rows[0].style.height=""+(parseInt(this.b5.style.height)-1)+"px";
else 
if (this.b5.rowIndex==i6.rows.length-1)
i6.rows[this.b5.rowIndex].style.height=""+(parseInt(this.b5.style.height)+1)+"px";
else 
i6.rows[this.b5.rowIndex].style.height=this.b5.style.height;
}else {
i6.rows[this.b5.rowIndex].style.height=""+(this.b5.offsetHeight-i6.rows[0].offsetTop)+"px";
}
var ai1=this.GetViewport2(e4);
if (this.IsChild(this.b5,this.GetFrozRowHeader(e4))){
ai1=this.GetViewport0(e4);
}
if (ai1!=null)
ai1.rows[this.b5.rowIndex].style.height=i6.rows[this.b5.rowIndex].style.height;
if (this.IsChild(this.b5,this.GetFrozRowHeader(e4))){
this.GetFrozRowHeader(e4).parentNode.parentNode.parentNode.style.posHeight+=ag2;
}
var r2=this.AddRowInfo(e4,this.b5.getAttribute("FpKey"));
if (r2!=null){
this.SetRowHeight(e4,r2,parseInt(this.b5.style.height));
}
if (this.b6!=event.clientY){
var g3=this.CreateEvent("RowHeightChanged");
g3.row=this.GetRowFromCell(e4,this.b5.cells[0]);
g3.height=this.b5.offsetHeight;
this.FireEvent(e4,g3);
}
var i8=this.GetParentSpread(e4);
if (i8!=null)this.UpdateRowHeight(i8,e4);
var e9=this.GetTopSpread(e4);
this.SizeAll(e9);
this.Refresh(e9);
this.ScrollView(e4);
this.PaintFocusRect(e4);
}else {
}
if (this.b8!=null){
this.b8=null;
}
}
if (i3)i3=!this.IsControl(o8);
if (i3&&this.HitCommandBar(o8))return ;
var ai2=false;
var n7=this.GetSelection(e4);
if (n7!=null){
var n8=n7.firstChild;
var h9=new this.Range();
if (n8!=null){
h9.row=this.GetRowByKey(e4,n8.getAttribute("row"));
h9.col=this.GetColByKey(e4,n8.getAttribute("col"));
h9.rowCount=parseInt(n8.getAttribute("rowcount"));
h9.colCount=parseInt(n8.getAttribute("colcount"));
}
switch (e4.d7){
case "":
var g9=this.GetViewport(e4).rows;
for (var f1=h9.row;f1<h9.row+h9.rowCount&&f1<g9.length;f1++){
if (g9[f1].cells.length>0&&g9[f1].cells[0].firstChild!=null&&g9[f1].cells[0].firstChild.nodeName!="#text"){
if (g9[f1].cells[0].firstChild.getAttribute("FpSpread")=="Spread"){
ai2=true;
break ;
}
}
}
break ;
case "c":
var i6=this.GetViewport(e4);
for (var f1=0;f1<i6.rows.length;f1++){
if (this.IsChildSpreadRow(e4,i6,f1)){
ai2=true;
break ;
}
}
break ;
case "r":
var i6=this.GetViewport(e4);
var u3=h9.rowCount;
for (var f1=h9.row;f1<h9.row+u3&&f1<i6.rows.length;f1++){
if (this.IsChildSpreadRow(e4,i6,f1)){
ai2=true;
break ;
}
}
}
}
if (ai2){
var f9=this.GetCmdBtn(e4,"Copy");
this.UpdateCmdBtnState(f9,true);
f9=this.GetCmdBtn(e4,"Paste");
this.UpdateCmdBtnState(f9,true);
f9=this.GetCmdBtn(e4,"Clear");
this.UpdateCmdBtnState(f9,true);
}
var e9=this.GetTopSpread(e4);
var g1=document.getElementById(e9.id+"_textBox");
if (g1!=null){
g1.style.left=event.clientX-e4.offsetLeft;
}
if (i3)this.Focus(e4);
}
this.UpdateRowHeight=function (i8,child){
var m6=child.parentNode;
while (m6!=null){
if (m6.tagName=="TR")break ;
m6=m6.parentNode;
}
var j6=this.IsXHTML(i8);
if (m6!=null){
var f0=m6.rowIndex;
if (this.GetRowHeader(i8)!=null){
var r0=0;
if (this.GetColHeader(child)!=null)r0=this.GetColHeader(child).offsetHeight;
if (this.GetRowHeader(child)!=null)r0+=this.GetRowHeader(child).offsetHeight;
if (!j6)r0-=this.GetViewport(i8).cellSpacing;
if (this.GetViewport(i8).cellSpacing==0){
this.GetRowHeader(i8).rows[f0].style.height=""+(r0+1)+"px";
if (this.GetParentSpread(i8)!=null){
this.GetRowHeader(i8).parentNode.style.height=""+this.GetRowHeader(i8).offsetHeight+"px";
}
}
else 
this.GetRowHeader(i8).rows[f0].style.height=""+(r0+2)+"px";
this.GetViewport(i8).rows[f0].style.height=""+r0+"px";
child.style.height=""+r0+"px";
}
}
var ai3=this.GetParentSpread(i8);
if (ai3!=null)
this.UpdateRowHeight(ai3,i8);
}
this.MouseOut=function (){
if (!this.a6&&this.b4!=null&&this.b4.style!=null)this.b4.style.cursor="auto";
}
this.KeyDown=function (e4,event){
if (window.fpPostOn!=null)return ;
if (!e4.ProcessKeyMap(event))return ;
if (event.keyCode==event.DOM_VK_SPACE&&e4.d1!=null){
var n9=this.GetOperationMode(e4);
if (n9=="MultiSelect"){
if (this.IsRowSelected(e4,this.GetRowFromCell(e4,e4.d1)))
this.SelectRow(e4,this.GetRowFromCell(e4,e4.d1),1,false,true);
else 
this.SelectRow(e4,this.GetRowFromCell(e4,e4.d1),1,true,true);
return ;
}
}
var i4=false;
if (this.a7&&this.a8!=null){
var ai4=this.GetEditor(this.a8);
i4=(ai4!=null);
}
if (event.keyCode!=event.DOM_VK_LEFT&&event.keyCode!=event.DOM_VK_RIGHT&&event.keyCode!=event.DOM_VK_RETURN&&event.keyCode!=event.DOM_VK_TAB&&(this.a7&&!i4)&&this.a8.tagName=="SELECT")return ;
if (this.a7&&this.a8!=null&&this.a8.getAttribute("MccbId")){
var ai5=eval(this.a8.getAttribute("MccbId")+"_Obj");
if (event.altKey&&event.keyCode==event.DOM_VK_DOWN)return ;
if (ai5!=null&&ai5.getIsDrop!=null&&ai5.getIsDrop())return ;
}
switch (event.keyCode){
case event.DOM_VK_LEFT:
case event.DOM_VK_RIGHT:
if (i4){
var ai6=this.a8.getAttribute("FpEditor");
if (this.a7&&ai6=="ExtenderEditor"){
var ai7=FpExtender.Util.getEditor(this.a8);
if (ai7&&ai7.type!="text")this.EndEdit();
}
if (ai6!="RadioButton"&&ai6!="ExtenderEditor")this.EndEdit();
}
if (!this.a7){
this.NextCell(e4,event,event.keyCode);
}
break ;
case event.DOM_VK_UP:
case event.DOM_VK_DOWN:
case event.DOM_VK_RETURN:
if (this.a8!=null&&this.a8.tagName=="TEXTAREA")return ;
if (event.keyCode!=event.DOM_VK_RETURN&&i4&&this.a7&&this.a8.getAttribute("FpEditor")=="ExtenderEditor"){
var ai8=this.a8.getAttribute("Extenders");
if (ai8&&ai8.indexOf("AutoCompleteExtender")!=-1)return ;
}
if (event.keyCode==event.DOM_VK_RETURN)this.CancelDefault(event);
if (this.a7){
var p1=this.EndEdit();
if (!p1)return ;
}
if (event.keyCode!=event.DOM_VK_RETURN)this.NextCell(e4,event,event.keyCode);
var e9=this.GetTopSpread(e4);
var g1=document.getElementById(e9.id+"_textBox");
if (event.DOM_VK_RETURN==event.keyCode)g1.focus();
break ;
case event.DOM_VK_TAB:
if (this.a7){
var p1=this.EndEdit();
if (!p1)return ;
}
var p0=this.GetProcessTab(e4);
var ai9=(p0=="true"||p0=="True");
if (ai9)this.NextCell(e4,event,event.keyCode);
break ;
case event.DOM_VK_SHIFT:
break ;
case event.DOM_VK_HOME:
case event.DOM_VK_END:
case event.DOM_VK_PAGE_UP:
case event.DOM_VK_PAGE_DOWN:
if (!this.a7){
this.NextCell(e4,event,event.keyCode);
}
break ;
default :
var e5=window.navigator.userAgent;
var x8=(e5.indexOf("Firefox/2.")>=0);
if (x8){
if (event.keyCode==67&&event.ctrlKey&&(!this.a7||i4))this.Copy(e4);
else if (event.keyCode==86&&event.ctrlKey&&(!this.a7||i4))this.Paste(e4);
else if (event.keyCode==88&&event.ctrlKey&&(!this.a7||i4))this.Clear(e4);
else if (!this.a7&&e4.d1!=null&&!this.IsHeaderCell(e4.d1)&&!event.ctrlKey&&!event.altKey){
this.StartEdit(e4,e4.d1);
}
}else {
if (event.charCode==99&&event.ctrlKey&&(!this.a7||i4))this.Copy(e4);
else if (event.charCode==118&&event.ctrlKey&&(!this.a7||i4))this.Paste(e4);
else if (event.charCode==120&&event.ctrlKey&&(!this.a7||i4))this.Clear(e4);
else if (!this.a7&&e4.d1!=null&&!this.IsHeaderCell(e4.d1)&&!event.ctrlKey&&!event.altKey){
this.StartEdit(e4,e4.d1);
}
}
break ;
}
}
this.GetProcessTab=function (e4){
return e4.getAttribute("ProcessTab");
}
this.ExpandRow=function (e4,i9){
var v4=e4.getAttribute("name");
var ab8=(e4.getAttribute("ajax")!="false");
if (ab8)
this.SyncData(v4,"ExpandView,"+i9,e4);
else 
__doPostBack(v4,"ExpandView,"+i9);
}
this.SortColumn=function (e4,column,t1){
var v4=e4.getAttribute("name");
var ab8=(e4.getAttribute("ajax")!="false");
if (ab8)
this.SyncData(v4,"SortColumn,"+column,e4);
else 
__doPostBack(v4,"SortColumn,"+column);
}
this.Filter=function (event,e4){
var o8=this.GetTarget(event);
var g0=o8.value;
if (o8.tagName=="SELECT"){
var ab7=new RegExp("\\s*");
var aj0=new RegExp("\\S*");
var v7=o8[o8.selectedIndex].text;
var aj1="";
var f1=0;
var f0=g0.length;
while (f0>0){
var h5=g0.match(ab7);
if (h5!=null){
aj1+=h5[0];
f1=h5[0].length;
f0-=f1;
g0=g0.substring(f1);
h5=g0.match(aj0);
if (h5!=null){
f1=h5[0].length;
f0-=f1;
g0=g0.substring(f1);
}
}else {
break ;
}
h5=v7.match(aj0);
if (h5!=null){
aj1+=h5[0];
f1=h5[0].length;
v7=v7.substring(f1);
h5=v7.match(ab7);
if (h5!=null){
f1=h5[0].length;
v7=v7.substring(f1);
}
}else {
break ;
}
}
g0=aj1;
}
var ab8=(e4.getAttribute("ajax")!="false");
if (ab8)
this.SyncData(o8.name,g0,e4);
else 
__doPostBack(o8.name,g0);
}
this.MoveCol=function (e4,from,to){
var v4=e4.getAttribute("name");
if (e4.selectedCols&&e4.selectedCols.length>0){
var aj2=[];
for (var f1=0;f1<e4.selectedCols.length;f1++)
aj2[f1]=this.GetSheetColIndex(e4,e4.selectedCols[f1]);
var aj3=aj2.join("+");
this.MoveMultiCol(e4,aj3,to);
return ;
}
var ab8=(e4.getAttribute("ajax")!="false");
if (ab8)
this.SyncData(v4,"MoveCol,"+from+","+to,e4);
else 
__doPostBack(v4,"MoveCol,"+from+","+to);
}
this.MoveMultiCol=function (e4,aj3,to){
var v4=e4.getAttribute("name");
var ab8=(e4.getAttribute("ajax")!="false");
if (ab8)
this.SyncData(v4,"MoveCol,"+aj3+","+to,e4);
else 
__doPostBack(v4,"MoveCol,"+aj3+","+to);
}
this.Group=function (e4,n6,toCol){
var v4=e4.getAttribute("name");
if (e4.selectedCols&&e4.selectedCols.length>0){
var aj2=[];
for (var f1=0;f1<e4.selectedCols.length;f1++)
if (e4.getAttribute("LayoutMode"))
aj2[f1]=parseInt(e4.selectedCols[f1]);
else 
aj2[f1]=this.GetSheetColIndex(e4,e4.selectedCols[f1]);
var aj3=aj2.join("+");
this.GroupMultiCol(e4,aj3,toCol);
e4.selectedCols=[];
return ;
}
var ab8=(e4.getAttribute("ajax")!="false");
if (ab8)
this.SyncData(v4,"Group,"+n6+","+toCol,e4);
else 
__doPostBack(v4,"Group,"+n6+","+toCol);
}
this.GroupMultiCol=function (e4,aj3,toCol){
var v4=e4.getAttribute("name");
var ab8=(e4.getAttribute("ajax")!="false");
if (ab8)
this.SyncData(v4,"Group,"+aj3+","+toCol,e4);
else 
__doPostBack(v4,"Group,"+aj3+","+toCol);
}
this.Ungroup=function (e4,n6,toCol){
var v4=e4.getAttribute("name");
var ab8=(e4.getAttribute("ajax")!="false");
if (ab8)
this.SyncData(v4,"Ungroup,"+n6+","+toCol,e4);
else 
__doPostBack(v4,"Ungroup,"+n6+","+toCol);
}
this.Regroup=function (e4,fromCol,toCol){
var v4=e4.getAttribute("name");
var ab8=(e4.getAttribute("ajax")!="false");
if (ab8)
this.SyncData(v4,"Regroup,"+fromCol+","+toCol,e4);
else 
__doPostBack(v4,"Regroup,"+fromCol+","+toCol);
}
this.ProcessData=function (){
try {
var aj4=this;
aj4.removeEventListener("load",the_fpSpread.ProcessData,false);
var o8=window.srcfpspread;
o8=o8.split(":").join("_");
var aj5=window.fpcommand;
var aj6=document;
var aj7=aj6.getElementById(o8+"_buff");
if (aj7==null){
aj7=aj6.createElement("iframe");
aj7.id=o8+"_buff";
aj7.style.display="none";
aj6.body.appendChild(aj7);
}
var e4=aj6.getElementById(o8);
the_fpSpread.CloseWaitMsg(e4);
if (aj7==null)return ;
var aj8=aj4.responseText;
aj7.contentWindow.document.body.innerHTML=aj8;
var p0=aj7.contentWindow.document.getElementById(o8+"_values");
if (p0!=null){
var v2=p0.getElementsByTagName("data")[0];
var n8=v2.firstChild;
the_fpSpread.error=false;
while (n8!=null){
var h1=the_fpSpread.GetRowByKey(e4,n8.getAttribute("r"));
var h3=the_fpSpread.GetColByKey(e4,n8.getAttribute("c"));
var ac0=the_fpSpread.GetValue(e4,h1,h3);
if (n8.innerHTML!=ac0){
var i3=the_fpSpread.GetFormula(e4,h1,h3);
var j2=the_fpSpread.GetCellByRowCol(e4,h1,h3);
the_fpSpread.SetCellValueFromView(j2,n8.innerHTML,true);
j2.setAttribute("FpFormula",i3);
}
n8=n8.nextSibling;
}
the_fpSpread.ClearCellData(e4);
}else {
the_fpSpread.UpdateSpread(aj6,aj7,o8,aj8,aj5);
}
var ab9=the_fpSpread.GetForm(e4);
ab9.__EVENTTARGET.value="";
ab9.__EVENTARGUMENT.value="";
var ac0=aj6.getElementsByName("__VIEWSTATE")[0];
var g0=aj7.contentWindow.document.getElementsByName("__VIEWSTATE")[0];
if (ac0!=null&&g0!=null)ac0.value=g0.value;
ac0=aj6.getElementsByName("__EVENTVALIDATION");
g0=aj7.contentWindow.document.getElementsByName("__EVENTVALIDATION");
if (ac0!=null&&g0!=null&&ac0.length>0&&g0.length>0)
ac0[0].value=g0[0].value;
aj7.contentWindow.document.location="about:blank";
window.fpPostOn=null;
d8=null;
}catch (g3){
window.fpPostOn=null;
d8=null;
}
var e4=the_fpSpread.GetTopSpread(aj6.getElementById(o8));
var g3=the_fpSpread.CreateEvent("CallBackStopped");
g3.command=aj5;
the_fpSpread.FireEvent(e4,g3);
};
this.UpdateSpread=function (aj6,aj7,o8,aj8,aj5){
var e4=the_fpSpread.GetTopSpread(aj6.getElementById(o8));
var t0=aj7.contentWindow.document.getElementById(e4.id);
if (t0!=null){
if (typeof(Sys)!=='undefined'){
FarPoint.System.ExtenderHelper.saveLoadedExtenderScripts(e4);
}
the_fpSpread.error=(t0.getAttribute("error")=="true");
if (aj5=="LoadOnDemand"&&!the_fpSpread.error){
var aj9=this.GetElementById(e4,e4.id+"_data");
var ak0=this.GetElementById(t0,e4.id+"_data");
if (aj9!=null&&ak0!=null)aj9.setAttribute("data",ak0.getAttribute("data"));
var ak1=t0.getElementsByTagName("style");
if (ak1!=null){
for (var f1=0;f1<ak1.length;f1++){
if (ak1[f1]!=null&&ak1[f1].innerHTML!=null&&ak1[f1].innerHTML.indexOf(e4.id+"msgStyle")<0)
e4.appendChild(ak1[f1].cloneNode(true));
}
}
var ak2=this.GetElementById(e4,e4.id+"_LoadInfo");
var ak3=this.GetElementById(t0,e4.id+"_LoadInfo");
if (ak2!=null&&ak3!=null)ak2.value=ak3.value;
var ak4=false;
var ak5=this.GetElementById(t0,e4.id+"_rowHeader");
if (ak5!=null){
ak5=ak5.firstChild;
ak4=(ak5.rows.length>1);
var j5=this.GetRowHeader(e4);
this.LoadRows(j5,ak5,true);
}
var ak6=this.GetElementById(t0,e4.id+"_viewport2");
if (ak6!=null){
ak4=(ak6.rows.length>0);
var e7=this.GetViewport2(e4);
this.LoadRows(e7,ak6,false);
}
ak6=this.GetElementById(t0,e4.id+"_viewport");
if (ak6!=null){
ak4=(ak6.rows.length>0);
var e7=this.GetViewport(e4);
this.LoadRows(e7,ak6,false);
}
the_fpSpread.Init(e4);
the_fpSpread.LoadScrollbarState(e4);
the_fpSpread.Focus(e4);
if (ak4)
e4.LoadState=null;
else 
e4.LoadState="complete";
if (typeof(Sys)!=='undefined'){
FarPoint.System.ExtenderHelper.loadExtenderScripts(e4,aj7.contentWindow.document);
}
}else {
e4.innerHTML=t0.innerHTML;
the_fpSpread.CopySpreadAttrs(t0,e4);
if (typeof(Sys)!=='undefined'){
FarPoint.System.ExtenderHelper.loadExtenderScripts(e4,aj7.contentWindow.document);
}
var ak7=aj7.contentWindow.document.getElementById(e4.id+"_initScript");
eval(ak7.value);
}
}else {
the_fpSpread.error=true;
var ak8=e4.getAttribute("errorPage");
if (ak8!=null&&ak8.length>0){
window.location.href=ak8;
}
}
}
this.LoadRows=function (e7,ak6,isHeader){
if (e7==null||ak6==null)return ;
var ak9=e7.tBodies[0];
var u3=ak6.rows.length;
var al0=null;
if (isHeader){
u3--;
if (ak9.rows.length>0)al0=ak9.rows[ak9.rows.length-1];
}
for (var f1=0;f1<u3;f1++){
var al1=ak6.rows[f1].cloneNode(false);
ak9.insertBefore(al1,al0);
al1.innerHTML=ak6.rows[f1].innerHTML;
}
if (!isHeader){
for (var f1=0;f1<ak6.parentNode.childNodes.length;f1++){
var ab5=ak6.parentNode.childNodes[f1];
if (ab5!=ak6){
e7.parentNode.insertBefore(ab5.cloneNode(true),null);
}
}
}
}
this.CopySpreadAttr=function (t9,dest,attrName){
var al2=t9.getAttribute(attrName);
var al3=dest.getAttribute(attrName);
if (al2!=null||al3!=null){
if (al2==null)
dest.removeAttribute(attrName);
else 
dest.setAttribute(attrName,al2);
}
}
this.CopySpreadAttrs=function (t9,dest){
this.CopySpreadAttr(t9,dest,"totalRowCount");
this.CopySpreadAttr(t9,dest,"pageCount");
this.CopySpreadAttr(t9,dest,"loadOnDemand");
this.CopySpreadAttr(t9,dest,"allowGroup");
this.CopySpreadAttr(t9,dest,"colMove");
this.CopySpreadAttr(t9,dest,"showFocusRect");
this.CopySpreadAttr(t9,dest,"FocusBorderColor");
this.CopySpreadAttr(t9,dest,"FocusBorderStyle");
this.CopySpreadAttr(t9,dest,"FpDefaultEditorID");
this.CopySpreadAttr(t9,dest,"hierView");
this.CopySpreadAttr(t9,dest,"IsNewRow");
this.CopySpreadAttr(t9,dest,"cmdTop");
this.CopySpreadAttr(t9,dest,"ProcessTab");
this.CopySpreadAttr(t9,dest,"AcceptFormula");
this.CopySpreadAttr(t9,dest,"EditMode");
this.CopySpreadAttr(t9,dest,"AllowInsert");
this.CopySpreadAttr(t9,dest,"AllowDelete");
this.CopySpreadAttr(t9,dest,"error");
this.CopySpreadAttr(t9,dest,"ajax");
this.CopySpreadAttr(t9,dest,"autoCalc");
this.CopySpreadAttr(t9,dest,"multiRange");
this.CopySpreadAttr(t9,dest,"rowFilter");
this.CopySpreadAttr(t9,dest,"OperationMode");
this.CopySpreadAttr(t9,dest,"selectedForeColor");
this.CopySpreadAttr(t9,dest,"selectedBackColor");
this.CopySpreadAttr(t9,dest,"anchorBackColor");
this.CopySpreadAttr(t9,dest,"columnHeaderAutoTextIndex");
this.CopySpreadAttr(t9,dest,"EnableRowEditTemplate");
this.CopySpreadAttr(t9,dest,"scrollContent");
this.CopySpreadAttr(t9,dest,"scrollContentColumns");
this.CopySpreadAttr(t9,dest,"scrollContentTime");
this.CopySpreadAttr(t9,dest,"scrollContentMaxHeight");
this.CopySpreadAttr(t9,dest,"SelectionPolicy");
this.CopySpreadAttr(t9,dest,"ShowHeaderSelection");
this.CopySpreadAttr(t9,dest,"layoutMode");
this.CopySpreadAttr(t9,dest,"layoutRowCount");
dest.tabIndex=t9.tabIndex;
if (dest.style!=null&&t9.style!=null){
if (dest.style.width!=t9.style.width)dest.style.width=t9.style.width;
if (dest.style.height!=t9.style.height)dest.style.height=t9.style.height;
if (dest.style.border!=t9.style.border)dest.style.border=t9.style.border;
}
}
this.Clone=function (m3){
var g0=document.createElement(m3.tagName);
g0.id=m3.id;
var h3=m3.firstChild;
while (h3!=null){
var p8=this.Clone(h3);
g0.appendChild(p8);
h3=h3.nextSibling;
}
return g0;
}
this.FireEvent=function (e4,g3){
if (e4==null||g3==null)return ;
var e9=this.GetTopSpread(e4);
if (e9!=null){
g3.spread=e4;
e9.dispatchEvent(g3);
}
}
this.GetForm=function (e4)
{
var i3=e4.parentNode;
while (i3!=null&&i3.tagName!="FORM")i3=i3.parentNode;
return i3;
}
this.SyncData=function (v4,aj5,e4,asyncCallBack){
if (window.fpPostOn!=null){
return ;
}
this.a7=false;
var g3=this.CreateEvent("CallBackStart");
g3.cancel=false;
g3.command=aj5;
if (asyncCallBack==null)asyncCallBack=false;
g3.async=asyncCallBack;
if (e4==null){
var p8=v4.split(":").join("_");
e4=document.getElementById(p8);
}
if (e4!=null){
var e9=this.GetTopSpread(e4);
this.FireEvent(e4,g3);
}
if (g3.cancel){
the_fpSpread.ClearCellData(e4);
return ;
}
if (aj5!=null&&(aj5.indexOf("SelectView,")==0||aj5=="Next"||aj5=="Prev"||aj5.indexOf("Group,")==0||aj5.indexOf("Page,")==0))
e4.LoadState=null;
var al4=g3.async;
if (al4){
this.OpenWaitMsg(e4);
}
window.fpPostOn=true;
if (this.error)aj5="update";
try {
var ab9=this.GetForm(e4);
if (ab9==null)return ;
ab9.__EVENTTARGET.value=v4;
ab9.__EVENTARGUMENT.value=encodeURIComponent(aj5);
var al5=ab9.action;
var g0;
if (al5.indexOf("?")>-1){
g0="&";
}
else 
{
g0="?";
}
al5=al5+g0;
var f6=this.CollectData(e4);
var aj8="";
var aj4=(window.XMLHttpRequest)?new XMLHttpRequest():new ActiveXObject("Microsoft.XMLHTTP");
if (aj4==null)return ;
aj4.open("POST",al5,al4);
aj4.setRequestHeader("Content-Type","application/x-www-form-urlencoded");
if (e4!=null)
window.srcfpspread=e4.id;
else 
window.srcfpspread=v4;
window.fpcommand=aj5;
this.AttachEvent(aj4,"load",the_fpSpread.ProcessData,false);
aj4.send(f6);
}catch (g3){
window.fpPostOn=false;
d8=null;
}
};
this.CollectData=function (e4){
var ab9=this.GetForm(e4);
var g0;
var g5="fpcallback=true&";
for (var f1=0;f1<ab9.elements.length;f1++){
g0=ab9.elements[f1];
var al6=g0.tagName.toLowerCase();
if (al6=="input"){
var al7=g0.type;
if (al7=="hidden"||al7=="text"||al7=="password"||((al7=="checkbox"||al7=="radio")&&g0.checked)){
g5+=(g0.name+"="+encodeURIComponent(g0.value)+"&");
}
}else if (al6=="select"){
if (g0.childNodes!=null){
for (var i1=0;i1<g0.childNodes.length;i1++){
var r1=g0.childNodes[i1];
if (r1!=null&&r1.tagName!=null&&r1.tagName.toLowerCase()=="option"&&r1.selected){
g5+=(g0.name+"="+encodeURIComponent(r1.value)+"&");
}
}
}
}else if (al6=="textarea"){
g5+=(g0.name+"="+encodeURIComponent(g0.value)+"&");
}
}
return g5;
};
this.ClearCellData=function (e4){
var f6=this.GetData(e4);
var al8=f6.getElementsByTagName("root")[0];
var f7=al8.getElementsByTagName("data")[0];
if (f7==null)return null;
if (e4.d8!=null){
var i9=e4.d8.firstChild;
while (i9!=null){
var h1=i9.getAttribute("key");
var al9=i9.firstChild;
while (al9!=null){
var h3=al9.getAttribute("key");
var am0=f7.firstChild;
while (am0!=null){
var h5=am0.getAttribute("key");
if (h1==h5){
var am1=false;
var am2=am0.firstChild;
while (am2!=null){
var h6=am2.getAttribute("key");
if (h3==h6){
am0.removeChild(am2);
am1=true;
break ;
}
am2=am2.nextSibling;
}
if (am1)break ;
}
am0=am0.nextSibling;
}
al9=al9.nextSibling;
}
i9=i9.nextSibling;
}
}
e4.d8=null;
var f9=this.GetCmdBtn(e4,"Cancel");
if (f9!=null)
this.UpdateCmdBtnState(f9,true);
}
this.StorePostData=function (e4){
var f6=this.GetData(e4);
var f7=f6.getElementsByTagName("root")[0];
var ae7=f7.getElementsByTagName("data")[0];
if (ae7!=null)e4.d8=ae7.cloneNode(true);
}
this.ShowMessage=function (e4,x5,i9,n6,time){
var n3=e4.GetRowCount();
var h0=e4.GetColCount();
if (i9==null||n6==null||i9<0||i9>=n3||n6<0||n6>=h0){
i9=-1;
n6=-1;
}
this.ShowMessageInner(e4,x5,i9,n6,time);
}
this.HideMessage=function (e4,i9,n6){
var n3=e4.GetRowCount();
var h0=e4.GetColCount();
if (i9==null||n6==null||i9<0||i9>=n3||n6<0||n6>=h0)
if (e4.msgList&&e4.msgList.centerMsg&&e4.msgList.centerMsg.msgBox.IsVisible)
e4.msgList.centerMsg.msgBox.Hide();
var am3=this.GetMsgObj(e4,i9,n6);
if (am3&&am3.msgBox.IsVisible){
am3.msgBox.Hide();
}
}
this.ShowMessageInner=function (e4,x5,i9,n6,time){
var am3=this.GetMsgObj(e4,i9,n6);
if (am3){
if (am3.timer)
am3.msgBox.Hide();
}
else 
am3=this.CreateMsgObj(e4,i9,n6);
var am4=am3.msgBox;
am4.Show(e4,this,x5);
if (time&&time>0)
am3.timer=setTimeout(function (){am4.Hide();},time);
this.SetMsgObj(e4,am3);
}
this.GetMsgObj=function (e4,i9,n6){
var am3;
var am5=e4.msgList;
if (am5){
if (i9==-1&&n6==-1)
am3=am5.centerMsg;
else if (i9==-2)
am3=am5.hScrollMsg;
else if (n6==-2)
am3=am5.vScrollMsg;
else {
if (am5[i9])
am3=am5[i9][n6];
}
}
return am3;
}
this.SetMsgObj=function (e4,am3){
var am5=e4.msgList;
if (am3.row==-1&&am3.col==-1)
am5.centerMsg=am3;
else if (am3.row==-2)
am5.hScrollMsg=am3;
else if (am3.col==-2)
am5.vScrollMsg=am3;
else {
if (!am5[am3.row])am5[am3.row]=new Array();
am5[am3.row][am3.col]=am3;
}
}
var am6=null;
this.CreateMsgObj=function (e4,i9,n6){
var am4=document.createElement("div");
var am3={row:i9,col:n6,msgBox:am4};
var am7=null;
if (i9!=-2&&n6!=-2){
am4.style.border="1px solid black";
am4.style.background="yellow";
am4.style.color="red";
}
else {
am4.style.border="1px solid #55678e";
am4.style.fontSize="small";
am4.style.background="#E6E9ED";
am4.style.color="#4c5b7f";
this.GetScrollingContentStyle(e4);
am7=am6;
}
if (am7!=null){
if (am7.fontFamily!=null)
am4.style.fontFamily=am7.fontFamily;
if (am7.fontSize!=null)
am4.style.fontSize=am7.fontSize;
if (am7.fontStyle!=null)
am4.style.fontStyle=am7.fontStyle;
if (am7.fontVariant!=null)
am4.style.fontVariant=am7.fontVariant;
if (am7.fontWeight!=null)
am4.style.fontWeight=am7.fontWeight;
if (am7.backgroundColor!=null)
am4.style.backgroundColor=am7.backgroundColor;
if (am7.color!=null)
am4.style.color=am7.color;
}
am4.style.position="absolute";
am4.style.overflow="hidden";
am4.style.display="block";
am4.style.marginLeft=0;
am4.style.marginTop=2;
am4.style.marginRight=0;
am4.style.marginBottom=0;
am4.msgObj=am3;
am4.Show=function (t0,fpObj,x5){
var y8=fpObj.GetMsgPos(t0,this.msgObj.row,this.msgObj.col);
var e8=fpObj.GetCommandBar(t0);
var am8=fpObj.GetGroupBar(t0);
this.style.visibility="visible";
this.style.display="block";
if (x5){
this.style.left=""+0+"px";
this.style.top=""+0+"px";
this.style.width="auto";
this.innerHTML=x5;
}
var n2=fpObj.GetViewport0(t0);
var f4=fpObj.GetViewport1(t0);
var x8=fpObj.GetViewport2(t0);
var am9=(n2||f4||x8);
var an0=(t0.style.position=="relative"||t0.style.position=="absolute");
var an1=y8.top;
var an2=y8.left;
var r1=e4.offsetParent;
while ((r1.tagName=="TD"||r1.tagName=="TR"||r1.tagName=="TBODY"||r1.tagName=="TABLE")&&r1.style.position!="relative"&&r1.style.position!="absolute")
r1=r1.offsetParent;
if (this.msgObj.row>=0&&this.msgObj.col>=0){
if (!an0&&am9&&r1){
var an3=fpObj.GetLocation(t0);
var an4=fpObj.GetLocation(r1);
an1+=an3.y-an4.y;
an2+=an3.x-an4.x;
if (r1.tagName!=="BODY"){
an1-=fpObj.GetBorderWidth(r1,0);
an2-=fpObj.GetBorderWidth(r1,3);
}
}
var an5=fpObj.GetViewPortByRowCol(t0,this.msgObj.row,this.msgObj.col);
if (!this.parentNode&&an5&&an5.parentNode)an5.parentNode.insertBefore(am4,null);
var j3=this.offsetWidth;
this.style.left=""+an2+"px";
if (!am9&&an5&&an5.parentNode&&an2+j3>an5.offsetWidth)
this.style.width=""+(y8.a5-2)+"px";
else if (parseInt(this.style.width)!=j3)
this.style.width=""+(j3-2)+"px";
if (!am9&&an5!=null&&an1>=an5.offsetHeight-2)an1-=y8.a4+this.offsetHeight;
this.style.top=""+an1+"px";
}
else {
if (!an0&&r1){
var an3=fpObj.GetLocation(t0);
var an4=fpObj.GetLocation(r1);
an1+=an3.y-an4.y;
an2+=an3.x-an4.x;
if (r1.tagName!=="BODY"){
an1-=fpObj.GetBorderWidth(r1,0);
an2-=fpObj.GetBorderWidth(r1,3);
}
}
var an6=20;
if (!this.parentNode)t0.insertBefore(am4,null);
if (this.offsetWidth+an6<t0.offsetWidth)
an2+=(t0.offsetWidth-this.offsetWidth-an6)/(this.msgObj.row==-2?1:2);
else 
this.style.width=""+(t0.offsetWidth-an6)+"px";
if (this.offsetHeight<t0.offsetHeight)
an1+=(t0.offsetHeight-this.offsetHeight)/(this.msgObj.col==-2?1:2);
if (this.msgObj.col==-2){
var an7=fpObj.GetColFooter(t0);
if (an7)an1-=an7.offsetHeight;
var e8=fpObj.GetCommandBar(t0);
if (e8)an1-=e8.offsetHeight;
an1-=an6;
}
this.style.top=""+an1+"px";
this.style.left=""+an2+"px";
}
this.IsVisible=true;
};
am4.Hide=function (){
this.style.visibility="hidden";
this.style.display="none";
this.IsVisible=false;
if (this.msgObj.timer){
clearTimeout(this.msgObj.timer);
this.msgObj.timer=null;
}
this.innerHTML="";
};
return am3;
}
this.GetLocation=function (ele){
if ((ele.window&&ele.window===ele)||ele.nodeType===9)return {x:0,y:0};
var an8=0;
var an9=0;
var ao0=null;
var ao1=null;
var ao2=null;
for (var i8=ele;i8;ao0=i8,ao1=ao2,i8=i8.offsetParent){
var al6=i8.tagName;
ao2=this.GetCurrentStyle2(i8);
if ((i8.offsetLeft||i8.offsetTop)&&
!((al6==="BODY")&&
(!ao1||ao1.position!=="absolute"))){
an8+=i8.offsetLeft;
an9+=i8.offsetTop;
}
if (ao0!==null&&ao2){
if ((al6!=="TABLE")&&(al6!=="TD")&&(al6!=="HTML")){
an8+=parseInt(ao2.borderLeftWidth)||0;
an9+=parseInt(ao2.borderTopWidth)||0;
}
if (al6==="TABLE"&&
(ao2.position==="relative"||ao2.position==="absolute")){
an8+=parseInt(ao2.marginLeft)||0;
an9+=parseInt(ao2.marginTop)||0;
}
}
}
ao2=this.GetCurrentStyle2(ele);
var ao3=ao2?ao2.position:null;
if (!ao3||(ao3!=="absolute")){
for (var i8=ele.parentNode;i8;i8=i8.parentNode){
al6=i8.tagName;
if ((al6!=="BODY")&&(al6!=="HTML")&&(i8.scrollLeft||i8.scrollTop)){
an8-=(i8.scrollLeft||0);
an9-=(i8.scrollTop||0);
ao2=this.GetCurrentStyle2(i8);
if (ao2){
an8+=parseInt(ao2.borderLeftWidth)||0;
an9+=parseInt(ao2.borderTopWidth)||0;
}
}
}
}
return {x:an8,y:an9};
}
var ao4=["borderTopWidth","borderRightWidth","borderBottomWidth","borderLeftWidth"];
var ao5=["borderTopStyle","borderRightStyle","borderBottomStyle","borderLeftStyle"];
var ao6;
this.GetBorderWidth=function (ele,side){
if (!this.GetBorderVisible(ele,side))return 0;
var n4=this.GetCurrentStyle(ele,ao4[side]);
return this.ParseBorderWidth(n4);
}
this.GetBorderVisible=function (ele,side){
return this.GetCurrentStyle(ele,ao5[side])!="none";
}
this.GetWindow=function (ele){
var aj6=ele.ownerDocument||ele.document||ele;
return aj6.defaultView||aj6.parentWindow;
}
this.GetCurrentStyle2=function (ele){
if (ele.nodeType===3)return null;
var j3=this.GetWindow(ele);
if (ele.documentElement)ele=ele.documentElement;
var ao7=(j3&&(ele!==j3))?j3.getComputedStyle(ele,null):ele.style;
return ao7;
}
this.GetCurrentStyle=function (ele,attribute,defaultValue){
var ao8=null;
if (ele){
if (ele.currentStyle){
ao8=ele.currentStyle[attribute];
}
else if (document.defaultView&&document.defaultView.getComputedStyle){
var ao9=document.defaultView.getComputedStyle(ele,null);
if (ao9){
ao8=ao9[attribute];
}
}
if (!ao8&&ele.style.getPropertyValue){
ao8=ele.style.getPropertyValue(attribute);
}
else if (!ao8&&ele.style.getAttribute){
ao8=ele.style.getAttribute(attribute);
}
}
if (!ao8||ao8==""||typeof(ao8)==='undefined'){
if (typeof(defaultValue)!='undefined'){
ao8=defaultValue;
}
else {
ao8=null;
}
}
return ao8;
}
this.ParseBorderWidth=function (n4){
if (!ao6){
var ap0={};
var ap1=document.createElement('div');
ap1.style.visibility='hidden';
ap1.style.position='absolute';
ap1.style.fontSize='1px';
document.body.appendChild(ap1)
var ap2=document.createElement('div');
ap2.style.height='0px';
ap2.style.overflow='hidden';
ap1.appendChild(ap2);
var ap3=ap1.offsetHeight;
ap2.style.borderTop='solid black';
ap2.style.borderTopWidth='thin';
ap0['thin']=ap1.offsetHeight-ap3;
ap2.style.borderTopWidth='medium';
ap0['medium']=ap1.offsetHeight-ap3;
ap2.style.borderTopWidth='thick';
ap0['thick']=ap1.offsetHeight-ap3;
ap1.removeChild(ap2);
document.body.removeChild(ap1);
ao6=ap0;
}
if (n4){
switch (n4){
case 'thin':
case 'medium':
case 'thick':
return ao6[n4];
case 'inherit':
return 0;
}
var ap4=this.ParseUnit(n4);
if (ap4.type!='px')
throw new Error();
return ap4.size;
}
return 0;
}
this.ParseUnit=function (n4){
if (!n4)
throw new Error();
n4=this.Trim(n4).toLowerCase();
var ad6=n4.length;
var t0=-1;
for (var f1=0;f1<ad6;f1++){
var ab5=n4.substr(f1,1);
if ((ab5<'0'||ab5>'9')&&ab5!='-'&&ab5!='.'&&ab5!=',')
break ;
t0=f1;
}
if (t0==-1)
throw new Error();
var al7;
var ap5;
if (t0<(ad6-1))
al7=this.Trim(n4.substring(t0+1));
else 
al7='px';
ap5=parseFloat(n4.substr(0,t0+1));
if (al7=='px'){
ap5=Math.floor(ap5);
}
return {size:ap5,type:al7};
}
this.GetViewPortByRowCol=function (e4,i9,n6){
var n2=this.GetViewport0(e4);
var f4=this.GetViewport1(e4);
var x8=this.GetViewport2(e4);
var n4=this.GetViewport(e4);
var h4=this.GetCellByRowCol(e4,i9,n6);
if (n4!=null&&this.IsChild(h4,n4))
return n4;
else if (x8!=null&&this.IsChild(h4,x8))
return x8;
else if (f4!=null&&this.IsChild(h4,f4))
return f4;
else if (n2!=null&&this.IsChild(h4,n2))
return n2;
return ;
}
this.GetMsgPos=function (e4,i9,n6){
if (i9<0||n6<0){
return {left:0,top:0};
}
else {
var n2=this.GetViewport0(e4);
var f4=this.GetViewport1(e4);
var x8=this.GetViewport2(e4);
var n4=this.GetViewport(e4);
var ap6=this.GetGroupBar(e4);
var m4=document.getElementById(e4.id+"_titleBar");
var h4=this.GetCellByRowCol(e4,i9,n6);
var g0=h4.offsetTop+h4.clientHeight;
var ad6=h4.offsetLeft;
if ((n2!=null||f4!=null)&&(this.IsChild(h4,x8)||this.IsChild(h4,n4))){
if (n2!=null)
g0+=n2.offsetHeight;
else 
g0+=f4.offsetHeight;
}
if ((n2!=null||x8!=null)&&(this.IsChild(h4,f4)||this.IsChild(h4,n4))){
if (n2!=null)
ad6+=n2.offsetWidth;
else 
ad6+=x8.offsetWidth;
}
if (n4!=null&&(n2||f4||x8)){
if (m4)g0+=m4.offsetHeight;
if (ap6)g0+=ap6.offsetHeight;
if (this.GetColHeader(e4))g0+=this.GetColHeader(e4).offsetHeight;
if (this.GetRowHeader(e4))ad6+=this.GetRowHeader(e4).offsetWidth;
}
if (n4!=null&&this.IsChild(h4,n4)){
if (f4||x8)
g0-=n4.parentNode.scrollTop;
if (f4||x8)
ad6-=n4.parentNode.scrollLeft;
}
if (x8!=null&&this.IsChild(h4,x8)){
g0-=x8.parentNode.scrollTop;
}
if (f4!=null&&this.IsChild(h4,f4)){
ad6-=f4.parentNode.scrollLeft;
}
var j7=h4.clientHeight;
var j3=h4.clientWidth;
return {left:ad6,top:g0,a4:j7,a5:j3};
}
}
this.SyncMsgs=function (e4){
if (!e4.msgList)return ;
for (f1 in e4.msgList){
if (e4.msgList[f1].constructor==Array){
for (i1 in e4.msgList[f1]){
if (e4.msgList[f1][i1]&&e4.msgList[f1][i1].msgBox&&e4.msgList[f1][i1].msgBox.IsVisible){
e4.msgList[f1][i1].msgBox.Show(e4,this);
}
}
}
}
}
this.GetCellInfo=function (e4,h1,h3,y8){
var f6=this.GetData(e4);
if (f6==null)return null;
var f7=f6.getElementsByTagName("root")[0];
if (f7==null)return null;
var o1=f7.getElementsByTagName("state")[0];
if (o1==null)return null;
var ap7=o1.getElementsByTagName("cellinfo")[0];
if (ap7==null)return null;
var g0=ap7.firstChild;
while (g0!=null){
if ((g0.getAttribute("r")==""+h1)&&(g0.getAttribute("c")==""+h3)&&(g0.getAttribute("pos")==""+y8))return g0;
g0=g0.nextSibling;
}
return null;
}
this.AddCellInfo=function (e4,h1,h3,y8){
var n8=this.GetCellInfo(e4,h1,h3,parseInt(y8));
if (n8!=null)return n8;
var f6=this.GetData(e4);
var f7=f6.getElementsByTagName("root")[0];
if (f7==null)return null;
var o1=f7.getElementsByTagName("state")[0];
if (o1==null)return null;
var ap7=o1.getElementsByTagName("cellinfo")[0];
if (ap7==null)return null;
if (document.all!=null){
n8=f6.createNode("element","c","");
}else {
n8=document.createElement("c");
n8.style.display="none";
}
n8.setAttribute("r",h1);
n8.setAttribute("c",h3);
n8.setAttribute("pos",y8);
ap7.appendChild(n8);
return n8;
}
this.setCellAttribute=function (e4,h4,attname,x2,noEvent,recalc){
if (h4==null)return ;
var h1=this.GetRowKeyFromCell(e4,h4);
var h3=e4.getAttribute("LayoutMode")?this.GetColKeyFromCell2(e4,h4):this.GetColKeyFromCell(e4,h4);
if (typeof(h1)=="undefined")return ;
var y8=-1;
if (this.IsChild(h4,this.GetCorner(e4)))
y8=0;
else if (this.IsChild(h4,this.GetRowHeader(e4))||this.IsChild(h4,this.GetFrozColHeader(e4)))
y8=1;
else if (this.IsChild(h4,this.GetColHeader(e4))||this.IsChild(h4,this.GetFrozColHeader(e4)))
y8=2;
else if (this.IsChild(h4,this.GetViewport(e4))||this.IsChild(h4,this.GetViewport0(e4))||this.IsChild(h4,this.GetViewport1(e4))||this.IsChild(h4,this.GetViewport2(e4)))
y8=3;
var s6=this.AddCellInfo(e4,h1,h3,y8);
s6.setAttribute(attname,x2);
if (!noEvent){
var g3=this.CreateEvent("DataChanged");
g3.cell=h4;
g3.cellValue=x2;
g3.row=h1;
g3.col=h3;
this.FireEvent(e4,g3);
}
var f9=this.GetCmdBtn(e4,"Update");
if (f9!=null&&f9.getAttribute("disabled")!=null)
this.UpdateCmdBtnState(f9,false);
f9=this.GetCmdBtn(e4,"Cancel");
if (f9!=null&&f9.getAttribute("disabled")!=null)
this.UpdateCmdBtnState(f9,false);
e4.e2=true;
if (recalc){
this.UpdateValues(e4);
}
}
this.updateCellLocked=function (h4,locked){
if (h4==null)return ;
var g0=h4.getAttribute("FpCellType")=="readonly";
if (g0==locked)return ;
var h3=h4.firstChild;
while (h3!=null){
if (typeof(h3.disabled)!="undefined")h3.disabled=locked;
h3=h3.nextSibling;
}
}
this.Cells=function (e4,h1,h3)
{
var ap8=this.GetCellByRowCol(e4,h1,h3);
if (ap8){
ap8.GetValue=function (){
return the_fpSpread.GetValue(e4,h1,h3);
}
ap8.SetValue=function (value){
if (typeof(value)=="undefined")return ;
if (this.parentNode.getAttribute("previewRow")!=null)return ;
the_fpSpread.SetValue(e4,h1,h3,value);
the_fpSpread.SaveClientEditedDataRealTime();
}
ap8.GetBackColor=function (){
if (this.getAttribute("bgColorBak")!=null)
return this.getAttribute("bgColorBak");
return document.defaultView.getComputedStyle(this,"").getPropertyValue("background-color");
}
ap8.SetBackColor=function (value){
if (typeof(value)=="undefined")return ;
this.bgColor=value;
this.setAttribute("bgColorBak",value);
this.style.backgroundColor=value;
the_fpSpread.setCellAttribute(e4,this,"bc",value);
the_fpSpread.SaveClientEditedDataRealTime();
}
ap8.GetForeColor=function (){
return document.defaultView.getComputedStyle(this,"").getPropertyValue("color");
}
ap8.SetForeColor=function (value){
if (typeof(value)=="undefined")return ;
this.style.color=value;
the_fpSpread.setCellAttribute(e4,this,"fc",value);
the_fpSpread.SaveClientEditedDataRealTime();
}
ap8.GetTabStop=function (){
return this.getAttribute("TabStop")!="false";
}
ap8.SetTabStop=function (value){
if (typeof(value)=="undefined")return ;
var ap9=new String(value);
if (ap9.toLocaleLowerCase()=="false"){
this.setAttribute("TabStop","false");
the_fpSpread.setCellAttribute(e4,this,"ts","false");
the_fpSpread.SaveClientEditedDataRealTime();
}else {
this.removeAttribute("TabStop");
}
}
ap8.GetCellType=function (){
var aq0=the_fpSpread.GetCellType2(this);
if (aq0=="text"||aq0=="readonly")
{
aq0=this.getAttribute("CellType2");
}
if (aq0==null)
aq0="GeneralCellType";
return aq0;
}
ap8.GetHAlign=function (){
var aq1=document.defaultView.getComputedStyle(this,"").getPropertyValue("text-Align");
if (aq1==""||aq1=="undefined"||aq1==null){
aq1=this.style.textAlign;
}
if (aq1==""||aq1=="undefined"||aq1==null)
aq1=this.getAttribute("align");
if (aq1=="start")aq1="left";
if (aq1!=null&&aq1.indexOf("-moz")!=-1)aq1=aq1.replace("-moz-","");
return aq1;
}
ap8.SetHAlign=function (value){
if (typeof(value)=="undefined")return ;
this.style.textAlign=typeof(value)=="string"?value:value.Name;
the_fpSpread.setCellAttribute(e4,this,"ha",typeof(value)=="string"?value:value.Name);
the_fpSpread.SaveClientEditedDataRealTime();
}
ap8.GetVAlign=function (){
var aq2=document.defaultView.getComputedStyle(this,"").getPropertyValue("vertical-Align");
if (aq2==""||aq2=="undefined"||aq2==null)
aq2=this.style.verticalAlign;
if (aq2==""||aq2=="undefined"||aq2==null)
aq2=this.getAttribute("valign");
return aq2;
}
ap8.SetVAlign=function (value){
if (typeof(value)=="undefined")return ;
this.style.verticalAlign=typeof(value)=="string"?value:value.Name;
the_fpSpread.setCellAttribute(e4,this,"va",typeof(value)=="string"?value:value.Name);
the_fpSpread.SaveClientEditedDataRealTime();
}
ap8.GetLocked=function (){
if (ap8.GetCellType()=="ButtonCellType"||ap8.GetCellType()=="TagCloudCellType"||ap8.GetCellType()=="HyperLinkCellType")
return ap8.getAttribute("Locked")=="1";
return the_fpSpread.GetCellType(this)=="readonly";
}
ap8.GetFont_Name=function (){
return document.defaultView.getComputedStyle(this,"").getPropertyValue("font-family");
}
ap8.SetFont_Name=function (value){
if (typeof(value)=="undefined")return ;
this.style.fontFamily=value;
the_fpSpread.setCellAttribute(e4,this,"fn",value);
the_fpSpread.SaveClientEditedDataRealTime();
}
ap8.GetFont_Size=function (){
return document.defaultView.getComputedStyle(this,"").getPropertyValue("font-size");
}
ap8.SetFont_Size=function (value){
if (typeof(value)=="undefined")return ;
if (typeof(value)=="number")value+="px";
this.style.fontSize=value;
the_fpSpread.setCellAttribute(e4,this,"fs",value);
the_fpSpread.SizeSpread(e4);
the_fpSpread.SaveClientEditedDataRealTime();
}
ap8.GetFont_Bold=function (){
return document.defaultView.getComputedStyle(this,"").getPropertyValue("font-weight")=="bold"?true:false;
}
ap8.SetFont_Bold=function (value){
if (typeof(value)=="undefined")return ;
this.style.fontWeight=value==true?"bold":"normal";
the_fpSpread.setCellAttribute(e4,this,"fb",new String(value).toLocaleLowerCase());
the_fpSpread.SaveClientEditedDataRealTime();
}
ap8.GetFont_Italic=function (){
return document.defaultView.getComputedStyle(this,"").getPropertyValue("font-style")=="italic"?true:false;
}
ap8.SetFont_Italic=function (value){
if (typeof(value)=="undefined")return ;
this.style.fontStyle=value==true?"italic":"normal";
the_fpSpread.setCellAttribute(e4,this,"fi",new String(value).toLocaleLowerCase());
the_fpSpread.SaveClientEditedDataRealTime();
}
ap8.GetFont_Overline=function (){
return document.defaultView.getComputedStyle(this,"").getPropertyValue("text-decoration").indexOf("overline")>=0?true:false;
}
ap8.SetFont_Overline=function (value){
if (value){
var aq3=new String("overline");
if (document.defaultView.getComputedStyle(this,"").getPropertyValue("text-decoration").indexOf("line-through")>=0)
aq3+=" line-through"
if (document.defaultView.getComputedStyle(this,"").getPropertyValue("text-decoration").indexOf("underline")>=0)
aq3+=" underline"
this.style.textDecoration=aq3;
}
else {
var aq3=new String("");
if (document.defaultView.getComputedStyle(this,"").getPropertyValue("text-decoration").indexOf("line-through")>=0)
aq3+=" line-through"
if (document.defaultView.getComputedStyle(this,"").getPropertyValue("text-decoration").indexOf("underline")>=0)
aq3+=" underline"
if (aq3=="")aq3="none";
this.style.textDecoration=aq3;
}
the_fpSpread.setCellAttribute(e4,this,"fo",new String(value).toLocaleLowerCase());
the_fpSpread.SaveClientEditedDataRealTime();
}
ap8.GetFont_Strikeout=function (){
return document.defaultView.getComputedStyle(this,"").getPropertyValue("text-decoration").indexOf("line-through")>=0?true:false;
}
ap8.SetFont_Strikeout=function (value){
if (value){
var aq3=new String("line-through");
if (document.defaultView.getComputedStyle(this,"").getPropertyValue("text-decoration").indexOf("overline")>=0)
aq3+=" overline"
if (document.defaultView.getComputedStyle(this,"").getPropertyValue("text-decoration").indexOf("underline")>=0)
aq3+=" underline"
this.style.textDecoration=aq3;
}
else {
var aq3=new String("");
if (document.defaultView.getComputedStyle(this,"").getPropertyValue("text-decoration").indexOf("overline")>=0)
aq3+=" overline"
if (document.defaultView.getComputedStyle(this,"").getPropertyValue("text-decoration").indexOf("underline")>=0)
aq3+=" underline"
if (aq3=="")aq3="none";
this.style.textDecoration=aq3;
}
the_fpSpread.setCellAttribute(e4,this,"fk",new String(value).toLocaleLowerCase());
the_fpSpread.SaveClientEditedDataRealTime();
}
ap8.GetFont_Underline=function (){
return document.defaultView.getComputedStyle(this,"").getPropertyValue("text-decoration").indexOf("underline")>=0?true:false;
}
ap8.SetFont_Underline=function (value){
if (value){
var aq3=new String("underline");
if (document.defaultView.getComputedStyle(this,"").getPropertyValue("text-decoration").indexOf("overline")>=0)
aq3+=" overline"
if (document.defaultView.getComputedStyle(this,"").getPropertyValue("text-decoration").indexOf("line-through")>=0)
aq3+=" line-through"
this.style.textDecoration=aq3;
}
else {
var aq3=new String("");
if (document.defaultView.getComputedStyle(this,"").getPropertyValue("text-decoration").indexOf("overline")>=0)
aq3+=" overline"
if (document.defaultView.getComputedStyle(this,"").getPropertyValue("text-decoration").indexOf("line-through")>=0)
aq3+=" line-through"
if (aq3=="")aq3="none";
this.style.textDecoration=aq3;
}
the_fpSpread.setCellAttribute(e4,this,"fu",new String(value).toLocaleLowerCase());
the_fpSpread.SaveClientEditedDataRealTime();
}
return ap8;
}
return null;
}
this.getDomRow=function (e4,h1){
var n3=this.GetRowCount(e4);
if (n3==0)return null;
var h4=this.GetCellByRowCol(e4,h1,0);
if (h4){
var f0=h4.parentNode.rowIndex;
if (f0>=0){
var i9=h4.parentNode.parentNode.rows[f0];
if (this.GetSizable(e4,i9))
return i9;
}
return null;
}
}
this.setRowInfo_RowAttribute=function (e4,h1,attname,x2,recalc){
h1=parseInt(h1);
if (h1<0)return ;
var aq4=this.AddRowInfo(e4,h1);
aq4.setAttribute(attname,x2);
var f9=this.GetCmdBtn(e4,"Update");
if (f9!=null&&f9.getAttribute("disabled")!=null)
this.UpdateCmdBtnState(f9,false);
f9=this.GetCmdBtn(e4,"Cancel");
if (f9!=null&&f9.getAttribute("disabled")!=null)
this.UpdateCmdBtnState(f9,false);
e4.e2=true;
if (recalc){
this.UpdateValues(e4);
}
}
this.Rows=function (e4,h1)
{
var aq5=this.getDomRow(e4,h1);
if (aq5){
aq5.GetHeight=function (){
return the_fpSpread.GetRowHeightInternal(e4,h1);
}
aq5.SetHeight=function (value){
if (typeof(value)=="undefined")return ;
the_fpSpread.SetRowHeight2(e4,h1,parseInt(value));
the_fpSpread.SaveClientEditedDataRealTime();
}
return aq5;
}
return null;
}
this.setColInfo_ColumnAttribute=function (e4,h3,attname,x2,recalc){
h3=parseInt(h3);
if (h3<0)return ;
var aq6=this.AddColInfo(e4,h3);
aq6.setAttribute(attname,x2);
var f9=this.GetCmdBtn(e4,"Update");
if (f9!=null&&f9.getAttribute("disabled")!=null)
this.UpdateCmdBtnState(f9,false);
f9=this.GetCmdBtn(e4,"Cancel");
if (f9!=null&&f9.getAttribute("disabled")!=null)
this.UpdateCmdBtnState(f9,false);
e4.e2=true;
if (recalc){
this.UpdateValues(e4);
}
}
this.Columns=function (e4,h3)
{
var aq7={a2:this.GetColByKey(e4,parseInt(h3))};
if (aq7){
aq7.GetWidth=function (){
return the_fpSpread.GetColWidthFromCol(e4,h3);
}
aq7.SetWidth=function (value){
if (typeof(value)=="undefined")return ;
the_fpSpread.SetColWidth(e4,h3,value);
the_fpSpread.SaveClientEditedDataRealTime();
}
return aq7;
}
return null;
}
this.GetTitleBar=function (e4){
try {
if (document.getElementById(e4.id+"_title")==null)return null;
var aq8=document.getElementById(e4.id+"_titleBar");
if (aq8!=null)aq8=document.getElementById(e4.id+"_title");
return aq8;
}
catch (ex){
return null;
}
}
this.CheckTitleInfo=function (e4){
var f6=this.GetData(e4);
if (f6==null)return null;
var f7=f6.getElementsByTagName("root")[0];
if (f7==null)return null;
var aq9=f7.getElementsByTagName("titleinfo")[0];
if (aq9==null)return null;
return aq9;
}
this.AddTitleInfo=function (e4){
var n8=this.CheckTitleInfo(e4);
if (n8!=null)return n8;
var f6=this.GetData(e4);
var f7=f6.getElementsByTagName("root")[0];
if (f7==null)return null;
if (document.all!=null){
n8=f6.createNode("element","titleinfo","");
}else {
n8=document.createElement("titleinfo");
n8.style.display="none";
}
f7.appendChild(n8);
return n8;
}
this.setTitleInfo_Attribute=function (e4,attname,x2,recalc){
var ar0=this.AddTitleInfo(e4);
ar0.setAttribute(attname,x2);
var f9=this.GetCmdBtn(e4,"Update");
if (f9!=null&&f9.getAttribute("disabled")!=null)
this.UpdateCmdBtnState(f9,false);
f9=this.GetCmdBtn(e4,"Cancel");
if (f9!=null&&f9.getAttribute("disabled")!=null)
this.UpdateCmdBtnState(f9,false);
e4.e2=true;
if (recalc){
this.UpdateValues(e4);
}
}
this.GetTitleInfo=function (e4)
{
var ar1=this.GetTitleBar(e4);
if (ar1){
ar1.GetHeight=function (){
return document.defaultView.getComputedStyle(this,"").getPropertyValue("height");
}
ar1.SetHeight=function (value){
this.style.height=parseInt(value)+"px";
the_fpSpread.setTitleInfo_Attribute(e4,"ht",value);
var e9=the_fpSpread.GetTopSpread(e4);
the_fpSpread.SizeAll(e9);
the_fpSpread.Refresh(e9);
the_fpSpread.SaveClientEditedDataRealTime();
}
ar1.GetVisible=function (){
return (document.defaultView.getComputedStyle(this,"").getPropertyValue("display")=="none")?false:true;
return document.defaultView.getComputedStyle(this,"").getPropertyValue("visibility");
}
ar1.SetVisible=function (value){
this.style.display=value?"":"none";
this.style.visibility=value?"visible":"hidden";
the_fpSpread.setTitleInfo_Attribute(e4,"vs",new String(value).toLocaleLowerCase());
var e9=the_fpSpread.GetTopSpread(e4);
the_fpSpread.SizeAll(e9);
the_fpSpread.Refresh(e9);
the_fpSpread.SaveClientEditedDataRealTime();
}
ar1.GetValue=function (){
return this.textContent;
}
ar1.SetValue=function (value){
this.textContent=""+value;
the_fpSpread.setTitleInfo_Attribute(e4,"tx",value);
the_fpSpread.SaveClientEditedDataRealTime();
}
return ar1;
}
return null;
}
this.SaveClientEditedDataRealTime=function (){
var ar2=this.GetPageActiveSpread();
if (ar2!=null){
this.SaveData(ar2);
ar2.e2=false;
}
ar2=this.GetPageActiveSheetView();
if (ar2!=null){
this.SaveData(ar2);
ar2.e2=false;
}
}
var ar3="";
this.ShowScrollingContent=function (e4,hs){
var s7="";
var p5=this.GetTopSpread(e4);
var ar4=p5.getAttribute("scrollContentColumns");
var ar5=p5.getAttribute("scrollContentMaxHeight");
var ar6=p5.getAttribute("scrollContentTime");
var i6=this.GetViewport(p5);
var ar7=this.GetColGroup(i6);
var n4=this.GetParent(i6);
var ar8=0;
if (hs){
var ar9=n4.scrollLeft;
var c6=this.GetColHeader(p5);
var u4=0;
for (;u4<ar7.childNodes.length;u4++){
var h3=ar7.childNodes[u4];
ar8+=h3.offsetWidth;
if (ar8>ar9)break ;
}
var as0=this.GetViewport2(p5);
if (as0)u4+=this.GetColGroup(as0).childNodes.length;
if (c6){
var t8=c6.rows.length-1;
if (e4.getAttribute("LayoutMode")==null)
t8=c6.getAttribute("ColTextIndex")?c6.getAttribute("ColTextIndex"):c6.rows.length-1;
var as1=this.GetHeaderCellFromRowCol(p5,t8,u4,true);
if (as1){
if (as1.getAttribute("FpCellType")=="ExtenderCellType"&&as1.getElementsByTagName("DIV").length>0){
var z7=this.GetEditor(as1);
var z8=this.GetFunction("ExtenderCellType_getEditorValue");
if (z7!==null&&z8!==null){
s7="&nbsp;Column:&nbsp;"+z8(z7)+"&nbsp;";
}
}
else 
s7="&nbsp;Column:&nbsp;"+as1.innerHTML+"&nbsp;";
}
}
if (s7.length<=0)s7="&nbsp;Column:&nbsp;"+(u4+1)+"&nbsp;"
}
else {
var o1=n4.scrollTop;
var c5=this.GetRowHeader(p5);
var t8=0;
var as2=0;
for (var aa8=0;aa8<i6.rows.length;aa8++){
var h1=i6.rows[aa8];
ar8+=h1.offsetHeight;
if (ar8>o1){
if (h1.getAttribute("fpkey")==null&&!h1.getAttribute("previewrow"))
t8--;
else 
as2=h1.offsetHeight;
break ;
}
if (h1.getAttribute("fpkey")!=null||h1.getAttribute("previewrow")){
t8++;
as2=h1.offsetHeight;
}
}
var as0=this.GetViewport1(p5);
if (as0)t8+=as0.rows.length;
if (e4.getAttribute("LayoutMode")==null&&ar4!=null&&ar4.length>0){
as2=as2>ar5?ar5:as2;
var as3=ar4.split(",");
var as4=false;
for (var f1=0;f1<as3.length;f1++){
var h3=parseInt(as3[f1]);
if (h3==null||h3>=this.GetColCount(e4))continue ;
var h4=p5.GetCellByRowCol(t8,h3);
if (!h4||h4.getAttribute("col")!=null&&h4.getAttribute("col")!=h3)continue ;
var as5=(h4.getAttribute("group")==1);
var af2=(h4.parentNode.getAttribute("previewrow")!=null);
var g3=(h4.getAttribute("RowEditTemplate")!=null);
var j6=this.IsXHTML(e4);
if (!j6&&ar3==""){
this.GetScrollingContentStyle(e4);
if (am6!=null){
if (am6.fontFamily!=null&&am6.fontFamily!="")ar3+="fontFamily:"+am6.fontFamily+";";
if (am6.fontSize!=null&&am6.fontSize!="")ar3+="fontSize:"+am6.fontSize+";";
if (am6.fontStyle!=null&&am6.fontStyle!="")ar3+="fontStyle:"+am6.fontStyle+";";
if (am6.fontVariant!=null&&am6.fontVariant!="")ar3+="fontVariant:"+am6.fontVariant+";";
if (am6.fontWeight!=null&&am6.fontWeight!="")ar3+="fontWeight:"+am6.fontWeight+";";
if (am6.backgroundColor!=null&&am6.backgroundColor!="")ar3+="backgroundColor:"+am6.backgroundColor+";";
if (am6.color!=null&&am6.color!="")ar3+="color:"+am6.color;
}
}
if (!as4){
s7+="<table cellPadding='0' cellSpacing='0' style='height:"+as2+"px;"+(as5?"":"table-layout:fixed;")+ar3+"'><tr>";
}
s7+="<td style='width:"+(as5?0:h4.offsetWidth)+"px;'>";
if (as5)
s7+="&nbsp;<i>GroupBar:</i>&nbsp;"+h4.textContent+"&nbsp;";
else if (af2)
s7+="&nbsp;<i>PreviewRow:</i>&nbsp;"+h4.textContent+"&nbsp;";
else if (g3){
var as6=this.parseCell(e4,h4);
s7+="&nbsp;<i>RowEditTemplate:</i>&nbsp;"+as6+"&nbsp;"
}
else {
if (h4.getAttribute("fpcelltype"))this.UpdateCellTypeDOM(h4);
if (h4.getAttribute("fpcelltype")=="MultiColumnComboBoxCellType"&&h4.childNodes[0]&&h4.childNodes[0].childNodes.length>0&&h4.childNodes[0].getAttribute("MccbId"))
s7+=p5.GetValue(t8,h3);
else if (h4.getAttribute("fpcelltype")=="RadioButtonListCellType"||h4.getAttribute("fpcelltype")=="ExtenderCellType"||h4.getAttribute("fpeditorid")!=null){
var as7=this.parseCell(e4,h4);
s7+=as7;
}
else 
s7+=h4.innerHTML;
}
s7+="</td>";
as4=true;
if (as5||af2||g3)break ;
}
if (as4)
s7+="</tr></table>";
}
if (s7.length<=0&&c5){
var u4=this.GetColGroup(c5).childNodes.length-1;
if (e4.getAttribute("LayoutMode")==null)
u4=c5.getAttribute("RowTextIndex")?parseInt(c5.getAttribute("RowTextIndex")):this.GetColGroup(c5).childNodes.length-1;
var i9=this.GetDisplayIndex(e4,t8);
var as1=this.GetHeaderCellFromRowCol(e4,i9,u4,false);
if (as1)s7="&nbsp;Row:&nbsp;"+as1.textContent+"&nbsp;";
}
if (s7.length<=0){
var n3=(e4.getAttribute("layoutrowcount")!=null)?parseInt(e4.getAttribute("layoutrowcount")):1;
s7="&nbsp;Row:&nbsp;"+(parseInt(t8/n3)+1)+"&nbsp;";
}
}
this.ShowMessageInner(p5,s7,(hs?-1:-2),(hs?-2:-1),ar6);
}
this.parseCell=function (e4,h4){
var s7=h4.innerHTML;
var p5=this.GetTopSpread(e4);
var as8=p5.id;
if (s7.length>0){
s7=s7.replace(new RegExp("=\""+as8,"g"),"=\""+as8+"src");
s7=s7.replace(new RegExp("name="+as8,"g"),"name="+as8+"src");
}
return s7;
}
this.UpdateCellTypeDOM=function (h4){
for (var f1=0;f1<h4.childNodes.length;f1++){
if (h4.childNodes[f1].tagName&&(h4.childNodes[f1].tagName=="INPUT"||h4.childNodes[f1].tagName=="SELECT"))
this.UpdateDOM(h4.childNodes[f1]);
if (h4.childNodes[f1].childNodes&&h4.childNodes[f1].childNodes.length>0)
this.UpdateCellTypeDOM(h4.childNodes[f1]);
}
}
this.UpdateDOM=function (inputField){
if (typeof(inputField)=="string"){
inputField=document.getElementById(inputField);
}
if (inputField.type=="select-one"){
for (var f1=0;f1<inputField.options.length;f1++){
if (f1==inputField.selectedIndex){
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
if (am6!=null)return ;
var f0=document.styleSheets.length;
for (var f1=0;f1<f0;f1++){
var as9=document.styleSheets[f1];
for (var i1=0;i1<as9.cssRules.length;i1++){
var at0=as9.cssRules[i1];
if (at0.selectorText=="."+e4.id+"scrollContentStyle"){
am6=at0.style;
break ;
}
}
if (am6!=null)break ;
}
}
}
function CheckBoxCellType_setFocus(h4){
var i4=h4.getElementsByTagName("INPUT");
if (i4!=null&&i4.length>0&&i4[0].type=="checkbox"){
i4[0].focus();
}
}
function CheckBoxCellType_getCheckBoxEditor(h4){
var i4=h4.getElementsByTagName("INPUT");
if (i4!=null&&i4.length>0&&i4[0].type=="checkbox"){
return i4[0];
}
return null;
}
function CheckBoxCellType_isValid(h4,x2){
if (x2==null)return "";
x2=the_fpSpread.Trim(x2);
if (x2=="")return "";
if (x2.toLowerCase()=="true"||x2.toLowerCase()=="false")
return "";
else 
return "invalid value";
}
function CheckBoxCellType_getValue(x6,e4){
return CheckBoxCellType_getEditorValue(x6,e4);
}
function CheckBoxCellType_getEditorValue(x6,e4){
var h4=the_fpSpread.GetCell(x6);
var i4=CheckBoxCellType_getCheckBoxEditor(h4);
if (i4!=null&&i4.checked){
return "true";
}
return "false";
}
function CheckBoxCellType_setValue(x6,x2){
var h4=the_fpSpread.GetCell(x6);
var i4=CheckBoxCellType_getCheckBoxEditor(h4);
if (i4!=null){
i4.checked=(x2!=null&&x2.toLowerCase()=="true");
return ;
}
}
function IntegerCellType_getValue(x6){
var g0=x6;
while (g0.firstChild!=null&&g0.firstChild.nodeName!="#text")g0=g0.firstChild;
if (g0.innerHTML=="&nbsp;")return "";
var v7=g0.innerHTML;
x6=the_fpSpread.GetCell(x6);
if (x6.getAttribute("FpRef")!=null)x6=document.getElementById(x6.getAttribute("FpRef"));
var at1=x6.getAttribute("groupchar");
if (at1==null)at1=",";
var w3=v7.length;
while (true){
v7=v7.replace(at1,"");
if (v7.length==w3)break ;
w3=v7.length;
}
if (v7.charAt(0)=='('&&v7.charAt(v7.length-1)==')'){
var at2=x6.getAttribute("negsign");
if (at2==null)at2="-";
v7=at2+v7.substring(1,v7.length-1);
}
v7=the_fpSpread.ReplaceAll(v7,"&nbsp;"," ");
return v7;
}
function IntegerCellType_isValid(h4,x2){
if (x2==null||x2.length==0)return "";
x2=x2.replace(" ","");
if (x2.length==0)return "";
var ar8=h4;
var at3=h4.getAttribute("FpRef");
if (at3!=null)ar8=document.getElementById(at3);
var at2=ar8.getAttribute("negsign");
var y8=ar8.getAttribute("possign");
if (at2!=null)x2=x2.replace(at2,"-");
if (y8!=null)x2=x2.replace(y8,"+");
if (x2.charAt(x2.length-1)=="-")x2="-"+x2.substring(0,x2.length-1);
var w5=new RegExp("^\\s*[-\\+]?\\d+\\s*$");
var p1=(x2.match(w5)!=null);
if (p1)p1=!isNaN(x2);
if (p1){
var w9=ar8.getAttribute("MinimumValue");
var j1=ar8.getAttribute("MaximumValue");
var w8=parseInt(x2);
if (w9!=null){
w9=parseInt(w9);
p1=(!isNaN(w9)&&w8>=w9);
}
if (p1&&j1!=null){
j1=parseInt(j1);
p1=(!isNaN(j1)&&w8<=j1);
}
}
if (!p1){
if (ar8.getAttribute("error")!=null)
return ar8.getAttribute("error");
else 
return "Integer";
}
return "";
}
function DoubleCellType_isValid(h4,x2){
if (x2==null||x2.length==0)return "";
var ar8=h4;
if (h4.getAttribute("FpRef")!=null)ar8=document.getElementById(h4.getAttribute("FpRef"));
var at4=ar8.getAttribute("decimalchar");
if (at4==null)at4=".";
var at1=ar8.getAttribute("groupchar");
if (at1==null)at1=",";
x2=the_fpSpread.Trim(x2);
var p1=true;
p1=(x2.length==0||x2.charAt(0)!=at1);
if (p1){
var f0=x2.indexOf(at4);
if (f0>=0){
f0=x2.indexOf(at1,f0);
p1=(f0<0);
}
}
if (p1){
var w3=x2.length;
while (true){
x2=x2.replace(at1,"");
if (x2.length==w3)break ;
w3=x2.length;
}
}
if (x2.length==0){
p1=false;
}else if (p1){
var at2=ar8.getAttribute("negsign");
var y8=ar8.getAttribute("possign");
var w9=ar8.getAttribute("MinimumValue");
var j1=ar8.getAttribute("MaximumValue");
p1=the_fpSpread.IsDouble(x2,at4,at2,y8,w9,j1);
}
if (!p1){
if (ar8.getAttribute("error")!=null)
return ar8.getAttribute("error");
else 
return "Double";
}
return "";
}
function DoubleCellType_getValue(x6){
var g0=x6;
while (g0.firstChild!=null&&g0.firstChild.nodeName!="#text")g0=g0.firstChild;
if (g0.innerHTML=="&nbsp;")return "";
var v7=g0.innerHTML;
x6=the_fpSpread.GetCell(x6);
if (x6.getAttribute("FpRef")!=null)x6=document.getElementById(x6.getAttribute("FpRef"));
var at1=x6.getAttribute("groupchar");
if (at1==null)at1=",";
var w3=v7.length;
while (true){
v7=v7.replace(at1,"");
if (v7.length==w3)break ;
w3=v7.length;
}
if (v7.charAt(0)=='('&&v7.charAt(v7.length-1)==')'){
var at2=x6.getAttribute("negsign");
if (at2==null)at2="-";
v7=at2+v7.substring(1,v7.length-1);
}
v7=the_fpSpread.ReplaceAll(v7,"&nbsp;"," ");
return v7;
}
function CurrencyCellType_isValid(h4,x2){
if (x2!=null&&x2.length>0){
var ar8=h4;
if (h4.getAttribute("FpRef")!=null)ar8=document.getElementById(h4.getAttribute("FpRef"));
var w2=ar8.getAttribute("currencychar");
if (w2==null)w2="$";
x2=x2.replace(w2,"");
var at1=ar8.getAttribute("groupchar");
if (at1==null)at1=",";
var at4=ar8.getAttribute("decimalchar");
if (at4==null)at4=".";
x2=the_fpSpread.Trim(x2);
var p1=true;
p1=(x2.length==0||x2.charAt(0)!=at1);
if (p1){
var f0=x2.indexOf(at4);
if (f0>=0){
f0=x2.indexOf(at1,f0);
p1=(f0<0);
}
}
if (p1){
var w3=x2.length;
while (true){
x2=x2.replace(at1,"");
if (x2.length==w3)break ;
w3=x2.length;
}
}
var p1=true;
if (x2.length==0){
p1=false;
}else if (p1){
var at2=ar8.getAttribute("negsign");
var y8=ar8.getAttribute("possign");
var w9=ar8.getAttribute("MinimumValue");
var j1=ar8.getAttribute("MaximumValue");
p1=the_fpSpread.IsDouble(x2,at4,at2,y8,w9,j1);
}
if (!p1){
if (ar8.getAttribute("error")!=null)
return ar8.getAttribute("error");
else 
return "Currency ("+w2+"100"+at4+"10) ";
}
}
return "";
}
function CurrencyCellType_getValue(x6){
var g0=x6;
while (g0.firstChild!=null&&g0.firstChild.nodeName!="#text")g0=g0.firstChild;
if (g0.innerHTML=="&nbsp;")return "";
var v7=g0.innerHTML;
x6=the_fpSpread.GetCell(x6);
if (x6.getAttribute("FpRef")!=null)x6=document.getElementById(x6.getAttribute("FpRef"));
var w2=x6.getAttribute("currencychar");
if (w2!=null){
var at5=document.createElement("SPAN");
at5.innerHTML=w2;
w2=at5.innerHTML;
}
if (w2==null)w2="$";
var at1=x6.getAttribute("groupchar");
if (at1==null)at1=",";
v7=v7.replace(w2,"");
var w3=v7.length;
while (true){
v7=v7.replace(at1,"");
if (v7.length==w3)break ;
w3=v7.length;
}
var at2=x6.getAttribute("negsign");
if (at2==null)at2="-";
if (v7.charAt(0)=='('&&v7.charAt(v7.length-1)==')'){
v7=at2+v7.substring(1,v7.length-1);
}
v7=the_fpSpread.ReplaceAll(v7,"&nbsp;"," ");
return v7;
}
function RegExpCellType_isValid(h4,x2){
if (x2==null||x2=="")
return "";
var ar8=h4;
if (h4.getAttribute("FpRef")!=null)ar8=document.getElementById(h4.getAttribute("FpRef"));
var at6=new RegExp(ar8.getAttribute("fpexpression"));
var w6=x2.match(at6);
var n4=(w6!=null&&w6.length>0&&x2==w6[0]);
if (!n4){
if (ar8.getAttribute("error")!=null)
return ar8.getAttribute("error");
else 
return "invalid";
}
return "";
}
function PercentCellType_getValue(x6){
var g0=x6;
while (g0.firstChild!=null&&g0.firstChild.nodeName!="#text")g0=g0.firstChild;
if (g0.innerHTML=="&nbsp;")return "";
g0=g0.innerHTML;
var h4=the_fpSpread.GetCell(x6);
var ar8=h4;
if (h4.getAttribute("FpRef")!=null)ar8=document.getElementById(h4.getAttribute("FpRef"));
var at7=ar8.getAttribute("percentchar");
if (at7==null)at7="%";
g0=g0.replace(at7,"");
var at1=ar8.getAttribute("groupchar");
if (at1==null)at1=",";
var w3=g0.length;
while (true){
g0=g0.replace(at1,"");
if (g0.length==w3)break ;
w3=g0.length;
}
var at2=ar8.getAttribute("negsign");
var y8=ar8.getAttribute("possign");
g0=the_fpSpread.ReplaceAll(g0,"&nbsp;"," ");
var g5=g0;
if (at2!=null)
g0=g0.replace(at2,"-");
if (y8!=null)
g0=g0.replace(y8,"+");
var at4=ar8.getAttribute("decimalchar");
if (at4!=null)
g0=g0.replace(at4,".");
if (!isNaN(g0))
return g5;
else 
return x6.innerHTML;
}
function PercentCellType_setValue(x6,x2){
var g0=x6;
while (g0.firstChild!=null&&g0.firstChild.nodeName!="#text")g0=g0.firstChild;
x6=g0;
if (x2!=null&&x2!=""){
var ar8=the_fpSpread.GetCell(x6);
if (ar8.getAttribute("FpRef")!=null)ar8=document.getElementById(ar8.getAttribute("FpRef"));
var at7=ar8.getAttribute("percentchar");
if (at7==null)at7="%";
x2=x2.replace(" ","");
x2=x2.replace(at7,"");
x6.innerHTML=x2+at7;
}else {
x6.innerHTML="";
}
}
function PercentCellType_isValid(h4,x2){
if (x2!=null){
var ar8=the_fpSpread.GetCell(h4);
if (ar8.getAttribute("FpRef")!=null)ar8=document.getElementById(ar8.getAttribute("FpRef"));
var at7=ar8.getAttribute("percentchar");
if (at7==null)at7="%";
x2=x2.replace(at7,"");
var at1=ar8.getAttribute("groupchar");
if (at1==null)at1=",";
var w3=x2.length;
while (true){
x2=x2.replace(at1,"");
if (x2.length==w3)break ;
w3=x2.length;
}
var at8=x2;
var at2=ar8.getAttribute("negsign");
var y8=ar8.getAttribute("possign");
if (at2!=null)x2=x2.replace(at2,"-");
if (y8!=null)x2=x2.replace(y8,"+");
var at4=ar8.getAttribute("decimalchar");
if (at4!=null)
x2=x2.replace(at4,".");
var p1=!isNaN(x2);
if (p1){
var at9=ar8.getAttribute("MinimumValue");
var au0=ar8.getAttribute("MaximumValue");
if (at9!=null||au0!=null){
var w9=parseFloat(at9);
var j1=parseFloat(au0);
p1=!isNaN(w9)&&!isNaN(j1);
if (p1){
if (at4==null)at4=".";
p1=the_fpSpread.IsDouble(at8,at4,at2,y8,w9*100,j1*100);
}
}
}
if (!p1){
if (ar8.getAttribute("error")!=null)
return ar8.getAttribute("error");
else 
return "Percent:(ex,10"+at7+")";
}
}
return "";
}
function ListBoxCellType_getValue(x6){
var g0=x6.getElementsByTagName("TABLE");
if (g0.length>0)
{
var g9=g0[0].rows;
for (var i1=0;i1<g9.length;i1++){
var h4=g9[i1].cells[0];
if (h4.selected=="true")
{
var au1=h4;
while (au1.firstChild!=null)au1=au1.firstChild;
var ar8=au1.nodeValue;
return ar8;
}
}
}
return "";
}
function ListBoxCellType_setValue(x6,x2){
var g0=x6.getElementsByTagName("TABLE");
if (g0.length>0)
{
g0[0].style.width=(x6.clientWidth-6)+"px";
var g9=g0[0].rows;
for (var i1=0;i1<g9.length;i1++){
var h4=g9[i1].cells[0];
var au1=h4;
while (au1.firstChild!=null)au1=au1.firstChild;
var ar8=au1.nodeValue;
if (ar8==x2){
h4.selected="true";
if (g0[0].parentNode.getAttribute("selectedBackColor")!="undefined")
h4.style.backgroundColor=g0[0].parentNode.getAttribute("selectedBackColor");
if (g0[0].parentNode.getAttribute("selectedForeColor")!="undefined")
h4.style.color=g0[0].parentNode.getAttribute("selectedForeColor");
}else {
h4.style.backgroundColor="";
h4.style.color="";
h4.selected="";
h4.bgColor="";
}
}
}
}
function TextCellType_getValue(x6){
var h4=the_fpSpread.GetCell(x6,true);
if (h4!=null&&h4.getAttribute("password")!=null){
if (h4!=null&&h4.getAttribute("value")!=null)
return h4.getAttribute("value");
else 
return "";
}else {
var g0=x6;
while (g0.firstChild!=null&&g0.firstChild.nodeName!="#text")g0=g0.firstChild;
if (g0.innerHTML=="&nbsp;")return "";
if (g0!=null)g0=the_fpSpread.HTMLDecode(g0.innerHTML);
g0=the_fpSpread.ReplaceAll(g0,"<br>","\n");
return g0;
}
}
function TextCellType_setValue(x6,x2){
var h4=the_fpSpread.GetCell(x6,true);
if (h4==null)return ;
var g0=x6;
while (g0.firstChild!=null&&g0.firstChild.nodeName!="#text")g0=g0.firstChild;
x6=g0;
if (h4.getAttribute("password")!=null){
if (x2!=null&&x2!=""){
x2=x2.replace(" ","");
x6.innerHTML="";
for (var f1=0;f1<x2.length;f1++)
x6.innerHTML+="*";
h4.setAttribute("value",x2);
}else {
x6.innerHTML="";
h4.setAttribute("value","");
}
}else {
if (x2!=null)x2=the_fpSpread.HTMLEncode(x2);
x2=the_fpSpread.ReplaceAll(x2,"\n","<br>");
x2=the_fpSpread.ReplaceAll(x2," ","&nbsp;");
x6.innerHTML=x2;
}
}
function TextCellType_setEditorValue(g0,x2){
if (x2!=null)x2=the_fpSpread.HTMLDecode(x2);
g0.value=x2;
}
function RadioButtonListCellType_getValue(x6){
var h4=the_fpSpread.GetCell(x6,true);
if (h4==null)return ;
var au2=h4.getElementsByTagName("INPUT");
for (var f1=0;f1<au2.length;f1++){
if (au2[f1].tagName=="INPUT"&&au2[f1].checked){
return au2[f1].value;
}
}
return "";
}
function RadioButtonListCellType_getEditorValue(x6){
return RadioButtonListCellType_getValue(x6);
}
function RadioButtonListCellType_setValue(x6,x2){
var h4=the_fpSpread.GetCell(x6,true);
if (h4==null)return ;
if (x2!=null)x2=the_fpSpread.Trim(x2);
var au2=h4.getElementsByTagName("INPUT");
for (var f1=0;f1<au2.length;f1++){
if (au2[f1].tagName=="INPUT"&&x2==the_fpSpread.Trim(au2[f1].value)){
au2[f1].checked=true;
break ;
}else {
if (au2[f1].checked)au2[f1].checked=false;
}
}
}
function RadioButtonListCellType_setFocus(x6){
var h4=the_fpSpread.GetCell(x6,true);
if (h4==null)return ;
var i4=h4.getElementsByTagName("INPUT");
if (i4==null)return ;
for (var f1=0;f1<i4.length;f1++){
if (i4[f1].type=="radio"&&i4[f1].checked){
i4[f1].focus();
return ;
}
}
}
function MultiColumnComboBoxCellType_setValue(x6,x2,e4){
var h4=the_fpSpread.GetCell(x6,true);
if (h4==null)return ;
var au3=h4.getElementsByTagName("DIV");
if (au3!=null&&au3.length>0){
var au4=h4.getElementsByTagName("input");
if (au4!=null&&au4.length>0)
au4[0].value=x2;
return ;
}
if (x2!=null&&x2!="")
x6.textContent=x2;
else 
x6.innerHTML="&nbsp;";
}
function MultiColumnComboBoxCellType_getValue(x6,e4){
var v7=x6.textContent;
var j2=the_fpSpread.GetCell(x6,true);
var au3=j2.getElementsByTagName("DIV");
if (au3!=null&&au3.length>0){
var au4=j2.getElementsByTagName("input");
if (au4!=null&&au4.length>0)
return au4[0].value;
return ;
}
if (!e4)return null;
var v8=the_fpSpread.GetCellEditorID(e4,j2);
var a8=null;
if (v8!=null&&typeof(v8)!="undefined"){
a8=the_fpSpread.GetCellEditor(e4,v8,true);
if (a8!=null){
var au5=a8.getAttribute("MccbId");
if (au5){
FarPoint.System.WebControl.MultiColumnComboBoxCellType.CheckInit(au5);
var ai5=eval(au5+"_Obj");
if (ai5!=null&&ai5.SetText!=null){
ai5.SetText(v7);
return v7;
}
}
}
return null;
}
return v7;
}
function MultiColumnComboBoxCellType_getEditorValue(x6,e4){
var h4=the_fpSpread.GetCell(x6,true);
if (h4==null)return ;
var au6=h4.getElementsByTagName("INPUT");
if (au6!=null&&au6.length>0){
var g0=au6[0];
return g0.value;
}
return null;
}
function MultiColumnComboBoxCellType_setFocus(x6){
var h4=the_fpSpread.GetCell(x6);
var e4=the_fpSpread.GetSpread(h4);
if (h4==null)return ;
var au7=h4.getElementsByTagName("DIV");
if (au7!=null&&au7.length>0){
var au5=au7[0].getAttribute("MccbId");
if (au5){
var ai5=eval(au5+"_Obj");
if (ai5!=null&&typeof(ai5.FocusForEdit)!="undefined"){
ai5.FocusForEdit();
}
}
}
}
function TagCloudCellType_getValue(x6,e4){
var v7=x6.textContent;
if (typeof(v7)!="undefined"&&v7!=null&&v7.length>0)
{
v7=the_fpSpread.ReplaceAll(v7,"<br>","");
v7=the_fpSpread.ReplaceAll(v7,"\n","");
v7=the_fpSpread.ReplaceAll(v7,"\t","");
var s8=new RegExp("\xA0","g");
v7=v7.replace(s8,String.fromCharCode(32));
v7=the_fpSpread.HTMLDecode(v7);
}
else 
v7="";
return v7;
}
