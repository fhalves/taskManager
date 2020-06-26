<template>
  <v-app>
    <v-app-bar
      app
      color="primary"
      dark
    >
      <span @click="redirect"><a href="" style="color:white;text-decoration:none">Task Manager</a></span>

      <v-spacer></v-spacer>
    </v-app-bar>

    <v-content>
      <router-view/>
    </v-content>
  </v-app>
</template>

<script>

export default {
  name: 'App',

  data: () => ({
    //
  }),
  methods:{
    redirect(){
      location.href = "/login";
    }
  }
};
</script>
