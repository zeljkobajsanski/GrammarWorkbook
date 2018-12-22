<template>
    <article>
        <b-card>
            <div class="row">
                <div class="col-4">
                    <input type="text" v-model="topic.title" placeholder="Title" class="form-control">
                </div>
                <div class="col-4">
                    <input type="text" v-model="topic.subtitle" placeholder="Subtitle" class="form-control">
                </div>
                <div class="col-4">
                    <button class="btn btn-danger">Confirm</button>
                </div>
            </div>
            <p style="margin-top: 10px">
                Exercises
                <button class="btn btn-xs btn-primary" @click="addExercise" title="Add exercise">
                    <i class="fa fa-plus"></i>
                </button>
            </p>
            <b-card v-for="(exercise, ix) in topic.exercises" style="margin-bottom: 5px">
                <i class="fa fa-times-rectangle-o pull-right pointer" title="Remove" @click="removeExerciseAt(ix)"></i>
                <div class="clearfix" style="margin-bottom: 10px"></div>
                <form>
                    <div class="form-group row">
                        <label class="col-2 col-form-label">Type</label>
                        <div class="col-10">
                            <select class="form-control">
                                <option value="fill">Fill the blanks</option>
                                <option value="dialogue">Dialog</option>
                            </select>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-2 col-form-label">Title</label>
                        <div class="col-10">
                            <input type="text" class="form-control">
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-2 col-form-label">Options</label>
                        <div class="col-10">
                            <div class="custom-control custom-checkbox">
                                <input type="checkbox" class="custom-control-input" id="useOptions">
                                <label class="custom-control-label" for="useOptions">Use options</label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-2 col-form-label"></label>
                        <div class="col-10">
                            <button class="btn btn-primary" @click="exercise.sentences.push({})">Add sentence</button>
                        </div>
                    </div>
                    <div class="form-group row" v-for="sentence in exercise.sentences">
                        <label class="col-2 col-form-label">Sentence</label>
                        <div class="col-8">
                            <input class="form-control">
                        </div>
                        <div class="col-1">
                            <button type="button" class="btn btn-sm btn-danger" @click="removeSentence(exercise, sentence)">Remove</button>
                        </div>
                    </div>
                    <hr>
                    <button class="btn btn-warning pull-right" type="button">Save</button>
                </form>
            </b-card>
        </b-card>
    </article>
</template>

<script lang="ts">
    import Vue from 'vue'
    import {Component, Prop, Watch} from "vue-property-decorator";
    import * as _ from 'lodash'

    @Component({})
    export default class TopicEdit extends Vue {
        topic = {
            exercises: [],
        };

        removeExerciseAt(index) {
            this.topic.exercises.splice(index, 1);
        }

        addExercise() {
            const exercise = {
                sentences: []
            };

            this.topic.exercises.push(exercise);
        }

        removeSentence(exercise, sentence) {
            const ix = exercise.sentences.indexOf(sentence);
            if (ix > -1) {
                exercise.sentences.splice(ix, 1);
            }
        }
    }
</script>

<style scoped>
    .option {
        margin-right: 10px;
        background-color: rgb(248, 248, 248);
        border-radius: 4px;
        padding: 4px;
        border: 1px solid rgb(166, 166, 166);
    }

    .pointer {
        cursor: pointer;
    }
</style>