/* select all table from Resipe */
select * from Resipe

/* select all table from ingraden */
select * from ingraden

/* select all table from recipeIngreden */
select * from recipeIngreden

/* select the name from table ingraden where it's id used in recipeIngraden table ingradenId
with condution of resipeId=2 in recipINGREDEN table */
select a.Name from ingraden a
inner join recipeIngreden b on a.Id=b.ingradenId
where b.ResipeId=2;

/* select the name from table ingraden where it's id used in recipeIngraden table ingradenId
with condution of resipeId=1 in recipINGREDEN table */
select a.Name from ingraden a
inner join recipeIngreden b on a.Id=b.ingradenId
where b.ResipeId=1;


update Resipe
set Name= 'Salald'
where id = 1;

insert into Resipe
values ('Salad 2',40,'test')

delete from Resipe
where id=3;