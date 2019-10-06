var vueApp = new Vue({
    el: '#app',
    data: {
        todos: []
    },
    methods: {
        loadTodos : function(e) {
            axios.get("/api/todos")
            .then(response => this.todos = response.data)
            .catch(error => alert(error));
        },
        onCompleteChanged: function (id, completed) {
            axios.get("/api/todos/complete?taskId=" + id + "&isComplete=" + completed)
                .then(response => console.log(response.data))
                .catch(error => alert(error));
        },
        prettifyDate: function (dStr) {
            if (dStr) {
                var d = new Date(dStr);
                return d.getDate() + "-" + (d.getMonth() + 1) + "-" + d.getFullYear() + " " +
                    d.getHours() + ":" + d.getMinutes();
            }
            return "-";
        }
    },
    computed: {
        sortedTodos: function () {
            return this.todos.sort((a,b) => compare(a.Title, b.Title));
        }
    }
})

vueApp.loadTodos();

function compare(a, b) {
    return a < b ? -1 : a > b;
}