<template>
<v-container>
  <br>
  <v-form
    ref="form"
    v-model="valid"
    lazy-validation
  >
    <v-text-field
      v-model="obj.name"
      :counter="100"
      :rules="parametroObrigatorio"
      label="Nome"
      required
    ></v-text-field>

    <v-text-field
      v-model="obj.email"
      :rules="emailRules"
      label="E-mail"
      required
    ></v-text-field>

    <v-text-field
      v-model="obj.login"
      :rules="parametroObrigatorio"
      label="Login"
      required
    ></v-text-field>

     <v-text-field
      v-model="obj.password"
      :rules="parametroObrigatorio"
      label="Senha"
      type="password"
      required
    ></v-text-field>
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

import cadastroService from "../services/cadastroService.js";

  export default {
    name: "CadastroUsuario",
    data: () => ({
      alertWarning: false,
      alertSuccess: false,
      notification: "",
      obj: {
        name:"",
        email:"",
        login:"",
        password:""
      },
      valid: true,
      name: '',
      parametroObrigatorio: [
        v => !!v || 'Parâmetro obrigatório.'
      ],
      email: '',
      emailRules: [
        v => !!v || 'Informe o email',
        v => /.+@.+\..+/.test(v) || 'Email inválido',
      ]
    }),

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
      cadastro(){
      cadastroService.cadastrar(this.obj).then(r =>{
        this.alertWarning = false;
        this.alertSuccess = true;
        setTimeout(function(){
          alert('Você será redirecionado para o login.'); 
          location.href="/login";
        }, 2000);
        
      }).catch(error => {
        if (error.response.status === 401) {
            localStorage.clear()
            location.href="/login";
        }
        this.notification = error.response.data.notifications[0].value
        console.log(error.response)
        this.alertWarning = true;
        this.alertSuccess = false;
      });
    }
    },
  }
</script>