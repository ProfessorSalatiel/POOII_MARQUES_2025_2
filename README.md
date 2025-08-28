# 🚀 Guia para uso do GitHub — Disciplina de Programação Orientada a Objetos II  
**Curso de Análise e Desenvolvimento de Sistemas**  

Este guia explica como criar sua conta no **GitHub**, configurar no seu computador e usar os comandos básicos de **Git** durante as aulas de **POO II**.  

---

## 1️⃣ Criando uma conta no GitHub
1. Acesse [https://github.com](https://github.com).
2. Clique em **Sign up** (cadastrar).
3. Preencha:
   - **Email** (use o email que você tem acesso).
   - **Senha segura**.
   - **Username** (nome de usuário, sem espaços).
4. Confirme sua conta pelo **email** enviado pelo GitHub.
5. Pronto ✅ — sua conta GitHub está criada!

---

## 2️⃣ Configurando o GitHub no computador
Antes de usar, você precisa **instalar e configurar o Git**.

### Instalar o Git
- Baixe e instale em: [https://git-scm.com/downloads](https://git-scm.com/downloads)

### Configurar sua conta no Git (apenas uma vez)
Abra o **Git Bash** e digite:

```bash
git config --global user.name "Seu Nome"
git config --global user.email "seu-email@exemplo.com"
```

Verifique se deu certo:
```bash
git config --list
```

---

## 3️⃣ Clonando o repositório da disciplina
Durante as aulas, vamos clonar o repositório com o código base. Para habilitar o proxy no computador do laboratório rodar a seguinte config:

```bash
git config --http://192.168.0.1:8080
git config --https://192.168.0.1:8080
```

**Observação:** Essa ação de config de proxy será necessária somente na aulas que serão ministradas nos laboratórios 1 e 2!

No **Git Bash**, escolha a pasta onde deseja salvar os arquivos e rode:

```bash
git clone https://github.com/ProfessorSalatiel/POOII
```

Isso vai criar uma pasta chamada **POOII** com o conteúdo do repositório.

---

## 4️⃣ Comandos básicos do Git

### Verificar status dos arquivos
```bash
git status
```

### Adicionar arquivos alterados para commit
```bash
git add .
```

(O `.` adiciona todos os arquivos alterados.)

### Criar um commit com mensagem
```bash
git commit -m "Mensagem explicando o que foi alterado"
```

### Enviar para o GitHub (push)
```bash
git push
```

### Atualizar seu repositório local (pull)
```bash
git pull
```

---

## 5️⃣ Fluxo básico de trabalho
1. **Atualizar repositório** antes de começar:
   ```bash
   git pull
   ```
2. **Fazer alterações** no código.
3. **Adicionar arquivos modificados**:
   ```bash
   git add .
   ```
4. **Criar commit com mensagem**:
   ```bash
   git commit -m "Implementação da classe X"
   ```
5. **Enviar para o GitHub**:
   ```bash
   git push
   ```

---

## ✅ Dicas importantes
- Sempre escreva mensagens de commit **claras e curtas**.
- Antes de iniciar um trabalho, faça sempre um `git pull` para garantir que está atualizado.
- Se aparecer alguma mensagem de erro, leia com atenção — muitas vezes o Git dá a dica de como corrigir.

---

📌 **Professor responsável**: [Salatiel Luz Marinho](https://github.com/ProfessorSalatiel)  
📚 **Disciplina**: Programação Orientada a Objetos II  
🏫 **Curso**: Análise e Desenvolvimento de Sistemas  