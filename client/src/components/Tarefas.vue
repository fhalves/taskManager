<template>
<v-container>
  <br>
  <v-form
    ref="form"
    v-model="valid"
    lazy-validation
  >
  <v-row>
    <v-col cols="12">
    <v-text-field
      v-model="obj.description"
      :counter="100"
      :rules="parametroObrigatorio"
      label="Tarefa"
      required
    ></v-text-field>
    </v-col>
    <v-col cols="12">
      <v-combobox
        v-model="obj.usuario"
        :items="users"
        item-text="nome"
        item-value="codigo"
        label="Usuário"
      ></v-combobox>
    </v-col>
    </v-row>
    <br>
    <v-btn
      :disabled="!valid"
      color="primary"
      class="mr-4"
      @click="validate"
    >
      Enviar
    </v-btn>
     <v-btn
      color="primary"
      @click="reset"
    >
      Resetar Formulário
    </v-btn>
    <br><br>
     <v-alert
      v-if="alertWarning"
      dense
      border="left"
      type="warning"
    >
    {{notification}}
    </v-alert>
     <v-alert
      v-if="alertSuccess"
      dense
      border="left"
      type="success"
    >
      Cadastro efetuado com sucesso!
    </v-alert>
  </v-form>
  </v-container>
</template>

<script>
/* eslint-disable */
import axios from "axios";
import tarefaService from "../services/tarefaService.js";

  export default {
    name: "CadastroUsuario",
    data: () => ({
        select: [],
        users: [
        
        ],
      alertWarning: false,
      alertSuccess: false,
      notification: "",
      obj: {
        description: "",
        concluded: false,
        usuario: "",
        userId:""
      },
      valid: true,
      name: '',
      parametroObrigatorio: [
        v => !!v || 'Parâmetro obrigatório.'
      ],
    }),
  created(){
    this.carregarUsuarios();
  },
    methods: {
      validate () {
          if(this.$refs.form.validate()){
            this.cadastro();
          }
      },
      reset () {
        this.alertWarning= false;
        this.alertSuccess = false;
        this.$refs.form.reset()
      },
      carregarUsuarios(){
        axios.get('https://localhost:44392/api/user',
        { headers: {
            'Access-Control-Allow-Origin': '*',
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + localStorage.getItem('jAuth'),
          }
        }).then(response => {
            for(let i = 0; i < response.data.data.length; i++){
              var obj = {
                codigo: response.data.data[i].id,
                nome: response.data.data[i].name
              }

              this.users.push(obj);
            }
        })
        .catch(error => {
            if (error.response.status === 401) {
            localStorage.clear()
            location.href="/login";
        }
        })
      },  
      cadastro(){
      this.obj.userId = this.obj.usuario.codigo;
      tarefaService.cadastrar(this.obj).then(r =>{
        this.alertWarning = false;
        this.alertSuccess = true;
      }).catch(error => {
         if (error.response.status === 401) {
            localStorage.clear()
            location.href="/login";
        }
        this.notification = error.response.data.notifications[0].value
        this.alertWarning = true;
        this.alertSuccess = false;
      });
    }
    },
  }
</script>