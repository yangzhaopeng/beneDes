//
//	Copyright?2005. FarPoint Technologies.	All rights reserved.
//

eval("var FarPoint={};");
FarPoint.System={};
FarPoint.System.CheckBrowserByName=function (browsername,version){
var e3=window.navigator.userAgent;
var e4=false;
var e5=(""+browsername).toLowerCase();
if ((e5.indexOf("ms")>=0)||(e5.indexOf("msie")>=0)||(e5.indexOf("ie")>=0))
e4=(e3.indexOf("MSIE")>=1);
else if ((e5.indexOf("safari")>=0)||(e5.indexOf("apple")>=0))
e4=(e3.indexOf("Safari")>=1);
else if ((e5.indexOf("ff")>=0)||(e5.indexOf("firefox")>=0))
e4=(e3.indexOf("Firefox")>=1);
return e4;
};
FarPoint.System.IsChild=function (parent,child){
if (child==null||parent==null)return false;
var e6=child.parentNode;
while (e6!=null){
if (e6==parent)return true;
e6=e6.parentNode;
}
return false;
};
FarPoint.System.FindElementById=function (ctl,id,ATTRI_ID){
if (ctl==null)return null;
var e7=ctl.getAttribute(ATTRI_ID);
if (e7==null)return null;
return document.getElementById(e7+id);
};
FarPoint.System.GetEvent=function (e){
if (e!=null)return e;
return window.event;
};
FarPoint.System.GetTarget=function (e){
e=FarPoint.System.GetEvent(e);
if (e.target==document&&e.currentTarget!=null)return e.currentTarget;
if (e.target!=null)return e.target;
return e.srcElement;
};
FarPoint.System.CancelDefault=function (e){
if (e.preventDefault!=null){
e.preventDefault();
e.stopPropagation();
}else {
e.cancelBubble=true;
e.returnValue=false;
}
return false;
};
FarPoint.System.GetMouseCoords=function (ev){
if (ev.pageX||ev.pageY){
return {x:ev.pageX,y:ev.pageY};
}
return {
x:ev.clientX+document.body.scrollLeft-document.body.clientLeft,
y:ev.clientY+document.body.scrollTop-document.body.clientTop
};
};
FarPoint.System.GetOffsetTop=function (ctl){
var e8=0;
while (ctl){
if ((ctl.tagName!="HTML")&&(typeof(ctl.tagName)!="undefined"))
e8+=typeof(ctl.offsetTop)!="undefined"?ctl.offsetTop:0-typeof(ctl.scrollTop)!="undefined"?ctl.scrollTop:0;
ctl=ctl.offsetParent;
}
return parseInt(e8);
};
FarPoint.System.GetOffsetLeft=function (ctl){
var e9=0;
while (ctl){
if ((ctl.tagName!="HTML")&&(typeof(ctl.tagName)!="undefined"))
e9+=typeof(ctl.offsetLeft)!="undefined"?ctl.offsetLeft:0-typeof(ctl.scrollLeft)!="undefined"?ctl.scrollLeft:0;
ctl=ctl.offsetParent;
}
return parseInt(e9);
};
FarPoint.System.AttachEvent=function (target,event,handler,useCapture){
if (target==null||event==null||handler==null)return ;
if (target.addEventListener!=null){
target.addEventListener(event,handler,useCapture);
}else if (target.attachEvent!=null){
target.attachEvent("on"+event,handler);
}
};
FarPoint.System.DetachEvent=function (target,event,handler,useCapture){
if (target==null||event==null||handler==null)return ;
if (target.removeEventListener!=null){
target.removeEventListener(event,handler,useCapture);
}else if (target.detachEvent!=null){
target.detachEvent("on"+event,handler);
}
};
FarPoint.System.Track=function (msg){
if (!FarPoint.System.Config.Consts.$FLAG_ISDEBUG)return ;
if (document.getElementById("txtOutput")==null){
var f0=document.createElement("textarea");
f0.id="txtOutput";
f0.style.width="100%";
f0.style.height="100px";
if (f0.style.bottom!=null&&f0.style.right!=null){
f0.style.bottom="0px";
f0.style.right="0px";
}
f0.style.color="#00ff00";
f0.style.position="absolute";
f0.style.backgroundColor="black";
if (FarPoint.System.CheckBrowserByName("IE")){
window.onload=function (){
if (document.all&&document.body.readyState=="complete"){
document.body.appendChild(f0);
}
};
}else {
document.body.appendChild(f0);
}
}
var f1=document.getElementById("txtOutput");
if (f1!=null){
f1.value="&nbsp;&nbsp;"+msg+"\r\n"+f1.value;
}
};
FarPoint.System.Config={};
FarPoint.System.Config.Consts={};
var f2=FarPoint.System.Config.Consts;
f2.$FLAG_ISDEBUG=false;
f2.$LEFT=37;
f2.$RIGHT=39;
f2.$UP=38;
f2.$DOWN=40;
f2.$ENTER=13;
f2.$CANCEL=27;
f2.$PageUp=33;
f2.$PageDown=34;
f2.$Home=36;
f2.$End=35;
FarPoint.System.WebControl={};
FarPoint.System.WebControl.MultiColumnComboBoxCellTypeUtilitis={};
var f3=FarPoint.System.WebControl.MultiColumnComboBoxCellTypeUtilitis;
var f4=f3.Consts={
$ATTRI_MULTICOMBO_PART_TYPE:"MccbPartType",
$ATTRI_LIST_ALIGNMENT:"MccbListAlignment",
$ATTRI_LIST_OFFSET:"MccbListOffset",
$ATTRI_LIST_WIDTH:"MccbListWidth",
$ATTRI_LIST_HEIGHT:"MccbListHEIGHT",
$ATTRI_COLUMN_EDIT:"MccbColumnEdit",
$ATTRI_COLUMN_DATA:"MccbColumnData",
$ATTRI_ID:"MccbId",
$ATTRI_LIST_MIN_HEIGHT:"MccbListMinHeight",
$ATTRI_LIST_MIN_WIDTH:"MccbListMinWidth",
$TYPE_DROPDOWNBUTTON:"DropDownButton",
$ID_BUTTON_OUTSIDE:"_DropDownButtonOutside",
$ID_BUTTON_INSIDE:"_DropDownButtonInside",
$ID_INPUT:"_Input",
$ID_CONTAINER:"_Container",
$ID_CONTAINER_DIV:"_ContainerDiv",
$ID_SPREAD:"_FpSpread",
$ID_STATUS_RESIZE:"_ResizeButton",
$OBJECT_SUFFIX:"_Obj"
};
f3.CloseAll=function (){
var f5=document.body.lastChild;
if (f5!=null&&f5.tagName!=null&&f5.tagName=="DIV"){
if (f5.id!=null&&f5.close&&f5.id.match(new RegExp(f4.$ID_CONTAINER_DIV+"$"))){
f5.close();
}
}
};
FarPoint.System.WebControl.MultiColumnComboBoxCellType=function (mc){
if (mc==null)return null;
var f6=true;
var f7=null;
var f8=false;
var f9=false;
var g0=-1;
var g1=0;
var g2=false;
var g3=0;
var g4=0;
var g5=50;
var g6=200;
var g7=false;
var g8=this;
this.Init=function (){
if (f6){
this.InitSpread();
this.setController();
var g9=0;
while (g9<12){
if (g9==0||g9==1||g9==3){
g9++;
continue ;
}
if (f7[g9]!=null&&f7[g9].event=="SelectionChanged"){
if (FarPoint.System.CheckBrowserByName("IE")){
this.getFpSpread().onSelectionChanged=f7[g9].handler;
g9++;
continue ;
}
}
this.SetHandler(f7,g9,0);
g9++;
}
var h0=this.getControl();
var h1=FarPoint.System.FindElementById(h0,f4.$ID_BUTTON_INSIDE,f4.$ATTRI_ID);
if (h0!=null&&h1!=null){
if (h0.offsetHeight-5>0)
h1.style.height=h0.offsetHeight-5;
}
this.setListWidth(parseInt(this.getControl().getAttribute(f4.$ATTRI_LIST_WIDTH)));
this.setListHeight(parseInt(this.getControl().getAttribute(f4.$ATTRI_LIST_HEIGHT)));
h0.Init=true;
f6=false;
}
}
this.Dispose=function (){
var h2=this.getController();
if (!h2)return ;
var g9=0;
while (f7[g9]!=null){
if (f7[g9].event=="SelectionChanged"){
if (FarPoint.System.CheckBrowserByName("IE")){
this.getFpSpread().onSelectionChanged=null;
g9++;
continue ;
}
}
this.SetHandler(f7,g9,1);
g9++;
}
}
this.getDragOffsetX=function (){
return g3;
}
this.setDragOffsetX=function (value){
g3=value;
}
this.getDragOffsetY=function (){
return g4;
}
this.setDragOffsetY=function (value){
g4=value;
}
this.getStatusBarHeight=function (){
return 13;
}
this.getIsDrag=function (){
return g2;
}
this.setIsDrag=function (value){
var h3=12;
g2=value;
if (g2){
this.SetHandler(f7,h3,0);
}else {
this.SetHandler(f7,h3,1);
}
}
this.getHostSpread=function (){
var h4=this.getFpSpread();
if (h4==null)return null;
var h5=FarPoint.System.CheckBrowserByName("IE")?h4.hostspread:h4.getAttribute("hostspread");
return document.getElementById(h5);
}
this.getControl=function (){
return mc;
}
this.getFpSpread=function (){
var h6=this.getContainer();
if (h6==null)return null;
return h6.getElementsByTagName("div")[0];
}
this.getContainer=function (){
var h0=this.getControl();
if (h0==null)return null;
return FarPoint.System.FindElementById(h0,f4.$ID_CONTAINER,f4.$ATTRI_ID);
}
this.getContainerDiv=function (){
var h0=this.getControl();
if (h0==null)return null;
return FarPoint.System.FindElementById(h0,f4.$ID_CONTAINER_DIV,f4.$ATTRI_ID);
}
this.getInputControl=function (){
var h0=this.getControl();
if (h0==null)return null;
return FarPoint.System.FindElementById(h0,f4.$ID_INPUT,f4.$ATTRI_ID);
}
this.getResizeButton=function (){
var h0=this.getControl();
if (h0==null)return null;
return FarPoint.System.FindElementById(h0,f4.$ID_STATUS_RESIZE,f4.$ATTRI_ID);
}
this.getController=function (){
return f7;
}
this.setController=function (){
if (f7==null){
f7={
1:{target:document,event:"mousedown",handler:function (event){g8.MouseDownOutside(event)},useCapture:false},
3:{target:document,event:"mouseup",handler:function (event){g8.MouseUpOutside(event)},useCapture:false},
12:{target:document,event:"mousemove",handler:function (event){g8.MouseMove(event)},useCapture:false},
4:{target:this.getControl(),event:"mousedown",handler:function (event){g8.MouseDown(event)},useCapture:false},
7:{target:this.getInputControl(),event:"keydown",handler:function (event){g8.OnInputKeyDown(event)},useCapture:FarPoint.System.CheckBrowserByName("IE")?false:true},
6:{target:this.getContainer(),event:"mousedown",handler:function (event){g8.CancelEvent(event)},useCapture:false},
9:{target:this.getFpSpread(),event:"SelectionChanged",handler:function (event){g8.OnSpreadSelectionChanged(event)},useCapture:false},
8:{target:this.getResizeButton(),event:"mousedown",handler:function (event){g8.ResizeButtonMouseDown(event)},useCapture:false}
};
if (FarPoint.System.CheckBrowserByName("IE")?typeof(this.getFpSpread().EnableClientScript)=="undefined":this.getFpSpread().getAttribute("EnableClientScript")==null){
f7[13]={target:FarPoint.System.CheckBrowserByName("IE")?this.getHostSpread():the_fpSpread.GetViewport(this.getHostSpread()).parentNode,event:"scroll",handler:function (event){g8.MccbctScroll(event)},useCapture:false};
}
}
}
this.getIsDrop=function (){
return f8;
}
this.setIsDrop=function (value){
f8=value;
}
this.getIsDroping=function (){
return f9;
}
this.setIsDroping=function (value){
f9=value;
}
this.getListAlignment=function (){
try {
var g9=this.getControl().getAttribute(f4.$ATTRI_LIST_ALIGNMENT);
return parseInt(g9);
}catch (exception ){
return 0;
}
}
this.getListOffset=function (){
try {
var g9=this.getControl().getAttribute(f4.$ATTRI_LIST_OFFSET);
return parseInt(g9);
}catch (exception ){
return 0;
}
}
this.getListWidth=function (){
return g5;
}
this.setListWidth=function (value){
if ((value<this.getListMinWidth())&&(value!=-1))
g5=this.getListMinWidth();
else {
if (!FarPoint.System.CheckBrowserByName("IE"))
if (value>2000)
value=2000;
g5=value;
}
}
this.getListHeight=function (){
return g6;
}
this.setListHeight=function (value){
if (value<this.getListMinHeight())
g6=this.getListMinHeight();
else 
g6=value;
}
this.getListMinWidth=function (){
try {
var g9=this.getControl().getAttribute(f4.$ATTRI_LIST_MIN_WIDTH);
return Math.min(Math.abs(parseInt(g9)),32767);
}catch (exception ){
return 50;
}
}
this.getListMinHeight=function (){
try {
var g9=this.getControl().getAttribute(f4.$ATTRI_LIST_MIN_HEIGHT);
return Math.min(Math.abs(parseInt(g9)),32767);
}catch (exception ){
return 50;
}
}
this.getEditColumnIndex=function (){
try {
var g9=this.getControl().getAttribute(f4.$ATTRI_COLUMN_EDIT);
return parseInt(g9);
}catch (exception ){
return 0;
}
}
this.setSelectedIndex=function (value){
g0=value;
}
this.getSelectedIndex=function (){
if (g0!=-1){
var h7=this.getFpSpread();
var h8=FarPoint.System.CheckBrowserByName("IE")?parseInt(h7.ActiveRow):parseInt(h7.GetActiveRow());
if (h7){
if (h8>=0){
this.setSelectedIndex(h8);
}
}
}
return g0;
}
this.setActiveColumnIndex=function (value){
g1=value;
}
this.getActiveColumnIndex=function (){
return g1;
}
this.getDataColumnIndex=function (){
try {
var g9=this.getControl().getAttribute(f4.$ATTRI_COLUMN_DATA);
return parseInt(g9);
}catch (exception ){
return 0;
}
}
this.FocusForEdit=function (){
var h0=this.getControl();
if (h0==null)return ;
if (h0.parentNode==null||typeof(h0.parentNode.tagName)=="undefined")return ;
if (h0.parentNode.tagName!="TD")return ;
if (h0.parentNode.getAttribute("FpCellType")!="MultiColumnComboBoxCellType")return ;
var h9=this.getInputControl();
if (h9!=null){
try {
h9.focus();
h9.select();
}catch (exception ){}
this.SetHandler(f7,7,1);
this.SetHandler(f7,7,0);
}
}
this.LockFocus=function (event){
var i0=this.getInputControl();
if (i0!=null&&typeof(i0.focus)!="undefined")
if (i0!=null){
try {
i0.focus();
i0.select();
}catch (exception ){}
}
}
this.MouseDown=function (event){
if (!FarPoint.System.CheckBrowserByName("IE")&&this.getControl().getAttribute("disabled")=="disabled")return FarPoint.System.CancelDefault(event);
if (!this.getIsDrop()&&event.button!=(FarPoint.System.CheckBrowserByName("IE")?1:0))return ;
var i1=FarPoint.System.GetTarget(event);
if (i1==null||i1.getAttribute(f4.$ATTRI_MULTICOMBO_PART_TYPE)!=f4.$TYPE_DROPDOWNBUTTON)return false;
this.setIsDroping(true);
var i2=this;
setTimeout(function (){i2.DropDown();},0);
}
this.DropDown=function (){
this.ShowHideContainer(!this.getIsDrop());
}
this.OnInputKeyDown=function (event){
if (event.altKey&&event.keyCode==f2.$DOWN){
this.ShowHideContainer(!this.getIsDrop());
FarPoint.System.CancelDefault(event);
return false;
}
switch (event.keyCode){
case f2.$UP:
if (this.getIsDrop()){
this.ChangeSelectedIndex(-1);
var i3=this.getFpSpread();
if (!FarPoint.System.CheckBrowserByName("IE")){
the_fpSpread.ScrollTo(i3,i3.GetActiveRow(),this.getActiveColumnIndex());
}else {
i3.ScrollTo(i3.ActiveRow,this.getActiveColumnIndex());
}
if (FarPoint.System.CheckBrowserByName("IE"))
FarPoint.System.CancelDefault(event);
}
if (!FarPoint.System.CheckBrowserByName("IE")){
FarPoint.System.CancelDefault(event);
}
break ;
case f2.$LEFT:
if (this.getIsDrop()){
this.ChangedActiveColumnIndex(true);
FarPoint.System.CancelDefault(event);
}
break ;
case f2.$DOWN:
if (this.getIsDrop()){
this.ChangeSelectedIndex(1);
var i3=this.getFpSpread();
if (!FarPoint.System.CheckBrowserByName("IE")){
the_fpSpread.ScrollTo(i3,i3.GetActiveRow(),this.getActiveColumnIndex());
}else {
i3.ScrollTo(i3.ActiveRow,this.getActiveColumnIndex());
}
if (FarPoint.System.CheckBrowserByName("IE"))
FarPoint.System.CancelDefault(event);
}
if (!FarPoint.System.CheckBrowserByName("IE")){
FarPoint.System.CancelDefault(event);
}
break ;
case f2.$RIGHT:
if (this.getIsDrop()){
this.ChangedActiveColumnIndex(false);
FarPoint.System.CancelDefault(event);
}
break ;
case f2.$ENTER:
if (this.getIsDrop()){
this.ShowHideContainer(false);
FarPoint.System.CancelDefault(event);
}
if (!FarPoint.System.CheckBrowserByName("safari")&&this.getFpSpread().getAttribute("EnableClientScript")=="0"){
return FarPoint.System.CancelDefault(event);
}
break ;
case f2.$CANCEL:
if (this.getIsDrop()){
this.ShowHideContainer(false);
}
FarPoint.System.CancelDefault(event);
break ;
case f2.$PageUp:
if (this.getIsDrop()){
this.ChangeSelectedIndex(null,1);
var i3=this.getFpSpread();
if (!FarPoint.System.CheckBrowserByName("IE")){
the_fpSpread.ScrollTo(i3,i3.GetActiveRow(),this.getActiveColumnIndex());
}else {
i3.ScrollTo(i3.ActiveRow,this.getActiveColumnIndex());
}
if (FarPoint.System.CheckBrowserByName("IE"))
FarPoint.System.CancelDefault(event);
}
if (!FarPoint.System.CheckBrowserByName("IE")){
FarPoint.System.CancelDefault(event);
}
break ;
case f2.$PageDown:
if (this.getIsDrop()){
this.ChangeSelectedIndex(null,2);
var i3=this.getFpSpread();
if (!FarPoint.System.CheckBrowserByName("IE")){
the_fpSpread.ScrollTo(i3,i3.GetActiveRow(),this.getActiveColumnIndex());
}else {
i3.ScrollTo(i3.ActiveRow,this.getActiveColumnIndex());
}
if (FarPoint.System.CheckBrowserByName("IE"))
FarPoint.System.CancelDefault(event);
}
if (!FarPoint.System.CheckBrowserByName("IE")){
FarPoint.System.CancelDefault(event);
}
break ;
case f2.$Home:
if (this.getIsDrop()){
this.ChangeSelectedIndex(null,3);
var i3=this.getFpSpread();
if (!FarPoint.System.CheckBrowserByName("IE")){
the_fpSpread.ScrollTo(i3,i3.GetActiveRow(),this.getActiveColumnIndex());
}else {
i3.ScrollTo(i3.ActiveRow,this.getActiveColumnIndex());
}
if (FarPoint.System.CheckBrowserByName("IE"))
FarPoint.System.CancelDefault(event);
}
if (!FarPoint.System.CheckBrowserByName("IE")){
FarPoint.System.CancelDefault(event);
}
break ;
case f2.$End:
if (this.getIsDrop()){
this.ChangeSelectedIndex(null,4);
var i3=this.getFpSpread();
if (!FarPoint.System.CheckBrowserByName("IE")){
the_fpSpread.ScrollTo(i3,i3.GetActiveRow(),this.getActiveColumnIndex());
}else {
i3.ScrollTo(i3.ActiveRow,this.getActiveColumnIndex());
}
if (FarPoint.System.CheckBrowserByName("IE"))
FarPoint.System.CancelDefault(event);
}
if (!FarPoint.System.CheckBrowserByName("IE")){
FarPoint.System.CancelDefault(event);
}
break ;
}
}
this.OnSpreadSelectionChanged=function (event){
var h7=this.getFpSpread();
if (h7==null)return ;
if (!FarPoint.System.CheckBrowserByName("IE")){
this.setSelectedIndex(h7.GetActiveRow());
}else {
this.setSelectedIndex(h7.ActiveRow);
}
var h9=this.getInputControl();
if (h9==null)return ;
if (this.getSelectedIndex()>=0&&this.getEditColumnIndex()>=0){
h9.value=h7.GetValue(this.getSelectedIndex(),this.getEditColumnIndex());
h9.select();
}
FarPoint.System.GetEvent(event).cancelBubble=true;
}
this.CancelEvent=function (event){
if (this.getIsDrop()){
this.LockFocus(event);
var i4=this;
setTimeout(function (){i4.LockFocus(event);},0);
}
return FarPoint.System.CancelDefault(event);
}
this.MouseDownOutside=function (event){
if (this.getIsDrag())
this.setIsDrag(false);
var i5=this.getContainerDiv();
var i6=this.getControl();
var i3=this.getFpSpread();
var i7=document.getElementById(i3.id+"_viewport");
var i1=FarPoint.System.GetTarget(event);
if (!FarPoint.System.IsChild(i5,i1)&&!FarPoint.System.IsChild(i6,i1)){
this.setIsDroping(false);
if (this.getIsDrop())
this.ShowHideContainer(false);
}
var i4=this;
setTimeout(function (){i4.LockFocus(event);},0);
}
this.MouseUpOutside=function (event){
if (this.getIsDroping()){
this.setIsDroping(false);
return ;
}
if (this.getIsDrop()==false)return ;
if (this.getIsDrag()){
this.setIsDrag(false);
return ;
}
var i5=this.getContainerDiv();
var i3=this.getFpSpread();
var i7=document.getElementById(i3.id+"_viewport");
var i1=FarPoint.System.GetTarget(event);
if (FarPoint.System.IsChild(i7,i1)||!FarPoint.System.IsChild(i5,i1)){
this.ShowHideContainer(false);
}
var i4=this;
setTimeout(function (){i4.LockFocus(event);},0);
}
this.ResizeButtonMouseDown=function (event){
this.LockFocus(event);
if (event.button!=(FarPoint.System.CheckBrowserByName("IE")?1:0))return ;
var h6=this.getContainer();
if (h6==null)return ;
var i8=FarPoint.System.GetMouseCoords(event);
this.setDragOffsetX(parseInt(h6.offsetLeft+h6.offsetWidth)-i8.x);
this.setDragOffsetY(parseInt(h6.offsetTop+h6.offsetHeight)-i8.y);
this.setIsDrag(true);
if (event.preventDefault)event.preventDefault();
event.returnValue=false;
event.cancelBubble=true;
return false;
}
this.MouseMove=function (event){
if (!this.getIsDrag())return ;
var h6=this.getContainer();
if (h6==null)return ;
var i9=this.getContainerDiv();
if (i9==null)return ;
var i8=FarPoint.System.GetMouseCoords(event);
var j0=i8.x-parseInt(h6.offsetLeft)+this.getDragOffsetX();
var j1=i8.y-parseInt(h6.offsetTop)+this.getDragOffsetY()-5;
if (j0>this.getListMinWidth()&&Math.abs(j0-h6.offsetWidth)>5){
i9.style.width=j0+"px";
h6.style.width=j0+"px";
this.setListWidth(j0+5);
}
if (j1>this.getListMinHeight()&&Math.abs(j1-h6.offsetHeight)>20){
var i3=this.getFpSpread();
if (i3!=null)
i3.style.height=""+(j1-this.getStatusBarHeight())+"px";
i9.style.height=(j1+5)+"px";
h6.style.height=j1+"px";
this.setListHeight(j1);
}
if (!FarPoint.System.CheckBrowserByName("IE")){
var j2=this.getFpSpread();
the_fpSpread.SizeSpread(j2)
the_fpSpread.Refresh(j2);
}
event.cancelBubble=true;
return false;
}
this.MccbctScroll=function (event){
var h0=this.getControl();
if (h0==null)return ;
var h6=this.getContainer();
if (h6==null)return ;
var i9=this.getContainerDiv();
if (i9==null)return ;
if (FarPoint.System.CheckBrowserByName("safari")&&h0.offsetHeight==0){
i9.style.top=(FarPoint.System.GetOffsetTop(h0)+25)-(this.GetSpreadClientData(this.getHostSpread(),1))+"px";
}else {
var j3=null;
if (FarPoint.System.CheckBrowserByName("IE")?typeof(this.getFpSpread().EnableClientScript)=="undefined":this.getFpSpread().getAttribute("EnableClientScript")==null)
j3=FarPoint.System.CheckBrowserByName("IE")?document.getElementById(this.getHostSpread().id+"_view"):document.getElementById(this.getHostSpread().id+"_viewport").parentNode;
var j4=FarPoint.System.IsChild(j3,h0)?(this.GetSpreadClientData(this.getHostSpread(),1)):0;
i9.style.top=(FarPoint.System.GetOffsetTop(h0)+h0.offsetHeight+2)-j4+"px";
}
}
this.GetAdjustorForScroll=function (){
var j5=0;var j6=0;
var e7=new String(this.getControl().getAttribute(f4.$ATTRI_ID));
j5=parseInt(e7.split(new RegExp("_"))[1]);
j6=parseInt(e7.split(new RegExp("_"))[2]);
var j7=e7.split(new RegExp("_"))[3];
var j8=this.getHostSpread();
var j9=FarPoint.System.CheckBrowserByName("IE");
var k0=FarPoint.System.CheckBrowserByName("safari");
var k1={left:0,top:0};
if (this.getFpSpread().getAttribute("EnableClientScript")=="0")
return k1;
if (k0){
if (j7!="sc"&&j7!="rh")
k1.left=this.GetSpreadClientData(j8,0);
if (j7!="ch"&&j7!="cf"&&j7!="sc")
k1.top=this.GetSpreadClientData(j8,1);
return k1;
}
var k2=j9?j8.getViewport():the_fpSpread.GetViewport(j8);
var k3=j9?j8.getViewport0():the_fpSpread.GetViewport0(j8);
var k4=j9?j8.getViewport1():the_fpSpread.GetViewport1(j8);
var k5=j9?j8.getViewport2():the_fpSpread.GetViewport2(j8);
var k6=0;var k7=0;
k6=j9?(k4!=null?k4.rows.length:0):j8.frzRows;
if (j9){
if (k3!=null){
var k8=k3.getElementsByTagName("COLGROUP");
if (k8!=null&&k8.length>0)
k7=k8[0].childNodes.length;
}else if (k5!=null){
var k8=k5.getElementsByTagName("COLGROUP");
if (k8!=null&&k8.length>0)
k7=k8[0].childNodes.length;
}
}else {
k7=j8.frzCols;
}
if ((j7!="ch"&&j7!="cf"&&j7!="sc")&&((k6>0&&(j5+1)>k6)||k6==0))
k1.top=this.GetSpreadClientData(j8,1);
if ((j7!="sc"&&j7!="rh")&&((k7>0&&(j6+1)>k7)||k7==0))
k1.left=this.GetSpreadClientData(j8,0);
return k1;
}
this.InitSpread=function (){
if (!FarPoint.System.CheckBrowserByName("IE")&&typeof(the_fpSpread)!="undefined"){
var i3=this.getFpSpread();
the_fpSpread.Init(i3);
the_fpSpread.SizeAll(i3);
i3.dispose=function (){
the_fpSpread.Dispose(i3);
}
}
}
this.IsContained=function (child){
return FarPoint.System.IsChild(this.getControl(),child)||FarPoint.System.IsChild(this.getContainer(),child);
}
this.GetActivePositonInDomTree=function (element){
if (element==null)return false;
while (element!=null&&element!=document.body){
if (element.tagName=="TR"&&element.getAttribute("FpSpread")!=null)return element.getAttribute("FpSpread");
element=element.parentNode;
}
return "";
}
this.GetSpreadClientData=function (i3,whichData){
if (this.getFpSpread().getAttribute("EnableClientScript")=="0")return 0;
var k9="";
var l0=0;
var l1=null;
if (FarPoint.System.CheckBrowserByName("ie")){
if (i3.GetParentSpread()!=null)return ;
l1=document.getElementById(i3.id+"_XMLDATA");
switch (whichData){
case 0:
k9="/root/scrollLeft";
break ;
case 1:
k9="/root/scrollTop";
break ;
}
var l0=l1.documentElement.selectSingleNode(k9);
if (l0!=null&&l0.text!=""){
l0=parseInt(l0.text);
}
}else {
if (the_fpSpread.GetParentSpread(i3)!=null)return ;
l1=the_fpSpread.GetData(i3);
var l2=l1.getElementsByTagName("root")[0];
switch (whichData){
case 0:
k9="scrollLeft";
break ;
case 1:
k9="scrollTop";
break ;
}
var l3=l2.getElementsByTagName(k9)[0];
if (l3!=null&&l3.innerHTML!=""){
l0=parseInt(l3.innerHTML);
}
}
if (isNaN(l0))l0=0;
return l0;
}
this.SetHandler=function (f7,index,method){
if (isNaN(index)||index<0)return ;
if ((typeof(f7[index])=="undefined")||(f7[index]==null))return ;
switch (method){
case 0:
FarPoint.System.AttachEvent(f7[index].target,f7[index].event,f7[index].handler,f7[index].useCapture);
break ;
case 1:
FarPoint.System.DetachEvent(f7[index].target,f7[index].event,f7[index].handler,f7[index].useCapture);
break ;
}
}
this.ChangeSelectedIndex=function (step,caseId){
var l4=0;
if (typeof(caseId)!="undefined")
l4=caseId;
var i3=this.getFpSpread();
if (!i3)return ;
var l5=i3.GetRowCount();
if (l5<=0)return ;
var l6=this.getSelectedIndex();
var l7=this.getActiveColumnIndex();
if (l7<0)l7=0;
switch (l4){
case 0:
l6+=step;
if ((l6<0)||(l6>=l5))return ;
break ;
case 1:
l6-=5;
l6=Math.max(l6,0);
break ;
case 2:
l6+=5;
l6=Math.min(l6,l5-1);
break ;
case 3:
l6=0;
break ;
case 4:
l6=l5-1;
break ;
}
this.setSelectedIndex(l6);
i3.SetActiveCell(l6,l7);
}
this.ChangedActiveColumnIndex=function (IsLeft){
var i3=this.getFpSpread();
if (!i3)return ;
var l8=i3.GetColCount();
if (l8<=0)return ;
var l9=FarPoint.System.CheckBrowserByName("IE")?i3.ActiveRow:i3.GetActiveRow();
var m0=this.getActiveColumnIndex();
if (isNaN(m0))m0=0;
m0=IsLeft?m0-1:m0+1;
if ((m0<0)||(m0>=l8)){
m0=Math.max(m0,0);
m0=Math.min(m0,l8-1);
this.setActiveColumnIndex(m0);
return ;
}
if (FarPoint.System.CheckBrowserByName("IE"))
i3.ScrollTo(l9,m0);
else 
the_fpSpread.ScrollTo(i3,l9,m0);
this.setActiveColumnIndex(m0);
}
this.ShowHideContainer=function (show){
var h0=this.getControl();
if (h0==null)return ;
var h6=this.getContainer();
if (h6==null)return ;
var i9=this.getContainerDiv();
if (i9==null)return ;
if (!FarPoint.System.CheckBrowserByName("IE")){
i9.style.display=(show?'block':'none');
i9.style.visibility=(show?'visible':'hidden');
}
if (show){
h6.style.height=this.getListHeight()+"px";
i9.style.height=(this.getListHeight()+5)+"px";
h6.style.top=(-this.getListHeight()*0.25)+"px";
var k1=this.GetAdjustorForScroll();
if (FarPoint.System.CheckBrowserByName("safari")&&h0.offsetHeight==0){
i9.style.top=(FarPoint.System.GetOffsetTop(h0)+25)-k1.top+"px";
}else {
i9.style.top=(FarPoint.System.GetOffsetTop(h0)+h0.offsetHeight+2)-k1.top+"px";
}
var m1=FarPoint.System.GetOffsetLeft(h0);
if (this.getListAlignment()==0)
m1+=this.getListOffset();
else {
var m2=0;
if (this.getListWidth()!=-1)
m2=(this.getListWidth()-h0.parentNode.offsetWidth);
m1-=(this.getListOffset()+m2);
}
i9.style.left=m1-k1.left+"px";
var m3=this.getListWidth();
if (m3<0)m3=Math.max(this.getListMinWidth(),h0.parentNode.offsetWidth);
h6.style.width=(m3+5)+"px";
i9.style.width=(m3+5)+"px";
document.body.appendChild(i9);
var m4=this;
i9.close=function (){
m4.ShowHideContainer(false);
};
this.SetHandler(f7,1,0);
this.SetHandler(f7,3,0);
this.SetHandler(f7,13,0);
}else {
i9.close=null;
h0.appendChild(i9);
i9.style.top=-10000;
i9.style.left=-10000;
this.SetHandler(f7,1,1);
this.SetHandler(f7,3,1);
this.SetHandler(f7,13,1);
}
var i3=this.getFpSpread();
if (show&&i3!=null){
i3.style.height=(parseInt(h6.style.height)-this.getStatusBarHeight())+"px";
}
this.setIsDrop(show);
if (!FarPoint.System.CheckBrowserByName("IE")){
if (i3!=null){
the_fpSpread.SizeAll(i3);
the_fpSpread.SizeAll(i3);
if (show){
if (this.getFpSpread().getAttribute("EnableClientScript")!="0"){
the_fpSpread.SetPageActiveSpread(i3);
the_fpSpread.SetActiveSpreadID(i3,i3.id,i3.id,false);
}
var l6=this.getSelectedIndex();
i3.SetActiveCell(l6,0);
this.setActiveColumnIndex(0);
if (i3.GetActiveRow()>-1)
the_fpSpread.ScrollTo(i3,i3.GetActiveRow(),0);
}else {
var m5=this.getHostSpread();
if (this.getFpSpread().getAttribute("EnableClientScript")!="0"){
the_fpSpread.SetPageActiveSpread(m5);
the_fpSpread.SetActiveSpreadID(m5,m5.id,m5.id,false);
}
}
}
}else {
if (show){
var l6=this.getSelectedIndex();
i3.SetActiveCell(l6,0);
this.setActiveColumnIndex(0);
if (i3.ActiveRow>-1)
i3.ScrollTo(i3.ActiveRow,0);
}
}
if (show){
var i2=this;
setTimeout(function (){i2.AnimShow(h6);},30);
}
var h9=this.getInputControl();
if (h9!=null){
try {
h9.focus();
h9.select();
if (h9.value.length>0&&this.getSelectedIndex()==-1){
this.SetText(h9.value);
}
}catch (exception ){}
}
if (FarPoint.System.CheckBrowserByName("IE")){
i9.style.display=(show?'block':'none');
i9.style.visibility=(show?'visible':'hidden');
}
}
this.AnimShow=function (h6){
var m6=h6.offsetTop;
if (m6>=0)return ;
var m7=m6<-5?m6*0.25:0;
h6.style.top=m7+"px";
var i2=this;
setTimeout(function (){i2.AnimShow(h6);},30);
}
this.SetText=function (text){
if (text==null||text.length<=0)return ;
var i3=this.getFpSpread();
if (i3==null)return ;
var m8=this.getEditColumnIndex();
if ((!FarPoint.System.CheckBrowserByName("IE"))&&typeof(i3.GetRowCount)=="undefined"){
var g9=0,l6=-1;
while ((g9<the_fpSpread.spreads.length)&&(l6==-1)){
if (the_fpSpread.spreads[g9].id==i3.id)l6=g9;
g9++;
}
the_fpSpread.spreads.splice(l6,1);
the_fpSpread.Init(document.getElementById(i3.id));
document.getElementById(i3.id).dispose=function (){
the_fpSpread.Dispose(document.getElementById(i3.id));
}
}
if (!FarPoint.System.CheckBrowserByName("IE")){
text=text.replace(new RegExp("\xA0","g"),String.fromCharCode(32));
}
var m9=i3.GetRowCount();
var l6=0;
for (;l6<m9;l6++){
try {
var n0=i3.GetValue(l6,m8);
if (n0==(FarPoint.System.CheckBrowserByName("IE")?text:the_fpSpread.Trim(text))){
i3.SetActiveCell(l6,m8);
break ;
}
}catch (exception ){
return ;
}
}
}
this.TestProps=function (){
if (!f2.$FLAG_ISDEBUG)return ;
}
this.Init();
}
FarPoint.System.WebControl.MultiColumnComboBoxCellType.CheckInit=function (id){
var h0=document.getElementById(id);
if (h0==null){
h0=document.getElementById(id+"Editor");
id+="Editor";
}
if (h0&&h0.Init)return ;
try {
var n1=eval(id+f4.$OBJECT_SUFFIX);
if (n1){
n1.Dispose();
delete n1;
}
}catch (exception ){}
var n2=id+f4.$OBJECT_SUFFIX+"=new FarPoint.System.WebControl.MultiColumnComboBoxCellType(document.getElementById('"+id+"'));";
eval(n2);
}
FarPoint.System._ExtenderHelper=function (){
this.ScriptHolderID="__PAGESCRIPT";
this.ScriptBlockID="__SCRIPTBLOCK";
this.StartupScriptID="__STARTUPSCRIPT";
this.CssLinksID="__CSSLINKS";
}
FarPoint.System._ExtenderHelper.prototype={
getExtenderScripts:function (){
var n3={};
var n4=document.getElementsByTagName("input");
for (var g9=0;g9<n4.length;g9++){
var n5=n4[g9].id.match(new RegExp("^(.+)_extender$"));
if (n5&&n5.length==2){
var n6=n5[1];
var n7=$get(n6);
if (n7&&(n7.FpSpread=="Spread"||n7.getAttribute("FpSpread")=="Spread")){
var n8=n4[g9].json||n4[g9].getAttribute("json");
var n9=eval("("+n8+")");
this.mergeExtenderScripts(n3,n9.extenderScripts);
}
}
}
return n3;
},
mergeExtenderScripts:function (i1,source){
for (var o0 in source){
var n0=source[o0];
if (!i1[o0]){
i1[o0]=n0;
}else {
for (var o1=0;o1<n0.length;o1++){
var o2=n0[o1];
if (!Array.contains(i1[o0],o2))i1[o0].push(o2);
}
}
}
},
getNeededExtenderScripts:function (newScripts,realScripts,loadedScripts){
var o3=[];
var o4=[];
for (var o0 in newScripts){
Array.addRange(o3,newScripts[o0]);
}
for (var o0 in loadedScripts){
Array.addRange(o4,loadedScripts[o0]);
}
var o5=[];
for (var g9=0;g9<realScripts.length;g9++){
var n0=realScripts[g9];
if (!Array.contains(o4,n0)&&Array.contains(o3,n0))
o5.push(n0);
}
return o5;
},
get_scriptHolder:function (){
var o6=$get(this.ScriptHolderID);
if (!o6){
o6=document.createElement("div");
o6.id=this.ScriptHolderID;
o6.style.display="none";
document.body.appendChild(o6);
}
return o6;
},
saveLoadedExtenderScripts:function (i3){
var o6=this.get_scriptHolder();
o6.innerHTML="";
var o7=this.getExtenderScripts();
var o4=o6.loaded;
if (!o4)o4={};
this.mergeExtenderScripts(o4,o7);
o6.loaded=o4;
if (typeof(FpExtender)!='undefined')FpExtender.Util.disposeExtenders(i3);
},
processCss:function (buff){
var o8=document.getElementsByTagName("head");
if (!o8){
o8=document.createElement("head");
document.documentElement.insertBefore(o8,document.body)
}else {
o8=o8[0];
}
var o9=[];
var p0=o8.getElementsByTagName("link");
if (p0){
for (var g9=0;g9<p0.length;g9++){
var p1=p0[g9];
if (p1.getAttribute("type")=="text/css"){
o9.push(p1.getAttribute("href"));
}
}
}
var p2=$get(this.CssLinksID,buff);
if (p2){
p2=eval("("+p2.value+")");
p2=p2.cssLinks;
for (var g9=0;g9<p2.length;g9++){
var p3=p2[g9];
if (!Array.contains(o9,p3)){
var p1=document.createElement("link");
p1.type="text/css";
p1.rel="stylesheet";
p1.href=p3;
o8.appendChild(p1);
}
}
}
},
loadExtenderScripts:function (i3,buff){
if (Sys.Browser.agent!=Sys.Browser.InternetExplorer)
this.processCss(buff);
var p4=[];
var p5=[];
var p6=$get(this.ScriptBlockID,buff);
if (p6){
var p7=new RegExp("<script src=\"(.+)\" type=\"text\\/javascript\"><\\/script>","gm");
var p8;
while ((p8=p7.exec(p6.value))!=null){
p5.push(p8[1]);
}
}
var p9=this.getExtenderScripts();
var o6=this.get_scriptHolder();
var o5=this.getNeededExtenderScripts(p9,p5,o6.loaded);
for (var g9=0;g9<o5.length;g9++)p4.push({src:o5[g9]});
var q0=$get(this.StartupScriptID,buff);
var q1=Sys._ScriptLoader.getInstance();
for (var g9=0;g9<p4.length;g9++){
var q2=p4[g9].src;
if (q2)q1.queueScriptReference(q2);
}
q1.loadScripts(0,function (){
if (q0&&typeof(FpExtender)!=='undefined'){
var q3=false;
var q4=FpExtender.Util.getExtenderInitScripts(i3,q0.value);
for (var g9=0;g9<q4.length;g9++){
eval(q4[g9]);
if (q4[g9].indexOf("Sys.Application.initialize")!=-1)q3=true;
}
if (!q3){
var q5=Sys.Application.getComponents();
for (var g9 in q5){
if (q5[g9].get_id().indexOf(i3.id)==0&&FpExtender.ContainerBehavior.isInstanceOfType(q5[g9]))
q5[g9]._load();
}
}
}
},function (){
},null);
}
}
FarPoint.System.ExtenderHelper=new FarPoint.System._ExtenderHelper();
