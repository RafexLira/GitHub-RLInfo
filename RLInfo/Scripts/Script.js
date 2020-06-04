//máscaras
$(document).ready(function () {
    $('.cpf').mask('000.000.000-00');
    $('.cep').mask('00000-000');
    $('.telefone').mask('(00)00000-0000');
    $('.preco').inputmask('currency', {
        "autoUnmask": true,
        radixPoint: ",",
        groupSeparator: ".",
        allowMinus: false,
       // prefix: 'R$ ',
        digits: 2,
        digitsOptional: false,
        rightAlign: true,
        unmaskAsNumber: true
    });

});

//Listar todos os clientes
function Testes() {
    return alert('Testado com sucesso!');

}
function Listar() {

    var lista = document.getElementById('lista');
    var DadosDoEquipamento = document.getElementById('DadosDoEquipamento');
    var DadosDoCliente = document.getElementById('DadosDoCliente');
    var BtnListarTodos = document.getElementById('BtnListarTodos');
    var BtnEditar = document.getElementById('BtnEditar');


    if (lista.style.display === 'none') {


        lista.style.display = 'block';
        DadosDoEquipamento.style.display = 'none';
        DadosDoCliente.style.display = 'none';
        BtnEditar.style.display = 'none';
        BtnListarTodos.style.display = 'none';


    }

}

//imprimir OS
function Ocultar() {

    var btnimprimir = document.getElementById("BtnImprimir");
    var btnvoltar = document.getElementById("BtnVoltar");


    btnvoltar.style.display = 'none';
    btnimprimir.style.display = 'none';  
    window.print();
    swal('Impresso com sucesso!');
    btnvoltar.style.display = 'block';
    btnimprimir.style.display = 'block';
  



}
