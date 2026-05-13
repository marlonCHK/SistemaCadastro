let clientes = [];
const tabela = $("#tabelaClientes");

//chama listar no inicio pra atualizar a tela
alert('vai chamar');
Listar();

$("#formCliente").on("submit", function (e) {
    e.preventDefault();
    SalvarCadastro();
});

function SalvarCadastro() {
    const documentoCliente = Number($("#documento").val());
    const nomeCliente = $("#nome").val();
    const emailCliente = $("#email").val();
    const ufCliente = $("#uf").val();
    //Verifica se documento ja existe
    const ExisteCliente = clientes.some(clientes =>
        clientes.documento === documentoCliente
    )

    if (ExisteCliente) {
        //editar
        const cliente = clientes.find(c => c.documento == documentoCliente);
        alert('Esse documento já está cadastrado.')
        //add a nova informação
        /*cliente.documento = documentoCliente
        cliente.nome = nomeCliente
        cliente.email = emailCliente
        cliente.uf = ufCliente*/
    } else {
        //adiciona o cliente no array
        const novoCliente = {
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
    //AtualizaTabela();
    $("#formCliente").trigger("reset");
    const modal = bootstrap.Modal.getOrCreateInstance($("#clienteModal")[0]);
    modal.hide();
}

function AtualizaTabela() {
    tabela.html(""); // LIMPA A TABELA
    clientes.forEach(cliente => {
        tabela.append(`
        <tr>
            <td>${cliente.documento}</td>
            <td>${cliente.nome}</td>
            <td>${cliente.email}</td>
            <td>${cliente.uf}</td>

            <td>
            <button class="btn btn-warning btn-sm" onclick="editar(${cliente.documento})">Editar</button>
            <button class="btn btn-danger btn-sm" onclick="deletar(${cliente.documento})">Remover</button>
            </td>
        </tr>
        `);
    });
}

function Listar(){
    alert('entrou ?');
    $.ajax({
            type:"GET",
            url:"https://localhost:44317/api/Clientes/Listar",
            contentType: "application/json; charset=utf-8",
            dataType:"json",
            success: function(OBJ){

                alert('Sucesso');
                OBJ.forEach(clienteJson =>{
                    const clientesAtualiza = {
                        documento: clienteJson.documento,
                        nome: clienteJson.nome,
                        email: clienteJson.email,
                        uf: clienteJson.uf
                    };
                    clientes.push(clientesAtualiza);
                    AtualizaTabela()

                })
            },
            error: function(OBJ){
                alert('erro');
            }
        });
}

function editar(documento) {
    const cliente = clientes.find(c => c.documento === documento);
    $("#documento").val(cliente.documento);
    $("#nome").val(cliente.nome);
    $("#email").val(cliente.email);
    $("#uf").val(cliente.uf);

    const modal = bootstrap.Modal.getOrCreateInstance($("#clienteModal")[0]);
    modal.show();
}

function deletar(documento) {
    clientes = clientes.filter(c => c.documento !== documento);
    AtualizaTabela();
}