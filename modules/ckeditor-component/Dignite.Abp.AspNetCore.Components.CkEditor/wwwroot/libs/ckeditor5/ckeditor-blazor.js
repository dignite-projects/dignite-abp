
(() => {
    let urlForUploadingImage = '';

    class editorUploadAdapter {
        constructor(loader) {
            // The file loader instance to use during the upload.
            this.loader = loader;
        }


        // Starts the upload process.
        upload() {
            return this.loader.file
                .then(file => new Promise((resolve, reject) => {
                    this._initRequest();
                    this._initListeners(resolve, reject, file);
                    this._sendRequest(file);
                }));
        }

        // Aborts the upload process.
        abort() {
            if (this.xhr) {
                this.xhr.abort();
            }
        }

        // Initializes the XMLHttpRequest object using the URL passed to the constructor.
        _initRequest() {
            const xhr = this.xhr = new XMLHttpRequest();

            // Note that your request may look different. It is up to you and your editor
            // integration to choose the right communication channel. This example uses
            // a POST request with JSON as a data structure but your configuration
            // could be different.
            xhr.open('POST', urlForUploadingImage, true);
            xhr.setRequestHeader('accept', 'application/json');
            xhr.setRequestHeader('accept', 'text/plain');
            xhr.setRequestHeader('accept', '*/*');

            xhr.responseType = 'json';
        }

        // Initializes XMLHttpRequest listeners.
        _initListeners(resolve, reject, file) {
            const xhr = this.xhr;
            const loader = this.loader;
            const genericErrorText = `Couldn't upload file: ${file.name}.`;

            xhr.addEventListener('error', () => reject(genericErrorText));
            xhr.addEventListener('abort', () => reject());
            xhr.addEventListener('load', () => {
                const response = xhr.response;
                // This example assumes the XHR server's "response" object will come with
                // an "error" which has its own "message" that can be passed to reject()
                // in the upload promise.
                //
                // Your integration may handle upload errors in a different way so make sure
                // it is done properly. The reject() function must be called when the upload fails.
                if (!response || response.error) {
                    return reject(response && response.error ? response.error.message : genericErrorText);
                }

                // If the upload is successful, resolve the upload promise with an object containing
                // at least the "default" URL, pointing to the image on the server.
                // This URL will be used to display the image in the content. Learn more in the
                // UploadAdapter#upload documentation.
                resolve({
                    //default: window.location.protocol + "//" + window.location.host + "/api/file-explorer/files/" + response.containerName + "/" + response.blobName
                    default: "/api/file-explorer/files/" + response.containerName + "/" + response.blobName
                });
            });

            // Upload progress when it is supported. The file loader has the #uploadTotal and #uploaded
            // properties which are used e.g. to display the upload progress bar in the editor
            // user interface.
            if (xhr.upload) {
                xhr.upload.addEventListener('progress', evt => {
                    if (evt.lengthComputable) {
                        loader.uploadTotal = evt.total;
                        loader.uploaded = evt.loaded;
                    }
                });
            }
        }

        // Prepares the data and sends the request.
        _sendRequest(file) {
            // Prepare the form data.
            const data = new FormData();
            data.append('File', file, file.name);


            // Send the request.
            this.xhr.send(data);
        }
    }


    function uploadAdapterPlugin(editor) {
        editor.plugins.get('FileRepository').createUploadAdapter = (loader) => {
            // Configure the URL to the upload script in your back-end here!
            return new editorUploadAdapter(loader);
        };
    }

    class BlazorEditor extends ClassicEditor {
        static async create(target, urlToPostImage, options, reference) {

            urlForUploadingImage = urlToPostImage;
            options["extraPlugins"] = [uploadAdapterPlugin];
            options["mediaEmbed"] = {
                extraProviders: [
                    {
                        /**
                         * Adapt to Tencent Video 
                         * https://v.qq.com/x/cover/mzc0020097hxjhz/w0924uwb61a.html
                         */
                        name: 'txp-cover',
                        url: /^https:\/\/v\.qq\.com\/x\/cover\/(\w+)\/(\w+)\.html/,
                        html: match => {
                            return `<div class="ck_media__wrapper" data-oembed-url="">
                                        <iframe style="min-height:498px;width:100%" frameborder="0" src="https://v.qq.com/txp/iframe/player.html?vid=${match[2]}" allowFullScreen="true"></iframe>
                                    </div>`;
                        }
                    },
                    {
                        /**
                         * Adapt to Tencent Video
                         * https://v.qq.com/x/page/p0395vmkypk.html
                         */
                        name: 'txp-page',
                        url: /^https:\/\/v\.qq\.com\/x\/page\/(\w+)\.html/,
                        html: match => {
                            return `<div class="ck_media__wrapper" data-oembed-url="">
                                        <iframe style="min-height:498px;width:100%" frameborder="0" src="https://v.qq.com/txp/iframe/player.html?vid=${match[1]}" allowFullScreen="true"></iframe>
                                    </div>`;
                        }
                    },
                    {
                        /**
                         * Adapt to Tencent Video
                         * https://v.qq.com/x/cover/mzc00200wf2x3yy.html
                         */
                        name: 'txp-cover1',
                        url: /^https:\/\/v\.qq\.com\/x\/cover\/(\w+)\.html/,
                        html: match => {
                            return `<div class="ck_media__wrapper" data-oembed-url="">
                                        <iframe style="min-height:498px;width:100%" frameborder="0" src="https://v.qq.com/txp/iframe/player.html?vid=${match[1]}" allowFullScreen="true"></iframe>
                                    </div>`;
                        }
                    },
                    {
                        /**
                         * Adapt to youku Video
                         * https://v.youku.com/v_show/id_XNTIwNTI3OTA4OA==.html
                         */
                        name: 'youku-video',
                        url: /^https:\/\/v\.youku\.com\/v_show\/id_(\w+)==\.html/,
                        html: match => {
                            return `<div class="ck_media__wrapper" data-oembed-url="">
                                        <iframe style="min-height:498px;width:100%" src='https://player.youku.com/embed/${match[1]}==' frameborder="0" allowFullScreen="true"></iframe>
                                    </div>`;
                        }
                    }
                ]
            };
            const editor = await super.create(target, options);
            
            editor.model.document.on('change:data', async () => {
                await reference.invokeMethodAsync('EditorContentChanged', editor.getData());
            });
            editor.editing.view.change(writer => {
                writer.setStyle('min-height', '400px', editor.editing.view.document.getRoot());
                writer.setStyle('max-height', '600px', editor.editing.view.document.getRoot());
            });
            return editor;
        }
    }
    ClassicEditor = BlazorEditor;
})();
