<template>
    <div>
        <div class="add-button-container">
            <button class="button" @click="addTodo">Add ToDo Item</button>
        </div>
        <div class="user-inputs">
            <input type="text" class="todo-input" placeholder="What's the next task? (Required)" v-model="newTodo"
                @keyup.enter="addTodo" maxlength="25">
            <input type="text" class="todo-input" placeholder="Add a note... (Optional)" v-model="newNote"
                @keyup.enter="addTodo" maxlength="100">
            <Datepicker v-model="newDeadLine" />
        </div>
        <transition-group name="fade" enter-active-class="animated fadeInUp" leave-active-class="animated fadeOutDown">
            <todo-item v-for="todo in todosFiltered" :key="todo.ToDoId" :todo="todo" :checkAll="!anyRemaining"
                @removedTodo="removeTodo" @finishedEdit="finishedEdit">
            </todo-item>
        </transition-group>

        <div class="extra-container">
            <div><label><input type="checkbox" :checked="!anyRemaining" @change="checkAllTodos"> Check All</label></div>
            <div>{{ remaining }} items left</div>
        </div>

        <div class="extra-container">
            <div class="button-container">
                <button class="button" :class="{ active: filter == 'all' }" @click="filter = 'all'">All</button>
                <button class="button" :class="{ active: filter == 'active' }"
                    @click="filter = 'active'">Active</button>
                <button class="button" :class="{ active: filter == 'completed' }"
                    @click="filter = 'completed'">Completed</button>
            </div>
        </div>

        <div v-if="showClearCompletedButton" class="extra-container">
            <div class="button-container">
                <transition name="fade">
                    <button class="button" @click="clearCompleted">Clear Completed</button>
                </transition>
            </div>
        </div>

    </div>
</template>

<script>
import TodoItem from './ToDoItem.vue'
import Datepicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css';

const API_ULR = 'http://localhost:5135/';

export default {
    name: 'todo-list',
    components: {
        TodoItem,
        Datepicker,
    },
    data() {
        return {
            newTodo: '',
            newNote: null,
            newDeadLine: null,
            filter: 'all',
            todos: []
        }
    },
    computed: {
        remaining() {
            return this.todos.filter(todo => !todo.IsComplete).length
        },
        anyRemaining() {
            return this.remaining != 0
        },
        todosFiltered() {
            if (this.filter == 'all') {
                return this.todos
            } else if (this.filter == 'active') {
                return this.todos.filter(todo => !todo.IsComplete)
            } else if (this.filter == 'completed') {
                return this.todos.filter(todo => todo.IsComplete)
            }

            return this.todos
        },
        showClearCompletedButton() {
            return this.todos.filter(todo => todo.IsComplete).length > 0
        }
    },
    methods: {
        async refreshData() {
            axios.get(API_ULR + "api/ToDo/GetToDoItems").then(
                (response) => {
                    console.log(response.data);
                    this.todos = response.data;
                    this.newTodo = '';
                    this.newNote = null;
                    this.newDeadLine = null;
                }
            )
        },
        async addTodo() {
            if (this.newTodo.trim().length == 0) {
                return
            }

            axios.post(API_ULR + "api/ToDo/AddToDoItem", {
                ToDoId: 0,
                Title: this.newTodo,
                Note: this.newNote,
                IsComplete: false,
                DeadLine: this.newDeadLine
            }).then(
                (response) => {
                    console.log(response.data);
                    this.refreshData();
                }
            )
        },
        removeTodo(id) {
            const index = this.todos.findIndex((item) => item.ToDoId == id)
            this.todos.splice(index, 1)
        },
        checkAllTodos() {
            this.todos.forEach((todo) => todo.IsComplete = event.target.checked)
        },
        clearCompleted() {
            this.todos = this.todos.filter(todo => !todo.IsComplete)
        },
        finishedEdit(data) {
            const index = this.todos.findIndex((item) => item.id == data.id)
            this.todos.splice(index, 1, data)
        },
    },
    mounted: function () {
        this.refreshData();
    },
}
</script>

