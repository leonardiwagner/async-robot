module.exports = function (grunt) {
    grunt.initConfig({
      
        stylus: {
            compile: {
                options: {
                    linenos: true,
                    compress: false
                },
                files: [{
                    expand: true,
                    cwd: './Content/css',
                    src: ['**/*.styl'],
                    dest: './Content/css',
                    ext: '.css'
                }]
            }
        },
        coffee: {
            compile: {
                expand: true,
                cwd: './Content/js',
                src: ['**/*.coffee'],
                dest: './Content/js',
                ext: '.js'
            }
        },
        cssmin: {
            compile: {
                files: {
                    './Content/css/async-robot.min.css': ['./Content/css/*.css']
                }
            }
        },
        uglify: {
            compile: {
                files: {
                    './Content/js/async-robot.min.js': ['./Content/js/*.js']
                }
            }
        },
        watch: {
            stylus: {
                files: ['./Content/css/*.styl'],
                tasks: ['stylus', 'cssmin']
            },
            coffee: {
                files: ['./Content/js/*.coffee'],
                tasks: ['coffee', 'uglify']
            }
        }
    });

    // Load the plugin that provides the "watch" & "stylus" tasks.
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-stylus');
    grunt.loadNpmTasks('grunt-contrib-coffee');
    grunt.loadNpmTasks('grunt-contrib-cssmin');
    grunt.loadNpmTasks('grunt-contrib-uglify');

    // Default task(s).
    grunt.registerTask('default', ['stylus', 'cssmin', 'coffee', 'uglify']);
};