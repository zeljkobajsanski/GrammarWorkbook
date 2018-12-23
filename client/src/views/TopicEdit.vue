<template>
    <article>
        <b-card>
            <div class="row">
                <div class="col-4">
                    <input type="text" v-model="topic.title" placeholder="Title" class="form-control">
                </div>
                <div class="col-4">
                    <input type="text" v-model="topic.subTitle" placeholder="Subtitle" class="form-control">
                </div>
                <div class="col-4">
                    <button class="btn btn-danger" @click="saveTopic" :disabled="!topic.title">Confirm</button>
                </div>
            </div>
            <p style="margin-top: 10px" v-if="topic.id">
                Exercises <i class="fa fa-plus-circle text-primary pointer" @click="addExercise" title="Add exercise"></i>
            </p>
            <b-card v-for="(exercise, ix) in topic.exercises" :key="ix" style="margin-bottom: 5px">
                <i class="fa fa-times-rectangle-o pull-right pointer" title="Remove" @click="removeExercise(exercise)"></i>
                <div class="clearfix" style="margin-bottom: 10px"></div>
                <form>
                    <div class="form-group row">
                        <label class="col-2 col-form-label">Type</label>
                        <div class="col-10">
                            <select class="form-control" v-model="exercise.type">
                                <option value="fill">Fill the blanks</option>
                            </select>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-2 col-form-label">Title</label>
                        <div class="col-10">
                            <input type="text" class="form-control" v-model="exercise.title">
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-2 col-form-label">Options</label>
                        <div class="col-10">
                            <div class="custom-control custom-checkbox">
                                <input type="checkbox" class="custom-control-input" :id="'useOptions' + ix" v-model="exercise.useOptions">
                                <label class="custom-control-label" :for="'useOptions' + ix" style="margin-right: 40px">Use options</label>

                                <input type="checkbox" class="custom-control-input" :id="'useDialog' + ix" v-model="exercise.isDialog">
                                <label class="custom-control-label" :for="'useDialog' + ix">as dialog</label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-2 col-form-label"></label>
                        <div class="col-10">
                            <button class="btn btn-primary" type="button" @click="exercise.sentences.push({})">Add sentence</button>
                        </div>
                    </div>
                    <div class="form-group row" v-for="(sentence, sentenceIndex) in exercise.sentences">
                        <label class="col-2 col-form-label">Sentence</label>
                        <div class="col-9">
                            <input class="form-control" v-model="sentence.text">
                        </div>
                        <div class="col-1">
                            <button type="button" class="btn btn-sm btn-danger" @click="removeSentence(exercise, sentence)">Remove</button>
                        </div>
                    </div>
                    <hr>
                    <button class="btn btn-warning pull-right" type="button" @click="saveExercise(exercise)">Save</button>
                </form>
            </b-card>
        </b-card>
    </article>
</template>

<script lang="ts">
    import Vue from 'vue'
    import {Component, Prop, Watch} from "vue-property-decorator";
    import RestClient from '../services/RestClient'
    import * as _ from 'lodash'

    @Component({})
    export default class TopicEdit extends Vue {
        unitId: string;

        topic = {
            unitId: '',
            id: '',
            title: '',
            subtitle: '',
            exercises: [],
        };

        async created() {
            this.unitId = this.$route.params['unitId'];
            this.topic.unitId = this.unitId;
            this.topic.id = this.$route.params['id'];
            if (this.topic.id) {
                const {data} = await RestClient.getTopic(this.topic.id);
                this.topic = data;
            }
        }

        async saveTopic() {
            try {
                const {data} = await RestClient.saveTopic(this.topic);
                this.$toasted.success('Topic has been saved', {icon: 'check'});
                this.topic.id = data.id;
            } catch (err) {
                this.$toasted.error('Failed to save topic');
            }
        }

        async saveExercise(exercise) {
            try {
                const {data} = await RestClient.saveExercise(exercise);
                const ix = this.topic.exercises.indexOf(exercise);
                this.topic[ix] = data;
                this.$toasted.success('Exercise has been saved', {icon: 'check'});
            } catch (err) {
                this.$toasted.error('Failed to save exercise');
            }
        }

        async removeExercise(exercise) {
            try {
                if (exercise.id) {
                    await RestClient.removeExercise(exercise.id);
                }
                const ix = this.topic.exercises.indexOf(exercise);
                this.topic.exercises.splice(ix, 1);
            } catch (err) {
                this.$toasted.error('Failed to remove exercise');
            }

        }

        addExercise() {
            const exercise = {
                type: 'fill',
                title: '',
                topicId: this.topic.id,
                useOptions: false,
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