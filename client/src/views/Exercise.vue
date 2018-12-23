<template>
    <article>
        <h1 id="title">{{unit.title}}</h1>
        <div class="btn-group" id="nav-buttons">
            <button class="btn btn-warning">Prev</button>
            <button class="btn btn-warning">Next</button>
        </div>
        <div class="clearfix"></div>
        <hr/>
        <b-card :title="(ix + 1) + '. ' + topic.title" :sub-title="'| ' + topic.subTitle"
                v-for="(topic, ix) in unit.topics" class="mb-4 topic">
            <div class="clearfix"></div>
            <b-card :sub-title="exercise.title" v-for="exercise in topic.exercises" class="md-12"
                    style="margin: 4px 0">
                <div v-if="exercise.type === 'fill'">
                    <ul class="options">
                        <li v-for="option in exercise.options">{{option}}</li>
                    </ul>
                    <ol>
                        <li v-for="sentence in exercise.sentences">
                            <span v-for="word in sentence.words">
                                <span v-if="!word.isBlank" class="token">{{word.text}}</span>
                                <input v-if="word.isBlank && !exercise.options.length" type="text" class="token"
                                       :class="{'wide-textbox': word.isFullSentence, 'short-textbox': !word.isFullSentence}"
                                       v-model="word.text">
                                <select v-if="word.isBlank && exercise.options.length" v-model="word.text">
                                    <option v-for="option in exercise.options" :value="option">{{option}}</option>
                                </select>
                            </span>
                            <p v-if="!sentence.isCorrect" class="text-danger correctText">{{sentence.correctText}}</p>
                        </li>
                    </ol>
                </div>
               <!-- <div v-if="exercise.type === 'match'" id="matcher">
                    <p class="text-muted" style="font-size: 12px; margin-left: 4px">
                        Drag and drop words from the right panel to the middle and vice versa
                    </p>
                    <div class="row">
                        <div class="col-md-4">
                            <ol>
                                <li v-for="item in exercise.left" class="match-iitem">{{item.text}}</li>
                            </ol>
                        </div>
                        <div class="col-md-4">
                            <ol>
                                <drag drop-effect="move" v-for="item in exercise.right" :transfer-data="item">
                                    <drop @drop="droppedIntoMiddlePanel(item, ...arguments)">
                                        <li class="match-iitem draggable"></li>
                                    </drop>
                                </drag>
                            </ol>
                        </div>
                        <div class="col-md-4">
                            <drop @drop="droppedIntoRightPanel">
                                <ol>
                                    <drag :effect-allowed="'move'" @drag="dragStart" :transfer-data="item"
                                          v-for="item in exercise.right">
                                        <li class="match-iitem draggable">
                                            {{item.text}}
                                        </li>
                                    </drag>
                                </ol>
                            </drop>
                        </div>
                    </div>
                </div>-->
                <!--<div v-if="exercise.type === 'dialogue'">
                    <ol>
                        <li v-for="d in exercise.dialogs">
                            <ul>
                                <li v-for="line in d.dialog" style="list-style: none">
                                   <span v-for="word in line.sentences">
                                      <span v-if="!word.isBlank" class="token">{{word.text}}</span>
                                       <input v-if="word.isBlank && !line.options" type="text" class="token"
                                              :class="{'wide-textbox': word.isFullSentence, 'short-textbox': !word.isFullSentence}"
                                              v-model="word.text">
                                       <select v-if="word.isBlank && line.options" v-model="word.text">
                                           <option v-for="option in line.options" :value="option">{{option}}</option>
                                       </select>
                                   </span>
                                </li>
                            </ul>
                        </li>
                    </ol>
                </div>-->
            </b-card>
        </b-card>
        <div class="float-right btn-group">
            <button class="btn btn-warning" @click="reset">Reset</button>
            <button class="btn btn-danger" @click="checkResults">Commit</button>
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

        created() {
            this.reset();
        }

        dragStart() {
            console.log('Drag start');
        }

        droppedIntoRightPanel(data: any) {
            console.log('Dropped into right panel ' + data.id);
        }

        droppedIntoMiddlePanel(item: any, data: any) {
            console.log(`Dropped into middle panel (item=${item}, data=${data}`);
        }

        async checkResults() {
            try {
                const {data} = await rest.checkResults(this.unit);
                this.unit = data;
            } catch(err) {
                this.$toasted.error('Failed to validate results');
            }
        }

        async reset() {
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
        outline: none;
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
        border: 1px solid rgb(166, 166, 166);
    }

    input.wide-textbox {
        width: 50%
    }

    input.short-textbox {
        width: 80px;
    }

    li.match-iitem {
        border: 1px solid rgb(166, 166, 166);
        padding: 4px;
        background-color: rgb(248, 248, 248);
        list-style: none;
        -webkit-user-select: none;
        -moz-user-select: none;
        -ms-user-select: none;
        user-select: none;
        min-height: 34px;
    }

    .draggable {
        cursor: pointer;
    }

    #matcher ol {
        border: 1px solid rgb(166, 166, 166);
        padding: 4px;
        min-height: 200px;
    }

    .correctText {
        font-size: 12px;
    }
</style>