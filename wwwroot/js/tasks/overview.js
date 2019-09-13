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
    },
    computed: {
        sortedTasks: function () {
            return this.tasks.sort((a,b) => compare(a.Title, b.Title));
        }
    }
})

vueApp.loadTasks();

function compare(a, b) {
    return a < b ? -1 : a > b;
}