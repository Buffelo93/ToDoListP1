<template>
    <div class="todo-item">
        <div class="todo-item-left">
            <input type="checkbox" @change="updateCheck()" v-model="isCompleted">
            <div v-if="note == null" class="todo-item-label"
                :class="{ isCompleted: isCompleted }">{{ title }}</div>
            <div v-else class="todo-item-label" :class="{ isCompleted: isCompleted }">{{ title }} - {{ note }}</div>
        </div>
        <div class="todo-item-right">
            <div class="deadline">
                {{ deadLine != null ? formatDate(deadLine) : '' }}
            </div>
            <div class="remove-item" @click="removeTodo(id)">
                &times;
            </div>
        </div>
    </div>
</template>
  
<script>
const API_ULR = 'http://localhost:5135/';

export default {
    name: 'todo-item',
    props: {
        todo: {
            type: Object,
            required: true,
        },
        checkAll: {
            type: Boolean,
            required: true,
        }
    },
    data() {
        return {
            'id': this.todo.ToDoId,
            'title': this.todo.Title,
            'note': this.todo.Note,
            'isCompleted': this.todo.IsComplete,
            'deadLine': this.todo.DeadLine,
        }
    },
    watch: {
        checkAll() {
            this.isCompleted = this.checkAll ? true : this.todo.isCompleted
        }
    },
    methods: {
        async removeTodo(id) {
            axios.delete(API_ULR+"api/ToDo/DeleteToDoItem?toDoId="+id)
            .then(
                (response)=>{
                    console.log(response.data);
                    this.$emit('removedTodo', id)
                }
            )
        },
        async updateCheck() {
            axios.post(API_ULR+"api/ToDo/UpdateIsComplete",{
                ToDoId: this.id,
                IsComplete: this.isCompleted
            }).then(
                (response)=>{
                    console.log(response.data);
                }
            )
        },
        formatDate(dateString) {
            const date = new Date(dateString);
            return new Intl.DateTimeFormat('default', { dateStyle: 'long' }).format(date);
        },
    }
}
</script>