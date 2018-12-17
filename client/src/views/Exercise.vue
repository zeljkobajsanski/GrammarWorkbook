<template>
    <article>
        <h1 id="title">{{unit.title}}</h1>
        <div class="btn-group" id="nav-buttons">
            <button class="btn btn-warning">Prev</button>
            <button class="btn btn-warning">Next</button>
        </div>
        <div class="clearfix"></div>
        <hr/>
        <b-card :title="(ix + 1) + '. ' + topic.title" :sub-title="'| ' + topic.subTitle" v-for="(topic, ix) in unit.topics" class="mb-4 topic">
            <div class="clearfix"></div>
            <b-card :sub-title="exercise.title" v-for="exercise in topic.exercises" class="md-12"
                    style="margin: 4px 0">
                <ul class="options">
                    <li v-for="option in exercise.options">{{option}}</li>
                </ul>
                <ol>
                    <li v-for="sentence in exercise.sentences">
                        <span v-for="word in sentence">
                            <span v-if="!word.isBlank" class="token">{{word.text}}</span>
                            <input v-if="word.isBlank && !exercise.options" type="text" class="token" v-model="word.text">
                            <select v-if="word.isBlank && exercise.options" v-model="word.text">
                                <option v-for="option in exercise.options" :value="option">{{option}}</option>
                            </select>
                        </span>
                    </li>
                </ol>
            </b-card>
        </b-card>
        <div class="float-right btn-group">
            <button class="btn btn-warning">Reset</button>
            <button class="btn btn-danger">Commit</button>
        </div>
        <div class="clearfix"></div>
    </article>
</template>

<script lang="ts">
    import Vue from 'vue'
    import {Component, Prop, Watch} from "vue-property-decorator";
    import rest from '@/services/RestClient';

    @Component({})
    export default class Exercise extends Vue {

        unit = {};

        async created() {
            const id = this.$route.params['id'];
            const {data} = await rest.getExerciseUnit(id);
            this.unit = data;
        }
    }
</script>

<style scoped>
    #title {
        float: left;
    }
    #nav-buttons {
        float: right;
    }

    #nav-buttons .btn-warning {
        border-color: #fff !important;
    }

    .token {
        margin: 0 4px;
    }

    li {
        margin: 4px 0;
    }

    .topic .card-title, .topic .card-subtitle {
        display: inline-block;
    }

    .topic .card-subtitle {
        margin-left: 4px;
    }

    input[type="text"] {
        padding: 0 4px;
    }

    .options {
        padding-left: 4px;
    }

    .options li {
        display: inline;
        margin-right: 10px;
        background-color: rgb(248, 248, 248);
        border-radius: 4px;
        padding: 4px;
        border: 1px solid rgb(166, 166, 166);;
    }
</style>