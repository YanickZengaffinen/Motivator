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
        sortedTasks: function () {
            return this.tasks.sort((a,b) => compare(a.Title, b.Title));
        }
    }
})

vueApp.loadTasks();

function compare(a, b) {
    return a < b ? -1 : a > b;
}