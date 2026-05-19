let clientes = [];
const tabela = $("#tabelaClientes");
let Editando = false;

//chama listar no inicio pra atualizar a tela
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
    //validação de update ou documento repetido
    if (ExisteCliente && !Editando) {
        alert('Esse documento já está cadastrado.')
    }

    //adiciona o cliente no array
    const novoCliente = {
        documento: documentoCliente,
        nome: nomeCliente,
        email: emailCliente,
        uf: ufCliente
    };

    if(Editando){
        $.ajax({
            type:"PUT",
            url:"https://localhost:44317/api/Clientes/Atualizar",
            data:JSON.stringify(novoCliente),
            contentType: "application/json; charset=utf-8",
            dataType:"json",
            success: function(){
                alert('Sucesso');
                clientes.push(novoCliente);
            },
            error: function(){
                alert('erro');
            }
        });
    }else {
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
    $("#formCliente").trigger("reset");
    const modal = bootstrap.Modal.getOrCreateInstance($("#clienteModal")[0]);
    modal.hide();
}

function AtualizaTabela() {
    tabela.html(""); // LIMPA A TABELA
    clientes.forEach(cliente => {
        tabela.append(`
        <tr id="Linha${cliente.documento}">

            <td>${cliente.documento}</td>
            <td>${cliente.nome}</td>
            <td>${cliente.email}</td>
            <td>${cliente.uf}</td>

            <td>
            <button class="btn btn-warning btn-sm" onclick="Editar(${cliente.documento})">Editar</button>
            <button class="btn btn-danger btn-sm" onclick="Deletar(${cliente.documento})">Remover</button>
            </td>
        </tr>
        `);
    });
}

function Listar(){
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

function Editar(documento){
    const clienteAtualizar = clientes.find(c => c.documento === documento)

    //Abre modal com dados preenchidos do cliente
    if(clienteAtualizar){
        $("#documento").val(clienteAtualizar.documento);
        $("#nome").val(clienteAtualizar.nome);
        $("#email").val(clienteAtualizar.email);
        $("#uf").val(clienteAtualizar.uf);

        Editando = true;
        const modal = bootstrap.Modal.getOrCreateInstance($("#clienteModal")[0]);
        modal.show();
        //Após as alterações, chama função SalvarCadastro() via modal
    }
}

/*
function Buscar(documento) {
    $.ajax({
            type:"GET",
            url:"https://localhost:44317/api/Clientes/Atualizar?documento="+documento,
            contentType: "application/json; charset=utf-8",
            dataType:"json",
            success: function(OBJ){
                alert('Sucesso');

            },
            error: function(OBJ){
                alert('erro');
            }
        });
 
}
*/

function Deletar(documento) {
    //atualiza lista de clientes 
    clientes = clientes.filter(c => c.documento !== documento);
    
    $.ajax({
        type: "DELETE",
        url:
        "https://localhost:44317/api/Clientes/Deletar?documento=" + documento,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (OBJ)
        {
            alert('Cliente removido');
            $('#Linha' + documento).remove();
        },
        error: function (OBJ)
        {
            alert('error');
        }

    });

}