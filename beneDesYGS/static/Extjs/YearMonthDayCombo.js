var nianStore = new Ext.data.ArrayStore({ fields: ['name', 'classid'], data: [[1990, 1990], [1991, 1991], [1992, 1992], [1993, 1993], [1994, 1994], [1995, 1995], [1996, 1996], [1997, 1997], [1998, 1998], [1999, 1999], [2000, 2000], [2001, 2001], [2002, 2002], [2003, 2003], [2004, 2004], [2005, 2005], [2006, 2006], [2007, 2007], [2008, 2008], [2009, 2009], [2010, 2010], [2011, 2011], [2012, 2012], [2013, 2013], [2014, 2014], [2015, 20015], [2016, 2016], [2017, 2017], [2018, 2018], [2019, 2019], [2020, 2020]] });
var yueStore = new Ext.data.ArrayStore({ fields: ['name', 'classid'], data: [['01', '01'], ['02', '02'], ['03', '03'], ['04', '04'], ['05', '05'], ['06', '06'], ['07', '07'], ['08', '08'], ['09', '09'], ['10', '10'], ['11', '11'], ['12', '12']] });
var ri1 = new Ext.data.ArrayStore({ fields: ['name', 'classid'], data: [['01', '01'], ['02', '02'], ['03', '03'], ['04', '04'], ['05', '05'], ['06', '06'], ['07', '07'], ['08', '08'], ['09', '09'], ['10', '10'], ['11', '11'], ['12', '12'], ['13', '13'], ['14', '14'], ['15', '15'], ['16', '16'], ['17', '17'], ['18', '18'], ['19', '19'], ['20', '20'], ['21', '21'], ['22', '22'], ['23', '23'], ['24', '24'], ['25', '25'], ['26', '26'], ['27', '27'], ['28', '28'], ['29', '29'], ['30', '30'], ['31', '31']] });
var ri2 = new Ext.data.ArrayStore({ fields: ['name', 'classid'], data: [['01', '01'], ['02', '02'], ['03', '03'], ['04', '04'], ['05', '05'], ['06', '06'], ['07', '07'], ['08', '08'], ['09', '09'], ['10', '10'], ['11', '11'], ['12', '12'], ['13', '13'], ['14', '14'], ['15', '15'], ['16', '16'], ['17', '17'], ['18', '18'], ['19', '19'], ['20', '20'], ['21', '21'], ['22', '22'], ['23', '23'], ['24', '24'], ['25', '25'], ['26', '26'], ['27', '27'], ['28', '28'], ['29', '29'], ['30', '30']] });
var ri3 = new Ext.data.ArrayStore({ fields: ['name', 'classid'], data: [['01', '01'], ['02', '02'], ['03', '03'], ['04', '04'], ['05', '05'], ['06', '06'], ['07', '07'], ['08', '08'], ['09', '09'], ['10', '10'], ['11', '11'], ['12', '12'], ['13', '13'], ['14', '14'], ['15', '15'], ['16', '16'], ['17', '17'], ['18', '18'], ['19', '19'], ['20', '20'], ['21', '21'], ['22', '22'], ['23', '23'], ['24', '24'], ['25', '25'], ['26', '26'], ['27', '27'], ['28', '28'], ['29', '29']] });
var ri4 = new Ext.data.ArrayStore({ fields: ['name', 'classid'], data: [['01', '01'], ['02', '02'], ['03', '03'], ['04', '04'], ['05', '05'], ['06', '06'], ['07', '07'], ['08', '08'], ['09', '09'], ['10', '10'], ['11', '11'], ['12', '12'], ['13', '13'], ['14', '14'], ['15', '15'], ['16', '16'], ['17', '17'], ['18', '18'], ['19', '19'], ['20', '20'], ['21', '21'], ['22', '22'], ['23', '23'], ['24', '24'], ['25', '25'], ['26', '26'], ['27', '27'], ['28', '28']] });

var myDate = new Date();
var dy = myDate.getFullYear();
var dm = myDate.getMonth() + 1;
if (dm < 10) {
    dm = "0" + dm;
}
var dd = myDate.getDate();
if (dd < 10) {
    dd = "0" + dd;
}
var month = null;
var year = null;
var getYearMonth = function() {
    return year + '' + month;
};

var setYearMonth = function() {
    var nian = new Ext.form.ComboBox({
        emptyText: '',
        renderTo: Ext.DomQuery.selectNode('#year'),
        width: 60,
        hidderName: 'classid',
        id: 'year',
        maxHeight: 200,
        forceSelection: true,
        typeAhead: true,
        minListWidth: 60,
        triggerAction: 'all',
        valueField: 'classid',
        displayField: 'name',
        store: nianStore,
        mode: 'local',
        listeners: {
            collapse: function() {
                year = nian.getValue();
            }
        }
    });
    var yue = new Ext.form.ComboBox({
        emptyText: '',
        renderTo: Ext.DomQuery.selectNode('#month'),
        width: 60,
        hidderName: 'classid',
        id: 'month',
        maxHeight: 200,
        forceSelection: true,
        typeAhead: true,
        minListWidth: 60,
        triggerAction: 'all',
        valueField: 'classid',
        displayField: 'name',
        store: yueStore,
        mode: 'local',
        listeners: {
            collapse: function() {
                month = yue.getValue();
            }
        }
    });
    yue.setValue(dm);
    month = dm;
    nian.setValue(dy);
    year = dy;
    //    ri.setValue(dd);
}
    var ri = new Ext.form.ComboBox({
        emptyText: '',
        width: 40,
        id: 'day',
        maxHeight: 200,
        forceSelection: true,
        typeAhead: true,
        minListWidth: 40,
        valueField: 'classid',
        displayField: 'name',
        triggerAction: 'all',
        store: ri1,
        mode: 'local'
    });


    function GetRiStore() {
        var y = nian.getValue();
        var m = yue.getValue();
        if (y != "" && m != "") {
            var d = ri.getValue();
            var len;
            if (m == "01" || m == "03" || m == "05" || m == "07" || m == "08" || m == "10" || m == "12") {
                ri.bindStore(ri1);
                len = 31;
            }
            else if (m == "04" || m == "06" || m == "09" || m == "11") {
                ri.bindStore(ri2);
                len = 30;
            }
            else {
                if (y == "1992" || y == "1996" || y == "2004" || y == "2008" || y == "2012" || y == "2020" || y == "2016") {
                    ri.bindStore(ri3);
                    len = 29;
                }
                else {
                    ri.bindStore(ri4);
                    len = 28;
                }
            }
            if (d > len) {
                ri.setValue(len.toString());
            }
            else {
                ri.setValue(d);
            }
        }
    }
    
    
    
   
