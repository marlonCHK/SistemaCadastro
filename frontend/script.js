let clientes = [];
let idAtual = 1;
const tabela = $("#tabelaClientes");

$("#formCliente").on("submit", function (e) {
    e.preventDefault();
    SalvarCadastro();
});

function SalvarCadastro() {
    const idCliente = Number($("#clienteId").val());
    const documentoCliente = Number($("#documento").val());
    const nomeCliente = $("#nome").val();
    const emailCliente = $("#email").val();
    const ufCliente = $("#uf").val();

    if (idCliente) {
        //editar
        const cliente = clientes.find(c => c.id == idCliente);
        //add a nova informação
        cliente.documento = documentoCliente
        cliente.nome = nomeCliente
        cliente.email = emailCliente
        cliente.uf = ufCliente
    } else {
        //adiciona o cliente no array
        const novoCliente = {
            id: idAtual++,
            documento: documentoCliente,
            nome: nomeCliente,
            email: emailCliente,
            uf: ufCliente
        };
        clientes.push(novoCliente);

        $.ajax({
            type:"POST",
            url:"https://localhost:44317/api/Clientes/Salvar",
            data:JSON.stringify(novoCliente),
            contentType: "application/json; charset=utf-8",
            dataType:"json",
            success: function(msg){
                alert('Sucesso');
                console.log(msg)
            },
            error: function(msg){
                alert('erro');
            }
        });
    }
    AtualizaTabela();
    $("#formCliente").trigger("reset");
    const modal = bootstrap.Modal.getOrCreateInstance($("#clienteModal")[0]);
    modal.hide();
}

function AtualizaTabela() {
    tabela.html(""); // LIMPA A TABELA
    clientes.forEach(cliente => {
        tabela.append(`
        <tr>
            <td>${cliente.id}</td>
            <td>${cliente.documento}</td>
            <td>${cliente.nome}</td>
            <td>${cliente.email}</td>
            <td>${cliente.uf}</td>

            <td>
            <button class="btn btn-warning btn-sm" onclick="editar(${cliente.id})">Editar</button>
            <button class="btn btn-danger btn-sm" onclick="deletar(${cliente.id})">Remover</button>
            </td>
        </tr>
        `);
    });
}

function editar(id) {
    const cliente = clientes.find(c => c.id === id);
    $("#clienteId").val(cliente.id);
    $("#documento").val(cliente.documento);
    $("#nome").val(cliente.nome);
    $("#email").val(cliente.email);
    $("#uf").val(cliente.uf);

    const modal = bootstrap.Modal.getOrCreateInstance($("#clienteModal")[0]);
    modal.show();
}

function deletar(id) {
    clientes = clientes.filter(c => c.id !== id);
    AtualizaTabela();
}