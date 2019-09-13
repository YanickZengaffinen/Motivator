var vueApp = new Vue({
    el: '#app',
    data: {
        tasks: []
    },
    methods: {
        loadTasks : function(e) {
            axios.get("/api/tasks")
            .then(response => this.tasks = response.data)
            .catch(error => alert(error));
        }
    }
})

vueApp.loadTasks();