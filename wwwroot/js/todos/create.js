var vueApp = new Vue({
    el: '#app',
    data: {
        todos: [],
        useDueDate: false,
        selectedTodo: null
    },
    methods: {
        loadTodos : function(e) {
            axios.get("/api/todos")
            .then(response => this.todos = response.data)
            .catch(error => alert(error));
        },
        addDueDate: function (e) {
            this.useDueDate = true;
            console.log(this.dueDate);
        }
    }
})

vueApp.loadTodos();