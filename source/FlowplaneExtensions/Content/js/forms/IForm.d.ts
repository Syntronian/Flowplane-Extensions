﻿/// <reference path="../refs.d.ts" />

declare module fpxt.forms {
    interface IForm {
        extId: string;

        setup(baseApiUrl: string, authKeys: fpxtParam[], objParams: fpxtParam[], onCompleted: () => void);
        setupAuthPre(baseApiUrl: string, authKeys: fpxtParam[], objParams: fpxtParam[], onCompleted: () => void);
        setupAuthPost(baseApiUrl: string, authKeys: fpxtParam[], objParams: fpxtParam[], onCompleted: () => void);
        fill(baseApiUrl: string, authKeys: fpxtParam[], values: fpxtParam[]);
        getProperties(): fpxtParam[];
    }
}

