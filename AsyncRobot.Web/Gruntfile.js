module.exports = function (grunt) {
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
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
        csslint: {
            compile: {
                files: {
                    './Content/js/async-robot.min.js': ['./Content/js/*.js']
                }
            }
        },
        jshint: {
            all: ['./Content/js/*.js']
        },
        concat: {
            compile: {
                src: ['./Content/js/hub.js', './Content/js/maze.js', './Content/js/console.js', './Content/js/index.js'],
                dest: './Content/js/async-robot.js'
            }
        },
        uglify: {
            options: {
                mangle: false
            },
            compile: {
                files: {
                    './Content/js/async-robot.min.js': ['./Content/js/async-robot.js']
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
                tasks: ['coffee', 'concat', 'uglify']
            }
        }
    });

    // Load the plugin that provides the "watch" & "stylus" tasks.
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-stylus');
    grunt.loadNpmTasks('grunt-contrib-coffee');
    grunt.loadNpmTasks('grunt-contrib-cssmin');
    grunt.loadNpmTasks('grunt-contrib-csslint');
    grunt.loadNpmTasks('grunt-contrib-csshint');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-concat');

    // Default task(s).
    grunt.registerTask('default', ['stylus', 'cssmin', 'coffee', 'concat', 'uglify']);
    //grunt.registerTask('watch', ['stylus', 'cssmin', 'coffee']);
};