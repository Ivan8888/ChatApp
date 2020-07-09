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
    import * as signalR from '@aspnet/signalr';

    export default{
        data(){
            return {
                userName: "",
                userMessage: "",
                connection: "",
                messages: []
            }
        },
        
        methods: {
            submitCard: function () {
                console.log("before connection");
                this.connection = new signalR.HubConnectionBuilder()
                    .withUrl("http://localhost:60588/chathub")
                    .configureLogging(signalR.LogLevel.Information)
                    .build();
                this.connection.start().catch(function (err) {
                    return console.error(err.toString());
                });
                console.log("after connection");

                console.log("log message");
                if (this.userName && this.userMessage) {
                    // ---------
                    //  Call hub methods from client
                    // ---------
                    this.connection
                        .invoke("MessageAll", this.userMessage, "Grupa")
                        .catch(function (err) {
                            return console.error(err.toString());
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
            //this.connection = new signalR.HubConnectionBuilder()
            //    .withUrl("http://localhost:60588/chathub")
            //    .configureLogging(signalR.LogLevel.Information)
            //    .build();
            //this.connection.start().catch(function (err) {
            //    return console.error(err.toString());
            //});
        },

        mounted: function () {
            // ---------
            // Call client methods from hub
            // ---------
            console.log("before connection");
            this.connection = new signalR.HubConnectionBuilder()
                .withUrl("http://localhost:60588/chathub")
                .configureLogging(signalR.LogLevel.Information)
                .build();
            this.connection.start().catch(function (err) {
                return console.error(err.toString());
            });
            console.log("after connection");

            var thisVue = this;
            thisVue.connection.on("ReceiveMessage", function (user, message) {
                thisVue.messages.push({ user, message });
            });
        }
    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>

