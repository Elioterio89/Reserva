function SalvarPedido() {

    //Data
    var data = $("#Data").val();

    //Cliente
    var cliente = $("#Cliente").val();

    //Valor
    var valor = $("#Valor").val();

    var token = $('input[name="__RequestVerificationToken"]').val();
    var tokenadr = $('form[action="/Militars/Retirar"] input[name="__RequestVerificationToken"]').val();
    var headers = {};
    var headersadr = {};
    headers['__RequestVerificationToken'] = token;
    headersadr['__RequestVerificationToken'] = tokenadr;

    //Gravar
    var url = "/Militars/Retirar";

    $.ajax({
        url: url
        , type: "POST"
        , datatype: "json"
        , headers: headersadr
        , data: { Id: 0, Data: data, Cliente: cliente, Valor: valor, __RequestVerificationToken: token }
        , success: function (data) {
            if (data.Resultado > 0) {
                ListarItens(data.Resultado);
            }
        }
    });
}

function ListarItens(idPedido) {

    var url = "/Itens/ListarItens";

    $.ajax({
        url: url
        , type: "GET"
        , data: { id: idPedido }
        , datatype: "html"
        , success: function (data) {
            var divItens = $("#divItens");
            divItens.empty();
            divItens.show();
            divItens.html(data);
        }
    });

}


function SalvarItens() {

    var quantidade = $("#Quantidade").val();
    var produto = $("#Produto").val();
    var valorunitario = $("#ValorUnitario").val();
    var idPedido = $("#idPedido").val();

    var url = "/Itens/SalvarItens";

    $.ajax({
        url: url
        , data: { quantidade: quantidade, produto: produto, valorunitario: valorunitario, idPedido: idPedido }
        , type: "GET"
        , datatype: "html"
        , success: function (data) {
            if (data.Resultado > 0) {
                ListarItens(idPedido);
            }
        }
    });
}