//sostituire PROGETTO con nome progetto
//sostituire ENTITY con nome entità
if (typeof (RSMNG) == "undefined") {
    RSMNG = {};
}

if (typeof (RSMNG.TAUMEDIKA) == "undefined") {
    RSMNG.TAUMEDIKA = {};
}

if (typeof (RSMNG.TAUMEDIKA.PRODUCT) == "undefined") {
    RSMNG.TAUMEDIKA.PRODUCT = {};
}

(function () {
    var _self = this;

    //Form model
    _self.formModel = {
        entity: {
            logicalName: "product",
            displayName: "Prodotto"
        },
        fields: {
            ///Padre
            parentproductid: "parentproductid",
            ///Prodotto
            productid: "productid",
            ///Struttura prodotto
            productstructure: "productstructure",
            /// Values for field Stato
            statecodeValues: {
                Attivo: 0,
                Bozza: 2,
                Inaggiornamento: 3,
                Ritirato: 1
            },
            /// Values for field Struttura prodotto
            productstructureValues: {
                Aggregazioneprodotti: 3,
                Famigliadiprodotti: 2,
                Prodotto: 1
            }
        },
        tabs: {

        },
        sections: {

        }
    };
    //---------------------------------------------------
    _self.filtroFamigliaAssociata = executionContext => {
        console.log(`Sono nel metodo filtroFamigliaAssociata`);
        const formContext = executionContext.getFormContext();
        const famigliaAssociataControl = formContext.getControl(_self.formModel.fields.parentproductid);

        if (!famigliaAssociataControl) { console.error(`Campo Famiglia associata non trovato`); return; }

        var fetchData = {
            "statecode": _self.formModel.fields.statecodeValues.Attivo,
            "Famigliadiprodotti": _self.formModel.fields.productstructureValues.Famigliadiprodotti,
            "Aggregazioneprodotti": _self.formModel.fields.productstructureValues.Aggregazioneprodotti
        };
        var filtro = [
            "    <filter>",
            "      <condition attribute='statecode' operator='eq' value='", fetchData.statecode/*0*/, "'/>",
            "      <condition attribute='productstructure' operator='in'>",
            "        <value>", fetchData.Famigliadiprodotti/*2*/, "</value>",
            "        <value>", fetchData.Aggregazioneprodotti/*3*/, "</value>",
            "      </condition>",
            "    </filter>"
        ].join("");

        famigliaAssociataControl.addCustomFilter(filtro, "product");
        console.log(`Ho applicato il CustomFilter al campo Famiglia Associata`);
    };
    //---------------------------------------------------
    /*
    Utilizzare la keyword async se si utilizza uno o più metodi await dentro la funzione onSaveForm
    per rendere il salvataggio asincrono (da attivare sull'app dynamics!)
    */
    _self.onSaveForm = function (executionContext) {
        if (executionContext.getEventArgs().getSaveMode() == 70) {
            executionContext.getEventArgs().preventDefault();
            return;
        }
    };
    //---------------------------------------------------
    _self.onLoadCreateForm = async function (executionContext) {

        var formContext = executionContext.getFormContext();
    };
    //---------------------------------------------------
    _self.onLoadUpdateForm = async function (executionContext) {

        var formContext = executionContext.getFormContext();
    };
    //---------------------------------------------------
    _self.onLoadReadyOnlyForm = function (executionContext) {

        var formContext = executionContext.getFormContext();
    };
    //---------------------------------------------------
    _self.onLoadForm = async function (executionContext) {

        //init lib
        await import('../res_scripts/res_global.js');

        //init formContext
        var formContext = executionContext.getFormContext();

        //Init event
        formContext.data.entity.addOnSave(_self.onSaveForm);

        //Init function
        formContext.getControl(_self.formModel.fields.parentproductid).addPreSearch(_self.filtroFamigliaAssociata);
        console.log(`Ho aggiunto il PreSearch dal metodo onLoadForm`);

        switch (formContext.ui.getFormType()) {
            case RSMNG.Global.CRM_FORM_TYPE_CREATE:
                _self.onLoadCreateForm(executionContext);
                break;
            case RSMNG.Global.CRM_FORM_TYPE_UPDATE:
                _self.onLoadUpdateForm(executionContext);
                break;
            case RSMNG.Global.CRM_FORM_TYPE_READONLY:
                _self.onLoadReadyOnlyForm(executionContext);
                break;
            case RSMNG.Global.CRM_FORM_TYPE_QUICKCREATE:
                _self.onLoadCreateForm(executionContext);
                break;
        }
    };
}
).call(RSMNG.TAUMEDIKA.PRODUCT);