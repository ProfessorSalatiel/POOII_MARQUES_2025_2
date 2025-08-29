-- 1) Criar schema para seu lab (opcional)
create schema if not exists public;

-- 2) Tabela dentro do schema pooii_lab
create table if not exists public.alunos (
  id             bigserial primary key,
  nome           varchar(120) not null,
  email          varchar(200) not null unique,
  data_cadastro  timestamptz  not null default now()
);

create unique index if not exists ux_alunos_email
  on public.alunos (email);

--3) Testar a tabela
insert into public.alunos (nome, email)
values ('Ana Souza','ana@example.com');

select * from public.alunos order by id;

--4) Dropar Tabela
BEGIN;
  DROP TABLE IF EXISTS public.alunos;
COMMIT;