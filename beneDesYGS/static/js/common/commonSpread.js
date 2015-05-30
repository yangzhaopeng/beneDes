(function() {
    function resize() {
        var obj = parent.document.getElementById('center');
        //        document.getElementById('FpSpread1').style.width = obj.scrollWidth - 2 + 'px';
        //        document.getElementById('FpSpread1').style.height = obj.scrollHeight - 2 + 'px';
    }

    window.resize = resize;
    window.onload = function() {
        resize();
        // farPointSpread真是太老了，针对chrome没有兼容，hack the bug
        //if (the_fpSpread) the_fpSpread.DoResize();
    };
})();