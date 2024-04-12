<template>
    <div class="todo-item">
        <div class="todo-item-left">
            <input type="checkbox" @change="updateCheck(this.todo.ToDoId, this.todo.IsComplete)" v-model="this.todo.IsComplete">
            <div v-if="this.todo.Note == null" class="todo-item-label">{{  this.todo.Title }}</div>
            <div v-else class="todo-item-label">{{ this.todo.Title }} - {{  this.todo.Note }}</div>
        </div>
        <div class="todo-item-right">
            <div class="deadline">
                {{ this.todo.DeadLine != null ? formatDate(this.todo.DeadLine) : '' }}
            </div>
            <div class="remove-item" @click="removeTodo(this.todo.ToDoId)">
                &times;
            </div>
        </div>
    </div>
</template>

<script>

export default {
    name: 'todo-item',
    props: {
        todo: {
            type: Object,
            required: true,
        }
    },
    methods: {
        removeTodo(id) {
            this.$emit('removedTodo', id)
        },
        updateCheck(id, isComplete) {
            this.$emit('checkChanged', id, isComplete)
        },
        formatDate(dateString) {
            const date = new Date(dateString);
            return new Intl.DateTimeFormat('default', { dateStyle: 'long' }).format(date);
        }
    }
}
</script>