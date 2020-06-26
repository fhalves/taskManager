<template>
    <v-content>
        <v-row
          align="center"
          justify="center"
        >
          <v-col
            cols="12"
            sm="8"
            md="4"
          >
            <v-card class="elevation-12">
              <v-toolbar
                color="primary"
                dark
                flat
              >
                <v-toolbar-title>Login</v-toolbar-title>
                <v-spacer></v-spacer>
                <v-alert
                  v-if="alertWarning"
                  dense
                  border="left"
                  type="warning"
                >
                  Falha na autenticação
                </v-alert>
              </v-toolbar>
              <v-card-text>
                <v-form>
                  <v-text-field
                    label="Login"
                    v-model="obj.user"
                    name="login"
                    prepend-icon="mdi-account"
                    type="text"
                  ></v-text-field>

                  <v-text-field
                    label="Password"
                    v-model="obj.password"
                    name="password"
                    prepend-icon="mdi-lock"
                    type="password"
                  ></v-text-field>
                </v-form>
              </v-card-text>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="primary" @click="login">Login</v-btn>
                <v-btn color="white" style="color:white"><router-link to="cadastro">Cadastro</router-link></v-btn>
              </v-card-actions>
            </v-card>
          </v-col>
        </v-row>
    </v-content>
</template>


<script>
/* eslint-disable */
import loginService from "../services/loginService.js";
export default {
  name: 'Login',
  data(){
    return {
      alertWarning: false,
      obj: {
        user: "",
        password : ""
      }
    }
  },
  created(){
    localStorage.clear();
  },
  methods:{
    login(){
      loginService.login(this.obj).then(r =>{
        localStorage.setItem("jAuth", r.data.data.token);
        this.alertWarning = false;
        location.href = "/tarefas"
      }).catch(e => {
        this.alertWarning = true;
        localStorage.clear();
      });
    }
  }
}
</script>