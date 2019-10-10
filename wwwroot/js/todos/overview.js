var testData = {
    Title: "Test",
    Id: 0,
    children: [
        {
            Id: 1,
            Title: "Sub1",
            children: []
        },
        {
            Id: 2,
            Title: "Sub2",
            children: []
        }
    ]
}

//Tree View
Vue.component('tree-item', {
    template: '#item-template',
    props: {
        item: Object,
        order: Number
    },
    data: function () {
        return {
            isOpen: false
        };
    },
    methods: {
        toggle: function () {
            this.isOpen = !this.isOpen;
            if (this.isOpen && !this.item.children) {
                this.loadTodos();
            }
        },
        loadTodos: function () {
            axios.get("/api/todos/hierarchy?parentId=" + this.item.Id)
                .then(response => {
                    this.item.children = response.data;
                    this.$forceUpdate();
                })
                .catch(error => alert(error));
        },
        onCompleteChange: function (id, completed) {
            this.item.IsCompleted = completed;
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
    }
})

var vueApp = new Vue({
    el: '#app',
    data: {
        rootTodos: []
    },
    methods: {
        loadTodos: function () {
            axios.get("/api/todos/hierarchy")
                .then(response => this.rootTodos = response.data)
                .catch(error => alert(error));
        }
    },
    computed: {
        sortedTodos: function () {
            return this.rootTodos.sort((a,b) => compare(a.Title, b.Title));
        }
    }
})

vueApp.loadTodos(); //initially load the root level todos

function compare(a, b) {
    return a < b ? -1 : a > b;
}