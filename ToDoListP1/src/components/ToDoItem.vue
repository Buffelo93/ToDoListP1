<template>
    <div class="todo-item">
        <div class="todo-item-left">
            <input type="checkbox" v-model="isCompleted" @change="doneEdit">
            <div v-if="note.trim().length == 0" @dblclick="editTitle" class="todo-item-label"
                :class="{ isCompleted: isCompleted }">{{ title }}</div>
            <div v-else class="todo-item-label"
                :class="{ isCompleted: isCompleted }">{{ title }} - {{ note }}</div>
        </div>
        <div class="remove-item" @click="removeTodo(todo.id)">
            &times;
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
        doneEdit() {
            if (this.title.trim() == '') {
                this.title = this.beforeEditCache
            }
            this.editingTitle = false
            this.editingNote = false
            this.$emit('finishedEdit', {
                'id': this.id,
                'title': this.title,
                'isCompleted': this.isCompleted,
                'editingTitle': this.editingTitle,
                'editingNote': this.editingNote,
            })

        },
    }
}
</script>