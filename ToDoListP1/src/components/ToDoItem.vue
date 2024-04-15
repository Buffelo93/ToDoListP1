<template>
    <div class="todo-item">
        <div class="todo-item-left">
            <input type="checkbox" @change="updateCheck(this.todo.ToDoId, this.todo.IsComplete)" v-model="this.todo.IsComplete">
            <div class="todo-item-text">
                <div class="todo-item-label">{{ this.todo.Title }}</div>
                <div v-if="this.todo.Note != null" class="todo-note-label">- {{ this.todo.Note }}</div>
            </div>
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

<style>

.todo-item-right {
    display: flex;
    align-items: center;
}

.deadline {
    font-size: small;
    font-style: italic;
    margin-right: 10px;
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
    font-size: 16px;
    padding: 2px 5px 5px 5px;
    margin-left: 20px;
    max-width: 400px;
    word-wrap: break-word;
}

</style>