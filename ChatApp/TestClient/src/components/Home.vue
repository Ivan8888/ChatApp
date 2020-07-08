<template>
    <div id="chatApp">
        <div class="form-group">
            <form>
                <div>
                    <label for="userName">Your Name</label>
                    <input type="text" name="Name" id="userName" v-model="userName" />
                </div>
                <div>
                    <label for="userMessage">Message</label>
                    <input type="text"
                           name="Message"
                           id="userMessage"
                           v-model="userMessage" />
                </div>
                <button v-on:click="submitCard" type="button">Submit</button>
            </form>
            <ul v-for="(item, index) in messages" v-bind:key="index + 'itemMessage'">
                <li><b>Name: </b>{{item.user}}</li>
                <li><b>Message: </b>{{item.message}}</li>
            </ul>
        </div>
    </div>
</template>

<script>
    document.addEventListener("DOMContentLoaded", function (event) {
        var app = new Vue({
            el: "#chatApp",
            data: {
                userName: "",
                userMessage: "",
                connection: "",
                messages: []
            },
            methods: {
                submitCard: function () {
                    if (this.userName && this.userMessage) {
                        // ---------
                        //  Call hub methods from client
                        // ---------
                        this.connection
                            .invoke("SendMessage", this.userName, this.userMessage)
                            .catch(function (err) {
                                return console.error(err.toSting());
                            });

                        this.userName = "";
                        this.userMessage = "";
                    }
                }
            },
            created: function () {
                // ---------
                // Connect to our hub
                // ---------
                this.connection = new signalR.HubConnectionBuilder()
                    .withUrl("/chatHub")
                    .configureLogging(signalR.LogLevel.Information)
                    .build();
                this.connection.start().catch(function (err) {
                    return console.error(err.toSting());
                });
            },
            mounted: function () {
                // ---------
                // Call client methods from hub
                // ---------
                var thisVue = this;
                thisVue.connection.start();
                thisVue.connection.on("ReceiveMessage", function (user, message) {
                    thisVue.messages.push({ user, message });
                });
            }
        });
    });
</script>

<script src="~/signalr.js"></script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>

