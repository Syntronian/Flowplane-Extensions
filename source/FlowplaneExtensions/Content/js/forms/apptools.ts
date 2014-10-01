/// <reference path="../refs.d.ts" />

module fpxt.forms.apptools {

    export function fmtDate(el: JQuery, fmt?: string) {
        if (fmt == null)
            fmt = 'lll';
        if (el == null)
            return;
        var dt = el.html();
        var useVal = false;
        if (dt == null || dt.trim() == '') {
            dt = el.val(); // try value instead of html
            useVal = true;
        }
        if (dt == null)
            return;
        var mt = moment(dt.trim());
        if (mt == null)
            return;
        if (!mt.isValid())
            return;
        if (useVal)
            el.val(moment(new Date(dt.trim())).format(fmt));
        else
            el.html(moment(new Date(dt.trim())).format(fmt));
    }
}
