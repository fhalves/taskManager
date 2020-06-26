import axios from "axios";

var apiAddress = "https://localhost:44392/api/task";

export default {
    cadastrar(data) {
        //eslint-disable-next-line
        var postPromisse = axios.post(apiAddress, data, {
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem('jAuth'),
            }
        });

        //eslint-disable-next-line
        postPromisse.catch(error => {});

        return postPromisse;
    }
}