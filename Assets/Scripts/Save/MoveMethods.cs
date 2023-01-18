/*
void Move(Vector2 direction) 
{
    direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

    // Recommended for wheels, flying in space etc.
    rb.AddForce(direction * speed); // accerlerar konstant ökningen!

    // Overwrites all physics elements including gravity and has no accerlation
    rb.velocity = direction * speed;

    // Let us move object to position but includes interaction with physics elements and collisions
    rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));  // rör sig nästan rakt ner, dålig känsla

    // Does not work with collision detection, will overwrite any physics used on object
    // Used for object that does require physics 
    transform.Translate(direction * speed * Time.deltaTime);


    Static - objects that shouldn’t move. 
    Kinematic - physics object, moving platform etc.
    Dynamic - 

}
        */