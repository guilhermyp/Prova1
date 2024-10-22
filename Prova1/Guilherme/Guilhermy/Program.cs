using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Guilhermy.Models;
using Guilhermy;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

List<Funcionario> funcionario = new List<Funcionario>
{
  
};


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Cadastrar Funcionario
app.MapPost("/api/funcionario/cadastrar", ([FromBody] Funcionario funcionario,
    [FromServices] AppDbContext db) =>
{
    db.Funcionarios.Add(funcionario);
    db.SaveChanges();
    return Results.Created("", funcionario);
});

// Listar Funcionarios
app.MapGet("/api/funcionario/listar", ([FromServices] AppDbContext db) =>
{
    if (db.Funcionarios.Any())
    {
        return Results.Ok(db.Funcionarios.ToList());
    }
    return Results.NotFound();
});



// Cadastrar Folha
app.MapPost("/api/folha/cadastrar", ([FromBody] Folha folha,
    [FromServices] AppDbContext db) =>
{
    db.Folhas.Add(folha);
    db.SaveChanges();
    return Results.Created("", folha);
});


app.MapGet("/api/folha/listar", async ([FromServices] AppDbContext db) =>
    await db.Folhas.ToListAsync()
);

// buscar por folha, cpf ou id
//GET: /api/produto/buscar/{id}
app.MapGet("/api/folha/buscar/{idFolha}", ([FromRoute] string idFolha,
    [FromServices] AppDbContext db) =>
{
    Folha? folha = db.Folhas.Find(idFolha);

    if (folha == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(idFolha);

   

});

app.MapGet("/api/folha/buscar/{Nome}", ([FromRoute] string Nome,
    [FromServices] AppDbContext db) =>
{
    Nome = db.Nome.Find(Nome);

    if (Nome == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(Nome);
});

app.MapGet("/api/folha/buscar/{Nome}", ([FromRoute] string Cpf,
    [FromServices] AppDbContext db) =>
{
    Cpf = db.Cpf.Find(Cpf);

    if (Cpf == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(Cpf);
});


// Vincular Folha ao FuncionarioId, Nome e CPF
app.MapPut("/api/folha/{FolhaId:int}/vincular-funcionario/{FuncionarioId:int}", 
    ([FromServices] AppDbContext db, int FolhaId, int FuncionarioId) =>
    {
        var folha = await db.Folhas.FindAsync(FolhaId);
        if (folha is null) return Results.NotFound($"Folha com ID {FolhaId} não encontrada.");

        var funcionario = await db.Funcionarios.FindAsync(FuncionarioId);
        if (funcionario is null) return Results.NotFound($"Funcionario com ID {FuncionarioId} não encontrado.");

        folha.FolhaId = FuncionarioId;
        await db.SaveChangesAsync();

        return Results.Ok($"Funcionario com ID {FuncionarioId} foi vinculado a folha com o ID {FolhaId}.");
    });


app.Run();