<style lang="scss">
@import url("https://cdnjs.cloudflare.com/ajax/libs/animate.css/3.5.2/animate.min.css");

.user-inputs input {
    display: block;
    display: flex;
    flex-direction: row;
    margin-bottom: 16px;
}

.todo-input {
    width: 100%;
    padding: 10px 18px;
    font-size: 18px;
    margin-bottom: 16px;

    &:focus {
        outline: 0;
    }
}

.todo-item {
    margin-bottom: 12px;
    display: flex;
    align-items: center;
    justify-content: space-between;
    margin: 0 auto;
    animation-duration: 0.3s;
}

.todo-item-right {
    display: flex;
    align-items: center;
}

.deadline {
    font-size: small;
    font-style: italic;
}

.remove-item {
    cursor: pointer;
    display: flex;
    align-items: center;
    margin-left: 10px;

    &:hover {
        color: black;
    }
}

.todo-item-left {
    display: flex;
    align-items: center;
}

.todo-item-label {
    padding: 10px;
    margin-left: 12px;
    max-width: 400px;
    word-wrap: break-word;
}

.todo-note-label {
    padding: 5px;
    border: 1px solid white;
    margin-left: 1px;
}

.todo-item-edit {
    font-size: 24px;
    color: #2c3e50;
    margin-left: 12px;
    width: 100%;
    padding: 10px;
    font-family: 'Avenir', Helvetica, Arial, sans-serif;

    &:focus {
        outline: none;
    }
}

.completed {
    text-decoration: line-through;
    color: grey;
}

.extra-container {
    display: flex;
    align-items: center;
    justify-content: space-between;
    font-size: 16px;
    border-top: 1px solid lightgrey;
    padding-top: 14px;
    margin-bottom: 14px;
}

.button-container {
    display: flex;
    align-items: center;
    font-size: 24px;
    padding-top: 14px;
    margin-bottom: 14px;
    margin: auto;
}

.add-button-container {
    display: flex;
    align-items: center;
    justify-content: center;
    font-size: 16px;
    padding-top: 14px;
    margin-bottom: 14px;
}

.button {
    align-items: center;
    background-color: #FFFFFF;
    border: 1px solid rgba(0, 0, 0, 0.1);
    border-radius: .25rem;
    box-shadow: rgba(0, 0, 0, 0.02) 0 1px 3px 0;
    box-sizing: border-box;
    color: rgba(0, 0, 0, 0.85);
    cursor: pointer;
    display: inline-flex;
    font-family: system-ui, -apple-system, system-ui, "Helvetica Neue", Helvetica, Arial, sans-serif;
    font-size: 24px;
    font-weight: 600;
    justify-content: center;
    line-height: 1.25;
    margin: 0;
    min-height: 3rem;
    padding: calc(.875rem - 1px) calc(1.5rem - 1px);
    position: relative;
    text-decoration: none;
    transition: all 250ms;
    user-select: none;
    -webkit-user-select: none;
    touch-action: manipulation;
    vertical-align: baseline;
    width: auto;
}

.button:hover,
.button:focus {
    border-color: rgba(0, 0, 0, 0.15);
    box-shadow: rgba(0, 0, 0, 0.1) 0 4px 12px;
    color: rgba(0, 0, 0, 0.65);
}

.button:hover {
    transform: translateY(-1px);
}

.button:active {
    background-color: #F0F0F1;
    border-color: rgba(0, 0, 0, 0.15);
    box-shadow: rgba(0, 0, 0, 0.06) 0 2px 4px;
    color: rgba(0, 0, 0, 0.65);
    transform: translateY(0);
}

.active {
    background: lightgreen;
}

.fade-enter-active,
.fade-leave-active {
    transition: opacity .2s;
}

.fade-enter,
.fade-leave-to {
    opacity: 0;
}
</style>