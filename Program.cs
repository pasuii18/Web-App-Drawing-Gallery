using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MinAPI.Algorithms;
using MinAPI.Data;
using MinAPI.Dtos;
using MinAPI.Models;

Validation valid = new Validation();
Hashing hash = new Hashing();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var sqlConBuilder = new SqlConnectionStringBuilder();

sqlConBuilder.ConnectionString = builder.Configuration.GetConnectionString("SQLDbConnection");
sqlConBuilder.UserID = builder.Configuration["UserID"];
sqlConBuilder.Password = builder.Configuration["Password"];

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(sqlConBuilder.ConnectionString));
builder.Services.AddScoped<ITableRowRepo, TableRowRepo>();
builder.Services.AddScoped<IPostRowRepo, PostRowRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();


// Users
app.MapGet("api/v1/users/isLoginExist/{login}", async (ITableRowRepo repo, IMapper mapper, string login) =>
{
    if(valid.isTableRowValid(login))
    {
        var row = await repo.GetTableRowByLogin(login);
        if (row != null)
        {
            return Results.Ok();
        }
    }
    return Results.NotFound();
});

app.MapGet("api/v1/users/isNicknameExist/{nickname}", async (ITableRowRepo repo, IMapper mapper, string nickname) =>
{
    if(valid.isTableRowValid(nickname))
    {
        var row = await repo.GetTableRowByNickname(nickname);
        if (row != null)
        {
            return Results.Ok();
        }
    }
    return Results.NotFound();
});

app.MapGet("api/v1/users/logging/{login},{password}", async (ITableRowRepo repo, IMapper mapper, string login, string password) =>
{
    if(valid.isTableRowValid(login) && valid.isTableRowValid(password))
    {
        var row = await repo.GetTableRowByLogin(login);
        if (row != null && hash.VerifyHashedPassword(row.Password, password))
        {
            return Results.Ok($"{row.Nickname},{row.Id}");
        }
    }
    return Results.NotFound();
});

app.MapPost("api/v1/users", async (ITableRowRepo repo, IMapper mapper, TableRowCreateDto createDto) => {
    if(valid.isTableRowValid(createDto.Nickname) && valid.isTableRowValid(createDto.Login) && valid.isTableRowValid(createDto.Password))
    {
        var rowModel = mapper.Map<TableRow>(createDto);

        rowModel.Password = hash.HashPassword(rowModel.Password);

        await repo.CreateTableRow(rowModel);
        await repo.SaveChanges();

        var cmdReadDto = mapper.Map<TableRowReadDto>(rowModel);

        return Results.Ok(cmdReadDto.Id);
    }
    return Results.NotFound();
});

app.MapPut("api/v1/users/{id}", async (ITableRowRepo repo, IMapper mapper, int id, TableRowUpdateDto UpdateDto) => {
    
    var row = await repo.GetTableRowById(id);
    var rowWithSameNickname = await repo.GetTableRowByNickname(UpdateDto.Nickname);

    bool isValidUpdate = valid.isTableRowValid(UpdateDto.Nickname) && valid.isTableRowValid(UpdateDto.Login) && valid.isTableRowValid(UpdateDto.Password);
    bool isLoginUnchanged = row.Login == UpdateDto.Login;
    bool isNicknameUnchanged = row.Nickname == UpdateDto.Nickname;

    if (isNicknameUnchanged && isValidUpdate && isLoginUnchanged)
    {
        UpdateDto.Password = hash.HashPassword(UpdateDto.Password);

        mapper.Map(UpdateDto, row);

        await repo.SaveChanges();

        return Results.Ok();
    }
    else if (row != null && rowWithSameNickname == null && isValidUpdate && isLoginUnchanged)
    {
        UpdateDto.Password = hash.HashPassword(UpdateDto.Password);
        await repo.UpdateAllPostRowsByField("Nickname", row.Nickname, UpdateDto.Nickname);

        mapper.Map(UpdateDto, row);

        await repo.SaveChanges();

        return Results.Ok();
    }

    return Results.NotFound();
});

app.MapDelete("api/v1/users/{id},{login}", async (ITableRowRepo repo, IMapper mapper, int id, string login) => {
    
    var row = await repo.GetTableRowById(id);

    if (row != null)
    {
        if(valid.isTableRowValid(login))
        {
            if(row.Login == login)
            {
                repo.DeleteTableRow(row);
                await repo.SaveChanges();
                return Results.NoContent();
            }
        }
    }
    return Results.NotFound();
});


// Posts
app.MapGet("api/v1/posts", async (IPostRowRepo repo, IMapper mapper) => {
    var rows = await repo.GetAllPostRows();
    return Results.Ok(mapper.Map<IEnumerable<PostRowReadDto>>(rows));
});

app.MapGet("api/v1/posts/getPostsByNickname/{nickname}", async (IPostRowRepo repo, IMapper mapper, string nickname) => {
    var rows = await repo.GetAllPostRowsByField("Nickname", nickname);
    return Results.Ok(mapper.Map<IEnumerable<PostRowReadDto>>(rows));
});

app.MapPost("api/v1/posts", async (IPostRowRepo repo, IMapper mapper, PostRowCreateDto CreateDto) => {
    var rowModel = mapper.Map<PostRow>(CreateDto);

    await repo.CreatePostRow(rowModel);
    await repo.SaveChanges();

    var ReadDto = mapper.Map<PostRowReadDto>(rowModel);

    return Results.Created($"api/v1/commands/{ReadDto.Id}", ReadDto);
});

app.Run();
