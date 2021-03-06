# API Integração Gist

Web API para criar um Gist e listar comentários de um Gist.


## Geral

O projeto foi desenvolvido no Visual Studio Community 2017. Para utilizar a aplicação, a mesma deve ser compilada local e deve ser utilizado o [Postman](https://www.getpostman.com/apps) para realizar a execução dos endpoints.


### Utilizando a API ...

As requisições a serem executadas na API Integração Gist são as seguintes:

* POST que cria um Gist (onde deve ser informada uma descrição para o Gist, o nome do arquivo e sua extensão):
```
http://localhost:57796/api/IntegrationToGist/{descrição}/{nome}/{extensão}
```

* GET que retorna todos os comentários de um Gist padrão definido no código-fonte (Fields na classe IntegrationClient):
```
http://localhost:57796/api/IntegrationToGist
```

* GET que retorna todos os comentários de um Gist informado pelo usuário:
```
http://localhost:57796/api/IntegrationToGist/{ID do Gist}
```


## Autor

* **Daiana Vargas** - *Segundo projeto no GitHub* 


## Licença

O projeto utiliza a licença MIT.

