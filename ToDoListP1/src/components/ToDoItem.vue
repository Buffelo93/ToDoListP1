<template>
    <div class="todo-item">
        <div class="todo-item-left">
            <input type="checkbox" v-model="isCompleted">
            <div v-if="note.trim().length == 0" @dblclick="editTitle" class="todo-item-label"
                :class="{ isCompleted: isCompleted }">{{ title }}</div>
            <div v-else class="todo-item-label" :class="{ isCompleted: isCompleted }">{{ title }} - {{ note }}</div>
        </div>
        <div class="todo-item-right">
            <div class="deadline">
                {{ formatDate(deadLineDateTime) }}
            </div>
            <div class="remove-item" @click="removeTodo(todo.id)">
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
        },
        checkAll: {
            type: Boolean,
            required: true,
        }
    },
    data() {
        return {
            'id': this.todo.id,
            'title': this.todo.title,
            'note': this.todo.note,
            'isCompleted': this.todo.isCompleted,
            'deadLineDateTime': this.todo.deadLineDateTime,
        }
    },
    watch: {
        checkAll() {
            this.isCompleted = this.checkAll ? true : this.todo.isCompleted
        }
    },
    methods: {
        removeTodo(id) {
            this.$emit('removedTodo', id)
        },
        formatDate(dateString) {
            const date = new Date(dateString);
            // Then specify how you want your dates to be formatted
            return new Intl.DateTimeFormat('default', { dateStyle: 'long' }).format(date);
        },
    }
}
</script>